namespace Doods.Framework.Repository.Std.Interfaces
{
    public interface ISqLiteFactory
    {
        string DefaultDatabaseName { get; }
        string GetDatabasePath(string fileName);
    }
}