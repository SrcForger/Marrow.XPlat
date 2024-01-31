using System;

namespace Marrow.ApiReader
{
    public record FoundDll(string Package, Version Version,
        string Framework, string Name, string Path);
}