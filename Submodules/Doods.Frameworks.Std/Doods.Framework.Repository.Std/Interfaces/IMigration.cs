using SQLite;

namespace Doods.Framework.Repository.Std.Interfaces
{
    internal interface IMigration
    {
        int VersionNumber { get; }

        void Run(SQLiteConnection connection);
    }
}