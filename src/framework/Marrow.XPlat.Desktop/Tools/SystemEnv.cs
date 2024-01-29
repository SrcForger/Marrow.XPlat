using System;
using System.IO;

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
    }
}