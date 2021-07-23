namespace Doods.Framework.Ssh.Std.Base.Queries
{
    public abstract class Queries<T> where T : new()
    {
        public T Run()
        {
            return new T();
        }
    }
}