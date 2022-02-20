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

Task("Statiq-Preview")
    .Does<BuildData>((context, buildData) =>
{
    var settings = new DotNetRunSettings {
        Configuration = configuration
    };

    DotNetRun(projectPath, new ProcessArgumentBuilder().Append(string.Format("preview --output \"{0}\"", buildData.OutputDirectory)), settings);
});

Task("Statiq-Build")
    .Does<BuildData>((context, buildData) =>
{
    var settings = new DotNetRunSettings {
        Configuration = configuration
    };

    DotNetRun(projectPath, new ProcessArgumentBuilder().Append(string.Format("--output \"{0}\"", buildData.OutputDirectory)), settings);
});

Task("Statiq-LinkValidation")
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
