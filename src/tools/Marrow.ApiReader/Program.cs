using System;
using System.IO;
using System.Linq;
using System.Text;
using static System.Environment;

namespace Marrow.ApiReader
{
    internal static class Program
    {
        internal const string MauiSpace = "Microsoft.Maui.";
        internal const string MyLibSpace = "Marrow.XPlat.";
        private const string MyLibName = $"{MyLibSpace}API";

        private static void Main(string[] args)
        {
            var userPath = GetFolderPath(SpecialFolder.UserProfile);
            Console.WriteLine($"User home  = {userPath}");

            var nugetPath = Path.Combine(userPath, ".nuget", "packages");
            Console.WriteLine($"NuGet path = {nugetPath}");

            var currentPath = CurrentDirectory;
            var apiPath = Path.GetFullPath(Path.Combine(currentPath, "..", "..",
                "..", "..", "..", "framework", MyLibName));
            Console.WriteLine($"My path    = {apiPath}");

            var sep = Path.DirectorySeparatorChar;
            var dllNames = new[] { $"{MauiSpace}Essentials", MyLibName };
            var frameNames = new[] { "net8.0" };
            frameNames = frameNames.Select(fn => sep + fn + sep).ToArray();

            foreach (var path in new[] { nugetPath, apiPath })
            {
                var files = Directory.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    if (!dllNames.Any(file.Contains))
                        continue;
                    if (!frameNames.Any(file.Contains))
                        continue;

                    var shortPath = file[path.Length..].Trim(sep);
                    var shortTmp = shortPath.Split(sep);

                    FoundDll found;
                    if (shortTmp.Length == 5)
                    {
                        var pkgName = shortTmp[0];
                        if (pkgName == "obj")
                            continue;
                        var pkgVer = new Version(shortTmp[1]);
                        var fwName = shortTmp[3];
                        var dllName = Path.GetFileNameWithoutExtension(shortTmp[4]);
                        found = new FoundDll(pkgName, pkgVer, fwName, dllName, file);
                    }
                    else if (shortTmp.Length == 4)
                    {
                        if (shortTmp[0] == "obj")
                            continue;
                        var pkgName = Path.GetFileName(path);
                        var pkgVer = new Version(1, 0);
                        var fwName = shortTmp[2];
                        var dllName = Path.GetFileNameWithoutExtension(shortTmp[3]);
                        found = new FoundDll(pkgName, pkgVer, fwName, dllName, file);
                    }
                    else
                    {
                        throw new InvalidOperationException(file);
                    }
                    Console.WriteLine($" * {found.Name} v{found.Version} [{found.Framework}]");

                    var outFileName = $"api_{found.Name}_v{found.Version}_{found.Framework}.txt";
                    outFileName = Path.GetFullPath(outFileName);
                    using var outFile = new StreamWriter(outFileName, false, Encoding.UTF8);
                    Reader.Read(found, outFile);
                }
            }

            Console.WriteLine("Done.");
        }
    }
}