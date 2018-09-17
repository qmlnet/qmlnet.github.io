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
        static Task Main(string[] args)
        {
            var options = ParseOptions(args);

            Target("clean", () =>
            {
                CleanDirectory(ExpandPath("./output"));
            });
            
            Target("build", DependsOn("clean"), () =>
            {
                RunShell("dotnet run --project statik-project-doc/StatikProject/StatikProject.csproj build");
            });
            
            Target("serve", () =>
            {
                RunShell("dotnet run --project statik-project-doc/StatikProject/StatikProject.csproj serve");
            });
            
            Target("default", DependsOn("serve"));

            return Run(options);
        }
    }
}