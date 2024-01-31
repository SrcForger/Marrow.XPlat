using System;
using System.IO;
using System.Linq;
using static System.Environment;

namespace Marrow.ApiReader
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var userPath = GetFolderPath(SpecialFolder.UserProfile);
            Console.WriteLine($"User home  = {userPath}");

            var nugetPath = Path.Combine(userPath, ".nuget", "packages");
            Console.WriteLine($"NuGet path = {nugetPath}");

            var sep = Path.DirectorySeparatorChar;
            var dllNames = new string[] { "Microsoft.Maui.Essentials" };
            var frameNames = new string[] { "net8.0" };
            frameNames = frameNames.Select(fn => sep + fn + sep).ToArray();

            var files = Directory.EnumerateFiles(nugetPath, "*.dll", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                if (!dllNames.Any(file.Contains))
                    continue;
                if (!frameNames.Any(file.Contains))
                    continue;

                var shortPath = file[nugetPath.Length..].Trim(sep);
                var shortTmp = shortPath.Split(sep);
                var pkgName = shortTmp[0];
                var pkgVer = new Version(shortTmp[1]);
                var fwName = shortTmp[3];
                var dllName = Path.GetFileNameWithoutExtension(shortTmp[4]);

                var found = new FoundDll(pkgName, pkgVer, fwName, dllName, file);
                Console.WriteLine($" * {found.Name} v{found.Version} [{found.Framework}]");

                Reader.Read(found);
            }

            Console.WriteLine("Done.");
        }
    }
}