using System.IO;
using System.Reflection;

namespace EpubCore.Tests
{
    public static class Cwd
    {
        public static string Combine(string relativePath)
        {

            return Path.Combine(AssemblyDirectory, relativePath);
        }

        public static string AssemblyDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
