using System;
using System.IO;

namespace VersionUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            var newVersion = "";
            var path = "";
            var version = "";

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains("path"))
                    path = args[i].Split('=')[1];
                if (args[i].Contains("version"))
                    version = args[i].Split('=')[1];
            }
            if (path == "")
                path = Environment.CurrentDirectory;


            try
            {
                string[] filePaths = Directory.GetFiles(path, "AssemblyInfo.cs",
                                         SearchOption.AllDirectories);



                var CurrentVersion = GetVersion(filePaths[0]);
                var CurrentBuild = CurrentVersion.Split('.')[2];
                var AutoBuildNumber = (int.Parse(CurrentBuild) + 1).ToString();

                if (version != "")
                {
                    newVersion = version;
                    Console.WriteLine("Version was updated from " + CurrentVersion + " ==> " + version);
                }
                else
                {
                    newVersion = BuildVersion(AutoBuildNumber, CurrentVersion);
                    Console.WriteLine("Build number was updated from " + CurrentBuild + " ==> " + AutoBuildNumber + " ==> " + newVersion);

                }

                for (int i = 0; i < filePaths.Length; i++)
                {
                    SetVersion(newVersion, filePaths[i]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static string BuildVersion(string version, string Currentversion)
        {
            string newVer = "";
            for (int i = 0; i <= 3; i++)
            {
                if (i != 2)
                    newVer = newVer + Currentversion.Split('.')[i] + ".";
                else
                    newVer = newVer + version + ".";
            }
            return newVer.Substring(0, newVer.LastIndexOf('.'));
        }



        static string GetVersion(string file)
        {
            string version = "";
            foreach (string line in File.ReadLines(file))
            {
                if (line.Contains("[assembly: AssemblyVersion"))
                {
                    version = line.Split('"')[1];
                }
            }
            return version;
        }
        static void SetVersion(string newVersion, string assemblyInfoFile)
        {
            using (var sr = new StringReader(File.ReadAllText(assemblyInfoFile)))
            {
                using (var sw = new StringWriter())
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith("[assembly: AssemblyVersion"))
                            line = $"[assembly: AssemblyVersion(\"{newVersion}\")]";
                        if (line.StartsWith("[assembly: AssemblyFileVersion"))
                            line = $"[assembly: AssemblyFileVersion(\"{newVersion}\")]";
                        sw.WriteLine(line);
                    }
                    File.WriteAllText(assemblyInfoFile, sw.ToString(), System.Text.Encoding.Unicode);
                }
            }
        }
    }
}
