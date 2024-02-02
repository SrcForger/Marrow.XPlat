using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;

namespace Marrow.ApiReader
{
    internal static class Reader
    {
        internal static void Read(FoundDll dll, StreamWriter console)
        {
            using var ass = AssemblyDefinition.ReadAssembly(dll.Path);
            foreach (var module in ass.Modules
                         .SelectMany(m => m.Types)
                         .OrderBy(m => $"{m.Namespace}:{m.Name}")
                         .GroupBy(t => t.Namespace))
            {
                if (string.IsNullOrWhiteSpace(module.Key))
                    continue;

                console.WriteLine();
                console.WriteLine($" --- {Shorten(module.Key).Trim('_', '.')} ---");
                console.WriteLine();
                foreach (var type in module.OrderBy(t => t.Name))
                {
                    var typeName = type.FullName;

                    if (type.IsInterface)
                    {
                        console.WriteLine($"    + {Shorten(type)}");

                        foreach (var method in type.Methods.OrderBy(m => $"{m.Name}{m.Parameters.Count:00}"))
                        {
                            var methodStr = method.ToString();
                            methodStr = methodStr.Replace($"{typeName}::", string.Empty);
                            methodStr = PatchTypes(methodStr);
                            console.WriteLine($"       # {methodStr}");
                        }
                    }
                    else if (type.IsClass && typeName.EndsWith("Options"))
                    {
                        console.WriteLine($"    + {Shorten(type)}");

                        foreach (var prop in type.Properties.OrderBy(p => p.Name))
                        {
                            if (prop?.GetMethod?.IsStatic ?? false)
                                continue;
                            var propStr = prop.ToString();
                            propStr = propStr.Replace($"{typeName}::", string.Empty);
                            propStr = PatchTypes(propStr);
                            propStr = propStr.Replace("()", " { get; set; }");
                            console.WriteLine($"       # {propStr}");
                        }
                    }
                    else if (type.IsEnum)
                    {
                        console.WriteLine($"    + {Shorten(type)}");

                        foreach (var field in type.Fields.OrderBy(f => f.Name))
                        {
                            var fieldStr = field.ToString();
                            if (fieldStr.EndsWith("__"))
                                continue;
                            fieldStr = fieldStr.Replace($"{typeName}::", string.Empty);
                            fieldStr = fieldStr.Split(' ', 2).Last();
                            var @const = field.Constant;
                            console.WriteLine($"       # {fieldStr} = {@const}");
                        }
                    }
                }
            }
        }

        private static string Shorten(object raw)
        {
            var text = raw.ToString() ?? string.Empty;
            text = text.Replace(Program.MauiSpace, "_.");
            text = text.Replace(Program.MyLibSpace, "_.");
            return text;
        }

        private static readonly Dictionary<string, string> _patchTable = new()
        {
            // Standard stuff
            { "/", "." },
            { "System.Void", "void" },
            { "System.Boolean", "bool" },
            { "System.Int32", "int" },
            { "System.Single", "float" },
            { "System.Double", "double" },
            { "System.String", "string" },
            { "System.Version", "Version" },
            { "System.TimeSpan", "TimeSpan" },
            { "System.Uri", "Uri" },
            { "System.EventArgs", "EventArgs" },
            { "System.IO.Stream", "Stream" },
            { "System.Threading.Tasks.Task`1", "Task" },
            { "System.Threading.Tasks.Task", "Task" },
            { "System.Threading.CancellationToken", "CancellationToken" },
            { "System.EventHandler`1", "EventHandler" },
            { "System.EventHandler", "EventHandler" },
            { "System.Nullable`1", "Nullable" },
            { "System.Collections.Generic.IEnumerable`1", "IEnumerable" },
            { "System.Collections.Generic.IDictionary`2", "IDictionary" },
            { "System.Collections.Generic.IReadOnlyList`1", "IReadOnlyList" },
            // MAUI stuff
            { $"{Program.MauiSpace}Authentication.", "MMA." },
            { $"{Program.MauiSpace}Storage.", "MMS." },
            { $"{Program.MauiSpace}Media.", "MMM." },
            { $"{Program.MauiSpace}Networking.", "MMN." },
            { $"{Program.MauiSpace}Graphics.", "MMG." },
            { $"{Program.MauiSpace}ApplicationModel.Communication.", "MMAMC." },
            { $"{Program.MauiSpace}ApplicationModel.DataTransfer.", "MMAMD." },
            { $"{Program.MauiSpace}ApplicationModel.", "MMAM." },
            { $"{Program.MauiSpace}Devices.Sensors.", "MMDS." },
            { $"{Program.MauiSpace}Devices.", "MMD." },
            // My stuff
            { $"{Program.MyLibSpace}ApplicationModel.Communication.", "MMAMC." },
            { $"{Program.MyLibSpace}Storage.", "MMS." },
            { $"{Program.MyLibSpace}Media.", "MMM." }
        };

        private static string PatchTypes(string text)
        {
            foreach (var entry in _patchTable)
                text = text.Replace(entry.Key, entry.Value);
            return text;
        }
    }
}