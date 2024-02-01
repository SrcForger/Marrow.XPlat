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
    }
}