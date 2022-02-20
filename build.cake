//////////////////////////////////////////////////////////////////////
// Addins
//////////////////////////////////////////////////////////////////////

#addin nuget:?package=Cake.Git&version=0.22.0
#addin nuget:?package=Cake.Gulp&version=1.0.0
#addin nuget:?package=Cake.Yarn&version=0.4.8

//////////////////////////////////////////////////////////////////////
// Scripts
//////////////////////////////////////////////////////////////////////

#load "buildData.cake"

//////////////////////////////////////////////////////////////////////
// Arguments
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// Setup
//////////////////////////////////////////////////////////////////////

Setup<BuildData>(context =>
{
    Information("Setting up BuildData...");

    var buildData = new BuildData(context);

    return buildData;
});

var projectPath = "./coryknox.dev.csproj";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("OS-test")
    .Does(() =>
{
    Information(Context.Environment.Platform.Family);
});
Task("Clean")
    .Does<BuildData>((context, buildData) =>
{
    var directoryToClean = new []{
        buildData.PublishDirectory,
        buildData.OutputDirectory,
        "./bin",
        "./obj",
        "./temp",
        "./wwwroot"
    };

    CleanDirectories(directoryToClean);
});

Task("Yarn-Install")
    .WithCriteria(() => FileExists("./package.json"), "package.json file not found in repository")
    .IsDependentOn("Clean")
    .Does(() =>
{
    if (BuildSystem.IsLocalBuild)
    {
        Information("Running yarn install...");
        Yarn.Install();
    }
    else
    {
        Information("Running yarn install --immutable...");
        Yarn.Install(settings => settings.ArgumentCustomization = args => args.Append("--immutable"));
    }
});

Task("Run-Gulp")
    .WithCriteria(() => FileExists("./gulpfile.js"), "gulpfile.js file not found in repository")
    .IsDependentOn("Yarn-Install")
    .Does(() =>
{
    Gulp.Local.Execute();
});

Task("Statiq-Preview")
    .IsDependentOn("Run-Gulp")
    .Does<BuildData>((context, buildData) =>
{
    var settings = new DotNetRunSettings {
        Configuration = configuration
    };

    DotNetRun(projectPath, new ProcessArgumentBuilder().Append(string.Format("preview --output \"{0}\"", buildData.OutputDirectory)), settings);
});

Task("Statiq-Build")
    .IsDependentOn("Run-Gulp")
    .Does<BuildData>((context, buildData) =>
{
    var settings = new DotNetRunSettings {
        Configuration = configuration
    };

    DotNetRun(projectPath, new ProcessArgumentBuilder().Append(string.Format("--output \"{0}\"", buildData.OutputDirectory)), settings);
});

Task("Statiq-LinkValidation")
    .IsDependentOn("Run-Gulp")
    .Does<BuildData>((context, buildData) =>
{
    var settings = new DotNetRunSettings {
        Configuration = configuration,
        ArgumentCustomization = args => args.Append("-a ValidateRelativeLinks=Error -a ValidateAbsoluteLinks=Error")
    };

    DotNetRun(projectPath, new ProcessArgumentBuilder().Append(string.Format("--output \"{0}\"", buildData.OutputDirectory)), settings);
});

Task("Default")
    .IsDependentOn("Statiq-Preview");

RunTarget(target);
