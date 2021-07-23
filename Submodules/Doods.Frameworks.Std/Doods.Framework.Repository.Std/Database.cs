using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Doods.Framework.Repository.Std.Interfaces;
using Doods.Framework.Repository.Std.Migrations;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std;
using Doods.Framework.Std.Extensions;
using SQLite;

namespace Doods.Framework.Repository.Std
{
    public class Database : IDatabase
    {
        private readonly SQLiteAsyncConnection _asyncConnection;
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private readonly ILogger _logger;
        private readonly ITelemetryService _telemetry;

        public Database(ISqLiteFactory factory, ILogger looger, ITelemetryService telemetryService)
        {
            _asyncConnection = new SQLiteAsyncConnection(factory.GetDatabasePath(factory.DefaultDatabaseName),
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);

            _logger = looger;
            _telemetry = telemetryService;
        }

        private IEnumerable<IMigration> Migrations
        {
            get
            {
                yield return new Migration1();
                yield return new Migration2();
                yield return new Migration3();
            }
        }

        public bool IsInitialize { get; private set; }

        public async Task<SQLiteAsyncConnection> GetConnection(ITimeWatcher timer)
        {
            await Initialize(timer);
            return _asyncConnection;
        }

        public async Task Reset()
        {
            await _lock.WaitAsync();

            IsInitialize = false;
            await Drop();

            _lock.Release();
        }

        private async Task Initialize(ITimeWatcher timer)
        {
            if (IsInitialize) return;
            await _lock.WaitAsync();

            if (IsInitialize)
            {
                _lock.Release();
                return;
            }

            using (timer.StartWatcher("Database.Initialize"))
            {
                try
                {
                    await _asyncConnection.CreateTableAsync<Host>();
                    await _asyncConnection.CreateTableAsync<CustomCommandSsh>();

                    await Migrate();

                    IsInitialize = true;
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                    _telemetry.Exception(e);
                }
            }

            _lock.Release();
        }

        public async Task Drop()
        {
            try
            {
                await _asyncConnection.DropTableAsync<Host>();
                await _asyncConnection.DropTableAsync<CustomCommandSsh>();


                await SetSchemaVersion(0);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                _telemetry.Exception(e);
            }
        }

        private async Task Migrate()
        {
            var currentVersion = await GetSchemaVersion();
            var migrations = Migrations.Where(m => m.VersionNumber > currentVersion).ToList();

            if (migrations.IsNotEmpty())
                foreach (var migration in migrations)
                {
                    await _asyncConnection.RunInTransactionAsync(c => migration.Run(c));
                    await SetSchemaVersion(migration.VersionNumber);
                }
        }

        private Task<int> GetSchemaVersion()
        {
            return _asyncConnection.ExecuteScalarAsync<int>("PRAGMA user_version;");
        }

        private Task SetSchemaVersion(int version)
        {
            return _asyncConnection.ExecuteAsync($"PRAGMA user_version = {version};");
        }
    }
}