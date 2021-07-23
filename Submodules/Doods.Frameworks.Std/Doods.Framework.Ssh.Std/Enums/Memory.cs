namespace Doods.Framework.Ssh.Std.Enums
{
    //TODO : I think implement a structure is better.
    public class Memory
    {
        private Memory(string name, string shortName, long scale)
        {
            LongName = name;
            ShortName = shortName;
            Scale = scale;
        }

        public static Memory B => new Memory("Byte", "B", 1);
        public static Memory KB => new Memory("KiloByte", "KB", 1024);
        public static Memory MB => new Memory("MegaByte", "MB", 1024 * 1024);
        public static Memory GB => new Memory("GigaByte", "GB", 1024 * 1024 * 1024);
        public static Memory TB => new Memory("TeraByte", "TB", 1099511627776); //1024 * 1024 * 1024 * 1024


        public string LongName { get; set; }

        public string ShortName { get; set; }

        public long Scale { get; set; }

        public static bool operator ==(Memory left, Memory right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);

            return left.Equals(right);
            //if (left == null || right == null) return false;
            //return left.ShortName == right.ShortName;
        }

        public static bool operator !=(Memory left, Memory right)
        {
            if (ReferenceEquals(left, null))
                return !ReferenceEquals(right, null);

            return !left.Equals(right);

            //if (left == null || right == null) return false;
            //return left.ShortName != right.ShortName;
        }

        protected bool Equals(Memory other)
        {
            return string.Equals(LongName, other.LongName) && string.Equals(ShortName, other.ShortName) &&
                   Scale == other.Scale;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Memory) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = LongName != null ? LongName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (ShortName != null ? ShortName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Scale.GetHashCode();
                return hashCode;
            }
        }
    }
}