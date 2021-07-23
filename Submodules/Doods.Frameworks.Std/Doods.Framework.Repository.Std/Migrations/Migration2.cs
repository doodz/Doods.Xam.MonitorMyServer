using Doods.Framework.Repository.Std.Interfaces;
using SQLite;

namespace Doods.Framework.Repository.Std.Migrations
{
    internal class Migration2 : IMigration
    {
        public int VersionNumber => 2;

        public void Run(SQLiteConnection connection)
        {
            connection.CreateCommand("ALTER TABLE Host ADD COLUMN IsSynoServer integer");
        }
    }
}