namespace Doods.Webmin.Webapi.Std.Datas
{
    public struct DiskElement
    {
        public long? Integer;
        public string String;

        public static implicit operator DiskElement(long Integer)
        {
            return new DiskElement {Integer = Integer};
        }

        public static implicit operator DiskElement(string String)
        {
            return new DiskElement {String = String};
        }
    }
}