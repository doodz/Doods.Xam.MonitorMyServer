using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;

namespace Doods.Tool.Toto
{
    internal class Program
    {
        //C:\GitHub\openmediavault\deb\openmediavault\usr\share\openmediavault\locale
        private static readonly string Expression = @"msgid ""(.*)""\w*\s*msgstr ""(.*)""";

        private static readonly string From = @"C:\GitHub\openmediavault\";
        private static readonly string To = @"msgid ""(.*)""\w*\s*msgstr ""(.*)""";

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello doods!");
            ProcessPotToREsxFile();
            ProcessPoToREsxFile();
           
        }

        private static void ProcessPotToREsxFile()
        {
            foreach (var potFile in GetPotFiles()) PotToResx(potFile);
        }

        private static void ProcessPoToREsxFile()
        {
            foreach (var poFile in GetPoFiles()) PoToResx(poFile);
        }


        private static void PotToResx(string poFilePath)
        {
            var info = new FileInfo(poFilePath);
            

            var resourceWriter = new ResXResourceWriter(poFilePath.Replace($".pot", ".resx"));
            ExtensionToResx(poFilePath, resourceWriter);
        }
        private static void PoToResx(string poFilePath)
        {
            var info = new FileInfo(poFilePath);
            var derectoriinfo = new DirectoryInfo(poFilePath);
            var resourceWriter =
                new ResXResourceWriter(
                    poFilePath.Replace(".po", $".{derectoriinfo.Parent.Name.Replace('_', '-')}.resx"));
            ExtensionToResx(poFilePath, resourceWriter);

        }
        private static void ExtensionToResx(string filePath, ResXResourceWriter resourceWriter)
        {
            var str = File.ReadAllText(filePath);
            var regex = new Regex(Expression);
            // Find matches.
            var matches = regex.Matches(str);
            // Report on each match.
            var dico = new Dictionary<string, string>();

            foreach (Match match in matches)
            {
                var groups = match.Groups;
                var key = ToCamelCase(groups[1].Value).Trim();
                var lowerKey = key.ToLower();
                if (!string.IsNullOrWhiteSpace(key) && dico.Keys.All(k => k.ToLower() != lowerKey))
                    dico.Add(key, groups[1].Value);
            }

            foreach (var lst in dico) resourceWriter.AddResource(lst.Key, lst.Value);

            resourceWriter.Generate();
            resourceWriter.Close();
        }

        private static string ToCamelCase(string str)
        {
            var titlecase = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
            var camelcase = titlecase.Replace("_", string.Empty).Replace(" ", string.Empty);
            return camelcase;
        }

        private static IEnumerable<string> GetFiles(string extension)
        {
            //var path = Directory.GetCurrentDirectory();
            var path = From;
            var filePaths = Directory.GetFiles(path, $"*.{extension}", SearchOption.AllDirectories);
            return filePaths;
        }

        private static IEnumerable<string> GetPoFiles()
        {
            return GetFiles("po");
           
        }

        private static IEnumerable<string> GetPotFiles()
        {
            return GetFiles("pot");
        }
    }
}