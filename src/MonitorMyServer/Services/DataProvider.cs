﻿using Doods.Framework.Repository.Std.Interfaces;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doods.Xam.MonitorMyServer.Services
{
    public interface IDataProvider
    {
        //IRepository Repository { get; }
        Task<int> InsertHostAsync(Host host);
        Task<Host> FindHostAsync(Host host);
        Task<Host> FindHostAsync(long hostId);
        Task UpdateHostAsync(Host host);
        Task UpdateCustomCommandAsync(CustomCommandSsh customCommandSsh);
        Task<int> InsertCustomCommandAsync(CustomCommandSsh customCommandSsh);
        Task<IEnumerable<Host>> GetHostsAsync();
        Task<int> CountHostAsync();
        Task DeleteHostAsync(Host host);
        Task<IEnumerable<T>> GetItemsAsync<T>() where T : TableBase, new();
        Task DeleteItemAsync(TableBase item);
    }
    public class DataProvider: NotifyPropertyChangedBase, IDataProvider
    {
        private IRepository _repository;
        private readonly ITimeWatcher _timer;
        public DataProvider(IRepository repository)
        {
            _timer = new TimeWatcher();
            _repository = repository;
        }

        public async Task<IEnumerable<Host>> GetHostsAsync()
        {
            return await _repository.GetAllAsync<Host>(_timer);
        }
        public async Task<IEnumerable<T>> GetItemsAsync<T>() where T: TableBase,new()
        {
            return await _repository.GetAllAsync<T>(_timer);
        }
        public async Task<int> InsertHostAsync(Host host)
        {
            return await _repository.InsertAsync(_timer, host);

        }

        public async Task<int> InsertCustomCommandAsync(CustomCommandSsh customCommandSsh)
        {
            return await _repository.InsertAsync(_timer, customCommandSsh);
        }
        public async Task<Host> FindHostAsync(Host host)
        {
            return await FindHostAsync(host.Id.Value);
        }

        public async Task<Host> FindHostAsync(long hostId)
        {
            return await _repository.FindAsync<Host>(_timer, hostId);
        }

        public Task<int> CountHostAsync()
        {
            return _repository.CountAsync<Host>(_timer);
        }
        public async Task UpdateHostAsync(Host host)
        {
            await _repository.UpdateAsync(_timer, host);

        }

        public async Task UpdateCustomCommandAsync(CustomCommandSsh customCommandSsh)
        {
            await _repository.UpdateAsync(_timer, customCommandSsh);

        }

        public async Task DeleteHostAsync(Host host)
        {
            await _repository.DeleteAsync(_timer, host);
        }

        public async Task DeleteItemAsync(TableBase item)
        {
            await _repository.DeleteAsync(_timer, item);
        }
    }
}
