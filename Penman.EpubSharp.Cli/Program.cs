using CommandLine;
using System.Reflection;

namespace Penman.EpubSharp.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var types = LoadVerbs();

            Parser.Default.ParseArguments(args, types)
                .WithParsed(Run)
                .WithNotParsed(HandleErrors);
        }

        private static void HandleErrors(IEnumerable<Error> obj)
        {
            //do something with the errors
        }

        private static Type[] LoadVerbs()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
        }

        private static void Run(object obj)
        {
            switch (obj)
            {
                case ReplaceCoverOptions c:
                    RunReplaceCoverAndReturnExitCode(c);
                    break;
                case ReplaceStylesheetOptions o:
                    RunReplaceStylesheetAndReturnExitCode(o);
                    break;
            }
        }


        private static int RunReplaceCoverAndReturnExitCode(ReplaceCoverOptions options)
        {
            throw new NotImplementedException();
        }

        private static int RunReplaceStylesheetAndReturnExitCode(ReplaceStylesheetOptions options)
        {
            throw new NotImplementedException();
        }
    }
}