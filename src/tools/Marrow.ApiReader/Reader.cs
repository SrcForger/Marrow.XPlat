using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

namespace Marrow.ApiReader
{
    internal static class Reader
    {
        internal static void Read(FoundDll dll)
        {
            using var ass = AssemblyDefinition.ReadAssembly(dll.Path);
            foreach (var module in ass.Modules)
            {
                foreach (var type in module.Types)
                {
                    var typeName = type.FullName;

                    if (type.IsInterface)
                    {
                        Console.WriteLine($"    + {type}");

                        foreach (var method in type.Methods)
                        {
                            var methodStr = method.ToString();
                            methodStr = methodStr.Replace($"{typeName}::", string.Empty);
                            methodStr = PatchTypes(methodStr);
                            Console.WriteLine($"       # {methodStr}");
                        }
                    }
                    else if (type.IsClass && typeName.EndsWith("Options"))
                    {
                        Console.WriteLine($"    + {type}");

                        foreach (var prop in type.Properties)
                        {
                            var propStr = prop.ToString();
                            propStr = propStr.Replace($"{typeName}::", string.Empty);
                            propStr = PatchTypes(propStr);
                            propStr = propStr.Replace("()", " { get; set; }");
                            Console.WriteLine($"       # {propStr}");
                        }
                    }
                    else if (type.IsEnum)
                    {
                        Console.WriteLine($"    + {type}");

                        foreach (var field in type.Fields)
                        {
                            var fieldStr = field.ToString();
                            if (fieldStr.EndsWith("__"))
                                continue;
                            fieldStr = fieldStr.Replace($"{typeName}::", string.Empty);
                            fieldStr = fieldStr.Split(' ', 2).Last();
                            var @const = field.Constant;
                            Console.WriteLine($"       # {fieldStr} = {@const}");
                        }
                    }
                }
            }
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
            { "Microsoft.Maui.Authentication.", "MMA." },
            { "Microsoft.Maui.Storage.", "MMS." },
            { "Microsoft.Maui.Media.", "MMM." },
            { "Microsoft.Maui.Networking.", "MMN." },
            { "Microsoft.Maui.Graphics.", "MMG." },
            { "Microsoft.Maui.ApplicationModel.Communication.", "MMAMC." },
            { "Microsoft.Maui.ApplicationModel.DataTransfer.", "MMAMD." },
            { "Microsoft.Maui.ApplicationModel.", "MMAM." },
            { "Microsoft.Maui.Devices.Sensors.", "MMDS." },
            { "Microsoft.Maui.Devices.", "MMD." }
        };

        private static string PatchTypes(string text)
        {
            foreach (var entry in _patchTable)
                text = text.Replace(entry.Key, entry.Value);
            return text;
        }
    }
}