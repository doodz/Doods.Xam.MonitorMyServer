using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Doods.tool.ResxFromPoFile
{
    class UpdatePoFiles
    {
        //C:\GitHub\openmediavault\deb\openmediavault\usr\share\openmediavault\locale
        private  readonly string Expression = @"msgid ""(.*)""\w*\s*msgstr ""(.*)""";

        private  readonly string From = @"C:\GitHub\openmediavault\";
        private  readonly string To = @"C:\GitHub\Doods.Xam.MonitorMyServer\Doods.Openmedivault\Doods.Openmediavault.Mobile.Std\Resources\";



        public void Process()
        {
            ProcessPotToREsxFile();
            ProcessPoToREsxFile();
        }
        private  void ProcessPotToREsxFile()
        {
            foreach (var potFile in GetPotFiles()) PotToResx(potFile);
        }

        private  void ProcessPoToREsxFile()
        {
            foreach (var poFile in GetPoFiles()) PoToResx(poFile);
        }


        private  void PotToResx(string poFilePath)
        {
            var info = new FileInfo(poFilePath);
            var newfileName = To + info.Name.Replace(info.Extension, ".resx");

            var resourceWriter = new ResXResourceWriter(newfileName);
            ExtensionToResx(poFilePath, resourceWriter, 1);
        }
        private  void PoToResx(string poFilePath)
        {
            var info = new FileInfo(poFilePath);
            var derectoriinfo = new DirectoryInfo(poFilePath);

            var newfileName = To + info.Name.Replace(info.Extension, $".{derectoriinfo.Parent.Name.Replace('_', '-')}.resx");


            var resourceWriter =
                new ResXResourceWriter(newfileName);
            ExtensionToResx(poFilePath, resourceWriter, 2);

        }
        private  void ExtensionToResx(string filePath, ResXResourceWriter resourceWriter, int pos)
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
                if (!string.IsNullOrWhiteSpace(groups[1].Value) && !string.IsNullOrWhiteSpace(key)
                                                                && dico.Keys.All(k => k.ToLower() != lowerKey))
                    dico.Add(key, groups[pos].Value);
            }

            foreach (var lst in dico) resourceWriter.AddResource(lst.Key, lst.Value);

            resourceWriter.Generate();
            resourceWriter.Close();
        }

        private  string ToCamelCase(string str)
        {
            var titlecase = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
            var camelcase = titlecase.Replace("_", string.Empty).Replace(" ", string.Empty);
            return camelcase;
        }

        private  IEnumerable<string> GetFiles(string extension)
        {
            //var path = Directory.GetCurrentDirectory();
            var path = From;
            var filePaths = Directory.GetFiles(path, $"*.{extension}", SearchOption.AllDirectories);
            return filePaths;
        }

        private  IEnumerable<string> GetPoFiles()
        {
            return GetFiles("po");

        }

        private  IEnumerable<string> GetPotFiles()
        {
            return GetFiles("pot");
        }
    }
}
