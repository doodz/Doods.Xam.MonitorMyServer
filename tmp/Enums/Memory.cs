namespace Doods.Framework.Ssh.Std.Enums
{
    //TODO : I think implement a structure is better.
    public class Memory
    {
        public static Memory B => new Memory("Byte", "B", 1);
        public static Memory KB => new Memory("KiloByte", "KB", 1024);
        public static Memory MB => new Memory("MegaByte", "MB", 1024 * 1024);
        public static Memory GB => new Memory("GigaByte", "GB", 1024 * 1024 * 1024);
        public static Memory TB => new Memory("TeraByte", "TB", 1099511627776); //1024 * 1024 * 1024 * 1024

        private string _longName;
        private string _shortName;
        private long _scale;


        public string LongName
        {
            get => _longName;
            set => _longName = value;
        }

        public string ShortName
        {
            get => _shortName;
            set => _shortName = value;
        }

        public long Scale
        {
            get => _scale;
            set => _scale = value;
        }

        private Memory(string name, string shortName, long scale)
        {
            _longName = name;
            _shortName = shortName;
            _scale = scale;
        }

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
            return string.Equals(_longName, other._longName) && string.Equals(_shortName, other._shortName) &&
                   _scale == other._scale;
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
                var hashCode = _longName != null ? _longName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (_shortName != null ? _shortName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ _scale.GetHashCode();
                return hashCode;
            }
        }
    }
}