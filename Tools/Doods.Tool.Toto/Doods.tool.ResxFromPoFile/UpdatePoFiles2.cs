using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;

namespace Doods.tool.ResxFromPoFile
{
    class UpdatePoFilesWebmin
    {
        //C:\GitHub\webmin\*\lang
        private readonly string Expression = @"msgid ""(.*)""\w*\s*msgstr ""(.*)""";

        private readonly string Expression2 =
            "msgid \"(.*)\"\\w*\\s*msgid_plural \"(.*)\"\\w*\\s*msgstr\\[0\\] \"(.*)\"\\w*\\s*msgstr\\[1\\] \"(.*)\"";

        private readonly string From = @"C:\GitHub\webmin\";
        private readonly string To = @"C:\GitHub\Doods.Xam.MonitorMyServer\Doods.Webmin\Doods.Webmin.Webapi.Std\Resx\";

        public void Process()
        {
           var dirs = Directory.GetDirectories(From, "lang", SearchOption.AllDirectories);
           foreach (var dir in dirs)
           {
               var dirName =Path.GetDirectoryName(dir).Split(Path.DirectorySeparatorChar).Last();
               var files = Directory.EnumerateFiles(dir);
               foreach (var file in files)
               {
                   var n = Path.GetFileName(file);
                   var resname = "Webmin_" + dirName + "." + n+ ".resx";
                   Directory.CreateDirectory(To + "\\" + dirName);
                   var resourceWriter = new ResXResourceWriter(To+ "\\"+dirName+"\\" + resname);
                    var info = new FileInfo(file);
                  
                   var dico = new Dictionary<string, string>();
                   var str = File.ReadAllLines(file).Where(r=> !string.IsNullOrWhiteSpace(r) && !r.Trim().StartsWith("#"));

                   foreach (var s in str)
                   {
                       var split =s.Split('=');
                        if(split.Count()<2)
                            continue;
                        if (split.Count() > 2)
                        {
                            continue;
                        }

                        if (!dico.ContainsKey(split[0]))
                       {
                           dico.Add(split[0], split[1]);
                       }
                   }

                   foreach (var lst in dico) resourceWriter.AddResource(lst.Key, lst.Value);

                  
                   resourceWriter.Generate();
                   resourceWriter.Close();

                }
           }
        }
    }
    class UpdatePoFilesCockpit
    {
        //C:\GitHub\openmediavault\deb\openmediavault\usr\share\openmediavault\locale
        private readonly string Expression = @"msgid ""(.*)""\w*\s*msgstr ""(.*)""";

        private readonly string Expression2 =
            "msgid \"(.*)\"\\w*\\s*msgid_plural \"(.*)\"\\w*\\s*msgstr\\[0\\] \"(.*)\"\\w*\\s*msgstr\\[1\\] \"(.*)\"";

        private  readonly string From = @"C:\GitHub\cockpit\po\";
        private  readonly string To = @"C:\GitHub\Doods.Xam.MonitorMyServer\src\MonitorMyServer\Resx\";



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
            var newfileName = To + "cockpit." + info.Name.Replace(info.Extension, ".resx");
            newfileName = newfileName.Replace(".en.resx", ".resx");
            var resourceWriter = new ResXResourceWriter(newfileName);
            ExtensionToResx(poFilePath, resourceWriter, 1);
        }
        private  void PoToResx(string poFilePath)
        {
            var info = new FileInfo(poFilePath);
            var derectoriinfo = new DirectoryInfo(poFilePath);

            var newfileName = To + "cockpit." + info.Name.Replace(info.Extension, ".resx");


            var resourceWriter =
                new ResXResourceWriter(newfileName);
            ExtensionToResx(poFilePath, resourceWriter, 2);

        }
        private  void ExtensionToResx(string filePath, ResXResourceWriter resourceWriter, int pos)
        {
            var str = File.ReadAllText(filePath);

            str = str.Replace("msgid \"\"\r\n", "msgid ");
            str = str.Replace("\"\r\n\"", " ");

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

            ExtensionPluralToResx(filePath,resourceWriter, pos);
            resourceWriter.Generate();
            resourceWriter.Close();
        }


        private void ExtensionPluralToResx(string filePath, ResXResourceWriter resourceWriter, int pos)
        {
            var str = File.ReadAllText(filePath);

            str = str.Replace("msgid \"\"\r\n", "msgid ");
            str = str.Replace("\"\r\n\"", " ");

            var regex = new Regex(Expression2);
            // Find matches.
            var matches = regex.Matches(str);
            // Report on each match.
            var dico = new Dictionary<string, string>();

            foreach (Match match in matches)
            {
               

                var groups = match.Groups;
                var key = ToCamelCase(groups[1].Value).Trim();
                var lowerKey = key.ToLower();
                if (!string.IsNullOrWhiteSpace(groups[1].Value) && !string.IsNullOrWhiteSpace(key + 1)
                                                                && dico.Keys.All(k => k.ToLower() != lowerKey))
                    dico.Add(key, groups[pos].Value);
                key = ToCamelCase(groups[2].Value).Trim();
                lowerKey = key.ToLower();
                if (!string.IsNullOrWhiteSpace(groups[1].Value) && !string.IsNullOrWhiteSpace(key + 2)
                                                                && dico.Keys.All(k => k.ToLower() != lowerKey))
                    dico.Add(key, groups[pos].Value);
            }

            foreach (var lst in dico) resourceWriter.AddResource(lst.Key, lst.Value);

          
        }

        private  string ToCamelCase(string str)
        {
            var titlecase = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
            var camelcase = titlecase.Replace("_", string.Empty).Replace(" ", string.Empty);
            camelcase = camelcase.Replace("$", "_dol_");
            camelcase = camelcase.Replace(",", "_comma_");
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

            yield return From + "en.po";
        }
    }
}
