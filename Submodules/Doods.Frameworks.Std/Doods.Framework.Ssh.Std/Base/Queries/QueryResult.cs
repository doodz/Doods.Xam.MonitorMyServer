namespace Doods.Framework.Ssh.Std.Base.Queries
{
    public class QueryResult<T>
    {
        internal QueryResult()
        {
        }

        public T Result { get; set; }
        public string Error { get; set; }

        public string Query { get; set; }
        public string BashLines { get; set; }

        public int ExitStatus { get; set; }

        public bool HaveError => !string.IsNullOrEmpty(Error);
    }
}