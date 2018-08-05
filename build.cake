#tool "nuget:?package=xunit.runner.console"


var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var artifactsDir = "./artifacts/";
var solutionPath = "./src/RodriBus.Utils.sln";

Task("Clean")
    .Does(() => {
        if (DirectoryExists(artifactsDir))
        {
            DeleteDirectory(
                artifactsDir, 
                new DeleteDirectorySettings {
                    Recursive = true,
                    Force = true
                }
            );
        }
        CreateDirectory(artifactsDir);
        DotNetCoreClean(solutionPath);
    });


Task("Restore")
    .Does(() => {
        DotNetCoreRestore(solutionPath);
    });



Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() => {
        DotNetCoreBuild(
            solutionPath,
            new DotNetCoreBuildSettings 
            {
                Configuration = configuration
            }
        );
    });



Task("Test")
    .Does(() => {
        var projects = GetFiles("./src/**/*.Tests.csproj");
        foreach(var project in projects)
        {
            var filename = project.GetFilenameWithoutExtension();
            DotNetCoreTool(project,
                "xunit", $"-nobuild -noshadow -xml \"{filename}.report.xml\" -configuration {configuration}");
        }
        var reports = GetFiles("./src/**/*.report.xml");
        MoveFiles(reports, artifactsDir);
    });



Task("Package")
    .Does(() => {
        var settings = new DotNetCorePackSettings
        {
            OutputDirectory = artifactsDir,
            Configuration = configuration,
            NoBuild = true
        };
        DotNetCorePack(solutionPath, settings);
    });



Task("BuildAndTest")
    .IsDependentOn("Build")
    .IsDependentOn("Test");

Task("BuildAndPack")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Package");

Task("Default")
    .IsDependentOn("BuildAndTest");

RunTarget(target);
