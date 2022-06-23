using System;
using System.IO;
using static System.Reflection.Assembly;

namespace Penman.EpubSharp.Tests
{
    public static class Cwd
    {
        public static string Combine(string relativePath)
        {

            return Path.Combine(AssemblyDirectory, relativePath);
        }

        [Obsolete("Obsolete")]
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
