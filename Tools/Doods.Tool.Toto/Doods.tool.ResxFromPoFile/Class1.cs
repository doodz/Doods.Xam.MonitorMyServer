using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Doods.tool.ResxFromPoFile
{
    public class Class1
    {

        private readonly string Expression = @"<PackageReference Include=""(.*)"" Version=""(.*)"" />";
        private readonly string From = @"C:\GitHub\Doods.Xam.MonitorMyServer\src\";
        private readonly string To = @"C:\GitHub\Doods.Xam.MonitorMyServer\src\MonitorMyServer\Resx\ThirdPartyLicenseOverview_doods.txt";
        private readonly Dictionary<string, string> dico = new Dictionary<string, string>();
        public void Process()
        {
            foreach (var csprojFile in GetCsprojFiles())
            {
                Toto(csprojFile);
            }

            //using (var stream = File.OpenRead(To))
            //{
            //    stream.Write();
            //}
            using (var sw = new StreamWriter(To))
            {
                foreach (var val in dico.OrderBy(d=>d.Key))
                {
                    sw.WriteLine(val.Value);
                }
            }

        }


        private void Toto(string filePath)

        {
            var str = File.ReadAllText(filePath);
            var regex = new Regex(Expression);
            var matches = regex.Matches(str);
            // Report on each match.
           
            foreach (Match match in matches)
            {
                var groups = match.Groups;
                if (!dico.ContainsKey(groups[1].Value))
                {
                    dico.Add(groups[1].Value, match.Value);
                }
            }
        }
        private IEnumerable<string> GetCsprojFiles()
        {
            return GetFiles("csproj");
        }

        private IEnumerable<string> GetFiles(string extension)
        {
            //var path = Directory.GetCurrentDirectory();
            var path = From;
            var filePaths = Directory.GetFiles(path, $"*.{extension}", SearchOption.AllDirectories);
            return filePaths;
        }
    }
}
