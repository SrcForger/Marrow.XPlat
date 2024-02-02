using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Marrow.XPlat.Tools
{
    public static class SystemEnv
    {
        public static bool TryGetEnvVariable(string key, out string val)
        {
            var env = Environment.GetEnvironmentVariable(key)?.Trim();
            if (!string.IsNullOrWhiteSpace(env))
            {
                val = env;
                return true;
            }
            val = string.Empty;
            return false;
        }

        public static string GetFullPath(params string[] paths)
        {
            var path = Path.Combine(paths);
            path = Path.GetFullPath(path);
            return path;
        }

        public static string CreateGetDir(params string[] paths)
        {
            var path = GetFullPath(paths);
            if (!Directory.Exists(path))
                path = Directory.CreateDirectory(path).FullName;
            return path;
        }

        public static bool TryGetEnvFile(string file, out Dictionary<string, string> dict)
        {
            dict = new Dictionary<string, string>();
            if (File.Exists(file))
            {
                var lines = File.ReadLines(file, Encoding.UTF8);
                foreach (var item in lines)
                {
                    var parts = item.Split('=', 2);
                    var key = parts[0].ToLowerInvariant();
                    var val = parts[1].Trim('"').Trim();
                    dict[key] = val;
                }
            }
            return dict.Count >= 1;
        }

        public static bool TryGetPlistFile(string file, out Dictionary<string, string> dict)
        {
            dict = new Dictionary<string, string>();
            if (File.Exists(file))
            {
                var plain = File.ReadAllText(file, Encoding.UTF8);
                var items = plain.Split("<key>");
                const string tmp = "</key>";
                foreach (var item in items)
                {
                    var parts = item.Split(tmp, 2);
                    if (parts.Length != 2)
                        continue;
                    var key = parts[0].ToLowerInvariant();
                    var val = parts[1].Trim();
                    var par = val.Split(["<string>", "</string>"], StringSplitOptions.None);
                    if (par.Length == 3)
                        val = par[1].Trim();
                    dict[key] = val;
                }
            }
            return dict.Count >= 1;
        }
    }
}