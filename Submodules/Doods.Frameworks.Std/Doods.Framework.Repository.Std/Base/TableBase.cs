using System;
using Doods.Framework.Repository.Std.Interfaces;
using SQLite;

namespace Doods.Framework.Repository.Std.Tables
{
    public class TableBase : ITableBase
    {
        public const long DefaultId = -1;

        public DateTimeOffset SyncDate { get; set; }

        [PrimaryKey] [AutoIncrement] public long? Id { get; set; }
    }
}