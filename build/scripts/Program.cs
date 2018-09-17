using System;
using System.Threading.Tasks;
using Build.Buildary;
using static Bullseye.Targets;
using static Build.Buildary.Runner;
using static Build.Buildary.Directory;
using static Build.Buildary.Path;
using static Build.Buildary.Shell;

namespace Build
{
    static class Program
    {
        static Task<int> Main(string[] args)
        {
            var options = ParseOptions(args);

            Add("clean", () =>
            {
                CleanDirectory(ExpandPath("./output"));
            });
            
            Add("build", DependsOn("clean"), () =>
            {
                RunShell("dotnet build Bulwark.sln");
            });
            
            Add("serve", () =>
            {
                RunShell("dotnet test test/Bulwark.Tests/");
            });
            
            Add("default", DependsOn("serve"));

            return Run(options);
        }
    }
}