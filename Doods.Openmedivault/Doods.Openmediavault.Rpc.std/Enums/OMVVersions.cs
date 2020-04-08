using System.Xml.XPath;

namespace Doods.Openmediavault.Mobile.Std.Enums
{
    public enum OMVNames
    {
        NotFound,
        Ix,
        Omnius,
        Fedaykin,
        Sardaukar,
        Kralizec,
        Stone_burner,
        Erasmus,
        Arrakis,
        Usul
    }


    public sealed class OMVVersions
    {


       
        public static string Ix = "Ix";
        public static string Omnius = "Omnius";
        public static string Fedaykin = "Fedaykin";
        public static string Sardaukar = "Sardaukar";
        public static string Kralizec = "Kralizec";
        public static string StoneBurner  = "Stone burner";
        public static string Erasmus = "Erasmus";
        public static string Arrakis = "Arrakis";
        public static string Usul = "Usul";


        public static OMVVersion NotFound = new OMVVersion("0.0", "NotFound");
        public static OMVVersion Version02 = new OMVVersion("0.2", "Ix");
        public static OMVVersion Version03 = new OMVVersion("0.3", "Omnius");
        public static OMVVersion Version04 = new OMVVersion("0.4", "Fedaykin");
        public static OMVVersion Version05 = new OMVVersion("0.5", "Sardaukar");
        public static OMVVersion Version1 = new OMVVersion("1.0", "Kralizec");
        public static OMVVersion Version2 = new OMVVersion("2.0", "Stone burner");
        public static OMVVersion Version3 = new OMVVersion("3.0", "Erasmus");
        public static OMVVersion Version4 = new OMVVersion("4.0", "Arrakis");
        public static OMVVersion Version5 = new OMVVersion("5.0", "Usul");


        /// <summary>
        /// Get Version object from string who contain version name 
        /// </summary>
        /// <param name="version">version of OMV from RPC like "version: "5.0.5-1 (Usul)"</param>
        /// <returns></returns>
        public static OMVVersion GetVersionFromString(string version)
        {
            switch (version)
            {
                case string v when v.Contains(Version02.Name): return Version02;
                case string v when v.Contains(Version03.Name): return Version03;
                case string v when v.Contains(Version04.Name): return Version04;
                case string v when v.Contains(Version05.Name): return Version05;
                case string v when v.Contains(Version1.Name): return Version1;
                case string v when v.Contains(Version2.Name): return Version2;
                case string v when v.Contains(Version3.Name): return Version3;
                case string v when v.Contains(Version4.Name): return Version4;
                case string v when v.Contains(Version5.Name): return Version5;
                default: return NotFound;
            }
        }
    }


    public sealed class OMVVersion
    {
        public string Name;

        public string Version;

        private OMVVersion()
        {
        }

        internal OMVVersion(string version, string name)
        {
            Version = version;
            Name = name;
        }
    }
}