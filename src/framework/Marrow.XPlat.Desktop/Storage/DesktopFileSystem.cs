using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Marrow.XPlat.Storage
{
    public sealed class DesktopFileSystem : IFileSystem
    {
        public string CacheDirectory => "!!?"; // TODO

        public string AppDataDirectory => GetAllEnv();








        private static string GetAllEnv()
        {
            var dict = new Dictionary<string, string>();
            var targets = Enum.GetValues<EnvironmentVariableTarget>();
            foreach (var evt in targets)
            {
                foreach (var item in Environment.GetEnvironmentVariables(evt).Cast<DictionaryEntry>())
                {
                    var key = $"{evt}_{item.Key}";
                    var val = item.Value?.ToString();
                    if (string.IsNullOrWhiteSpace(val))
                        continue;
                    dict[key] = val;
                }
            }
            var values = Enum.GetValues<Environment.SpecialFolder>();
            foreach (var value in values)
            {
                var key = value.ToString();
                var folder = Environment.GetFolderPath(value)?.Trim();
                if (string.IsNullOrWhiteSpace(folder))
                    continue;
                dict[key] = folder;
            }
            var fin = dict.GroupBy(d => d.Value).Select(d =>
                (Key: d.Key, Value: string.Join(" | ", d.Select(i => i.Key).Order()))
            );
            var args = fin.Select(i => $"{i.Value} = {i.Key}").Order();
            var text = string.Join(Environment.NewLine, args);
            return text;
        }
    }
}