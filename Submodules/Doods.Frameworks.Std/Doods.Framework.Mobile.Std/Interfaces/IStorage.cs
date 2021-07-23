namespace Doods.Framework.Mobile.Std.Interfaces
{
    public interface IStorage
    {
        string GetValue(string key, string defaultValue = null);

        void SetValue(string key, string value);

        void DeleteKey(string key);

        bool HasKey(string key);
    }
}