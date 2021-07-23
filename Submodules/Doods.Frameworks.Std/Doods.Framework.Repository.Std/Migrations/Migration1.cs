using Doods.Framework.Repository.Std.Interfaces;
using SQLite;

namespace Doods.Framework.Repository.Std.Migrations
{
    internal class Migration1 : IMigration
    {
        public int VersionNumber => 1;

        public void Run(SQLiteConnection connection)
        {
            connection.CreateCommand("ALTER TABLE Host ADD COLUMN Url VARCHAR(255)");
        }
    }
}