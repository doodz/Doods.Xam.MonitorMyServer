using System;
using System.Globalization;

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
        public static string StoneBurner = "Stone burner";
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
        ///     Get Version object from string who contain version name
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

    // 4.1.35-1 (Arrakis)
    // 5.4.5-1 (Usul)

    public sealed class OMVVersion : ICloneable, IComparable, IComparable<OMVVersion?>
    {
        public string Name;

        public string Version;


        public OMVVersion(int major, int minor, int build, int revision)
        {
            if (major < 0)
                throw new ArgumentOutOfRangeException(nameof(major), "ArgumentOutOfRange_Version");

            if (minor < 0)
                throw new ArgumentOutOfRangeException(nameof(minor), "ArgumentOutOfRange_Version");

            if (build < 0)
                throw new ArgumentOutOfRangeException(nameof(build), "ArgumentOutOfRange_Version");

            if (revision < 0)
                throw new ArgumentOutOfRangeException(nameof(revision), "ArgumentOutOfRange_Version");

            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        private OMVVersion(OMVVersion version)
        {
            Major = version.Major;
            Minor = version.Minor;
            Build = version.Build;
            Revision = version.Revision;
        }

        public OMVVersion(int major, int minor, int build)
        {
            if (major < 0)
                throw new ArgumentOutOfRangeException(nameof(major), "ArgumentOutOfRange_Version");

            if (minor < 0)
                throw new ArgumentOutOfRangeException(nameof(minor), "ArgumentOutOfRange_Version");

            if (build < 0)
                throw new ArgumentOutOfRangeException(nameof(build), "ArgumentOutOfRange_Version");


            Major = major;
            Minor = minor;
            Build = build;
        }

        public OMVVersion(int major, int minor)
        {
            if (major < 0)
                throw new ArgumentOutOfRangeException(nameof(major), "ArgumentOutOfRange_Version");

            if (minor < 0)
                throw new ArgumentOutOfRangeException(nameof(minor), "ArgumentOutOfRange_Version");

            Major = major;
            Minor = minor;
        }

        public OMVVersion(string version)
        {
            var v = Parse(version);
            Major = v.Major;
            Minor = v.Minor;
            Build = v.Build;
            Revision = v.Revision;
        }

        public OMVVersion()
        {
            Major = 0;
            Minor = 0;
        }


        internal OMVVersion(string version, string name) : this(version)
        {
            Name = name;
        }

        public int Major { get; }

        public int Minor { get; }

        public int Build { get; } = -1;

        public int Revision { get; } = -1;

        public object Clone()
        {
            return new OMVVersion(this);
        }

        public int CompareTo(object? version)
        {
            if (version == null) return 1;

            if (version is OMVVersion v) return CompareTo(v);

            throw new ArgumentException("Arg_MustBeVersion");
        }

        public int CompareTo(OMVVersion? value)
        {
            return
                ReferenceEquals(value, this) ? 0 :
                value is null ? 1 :
                Major != value.Major ? Major > value.Major ? 1 : -1 :
                Minor != value.Minor ? Minor > value.Minor ? 1 : -1 :
                Build != value.Build ? Build > value.Build ? 1 : -1 :
                Revision != value.Revision ? Revision > value.Revision ? 1 : -1 :
                0;
        }

        public static OMVVersion Parse(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            return ParseVersion(input.AsSpan(), true)!;
        }

        private static OMVVersion? ParseVersion(ReadOnlySpan<char> input, bool throwOnFailure)
        {
            var array = new[] {'.'};
            var separator = new ReadOnlySpan<char>(array);


            // Find the separator between major and minor.  It must exist.
            var majorEnd = input.IndexOf('.');
            if (majorEnd < 0)
            {
                if (throwOnFailure)
                    throw new ArgumentException("Separator between major and minor must exist", nameof(input));
                return null;
            }

            // Find the ends of the optional minor and build portions.
            // We musn't have any separators after build.
            var buildEnd = -1;
            var minorEnd = input.Slice(majorEnd + 1).IndexOf('.');
            if (minorEnd != -1)
            {
                minorEnd += majorEnd + 1;
                buildEnd = input.Slice(minorEnd + 1).IndexOf('.');
                if (buildEnd != -1)
                {
                    buildEnd += minorEnd + 1;
                    if (input.Slice(buildEnd + 1).Contains(separator, StringComparison.InvariantCultureIgnoreCase))


                    {
                        if (throwOnFailure)
                            throw new ArgumentException("Separator between major and minor must exist", nameof(input));
                        return null;
                    }
                }
            }

            int minor, build, revision;
            // Parse the major version
            if (!TryParseComponent(input.Slice(0, majorEnd), nameof(input), throwOnFailure, out var major)) return null;

            if (minorEnd != -1)
            {
                // If there's more than a major and minor, parse the minor, too.
                if (!TryParseComponent(input.Slice(majorEnd + 1, minorEnd - majorEnd - 1), nameof(input),
                    throwOnFailure, out minor))
                    return null;

                if (buildEnd != -1)
                    // major.minor.build.revision
                    return
                        TryParseComponent(input.Slice(minorEnd + 1, buildEnd - minorEnd - 1), nameof(build),
                            throwOnFailure, out build) &&
                        TryParseComponent(input.Slice(buildEnd + 1), nameof(revision), throwOnFailure, out revision)
                            ? new OMVVersion(major, minor, build, revision)
                            : null;
                return TryParseComponent(input.Slice(minorEnd + 1), nameof(build), throwOnFailure, out build)
                    ? new OMVVersion(major, minor, build)
                    : null;
            }

            // major.minor
            return TryParseComponent(input.Slice(majorEnd + 1), nameof(input), throwOnFailure, out minor)
                ? new OMVVersion(major, minor)
                : null;
        }

        private static bool TryParseComponent(ReadOnlySpan<char> component, string componentName, bool throwOnFailure,
            out int parsedComponent)
        {
            if (throwOnFailure)
            {
                if ((parsedComponent = int.Parse(component, NumberStyles.Integer, CultureInfo.InvariantCulture)) < 0)
                    throw new ArgumentOutOfRangeException(componentName, "ArgumentOutOfRange_Version");

                return true;
            }

            return int.TryParse(component, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedComponent) &&
                   parsedComponent >= 0;
        }


        public static bool operator ==(OMVVersion? v1, OMVVersion? v2)
        {
            // Test "right" first to allow branch elimination when inlined for null checks (== null)
            // so it can become a simple test
            if (v2 is null)
                // return true/false not the test result https://github.com/dotnet/coreclr/issues/914
                return v1 is null ? true : false;

            // Quick reference equality test prior to calling the virtual Equality
            return ReferenceEquals(v2, v1) ? true : v2.Equals(v1);
        }

        public static bool operator !=(OMVVersion? v1, OMVVersion? v2)
        {
            return !(v1 == v2);
        }

        public static bool operator <(OMVVersion? v1, OMVVersion? v2)
        {
            if (v1 is null) return !(v2 is null);

            return v1.CompareTo(v2) < 0;
        }

        public static bool operator <=(OMVVersion? v1, OMVVersion? v2)
        {
            if (v1 is null) return true;

            return v1.CompareTo(v2) <= 0;
        }

        public static bool operator >(OMVVersion? v1, OMVVersion? v2)
        {
            return v2 < v1;
        }

        public static bool operator >=(OMVVersion? v1, OMVVersion? v2)
        {
            return v2 <= v1;
        }
    }
}