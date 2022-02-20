public class BuildData
{
    public DirectoryPath PublishDirectory { get; set; }
    public DirectoryPath OutputDirectory { get; set; }
    public string ProjectPath { get; set; }

    public BuildData(ICakeContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        PublishDirectory = context.MakeAbsolute(context.Directory("publish"));
        OutputDirectory = context.MakeAbsolute(context.Directory("output"));
        ProjectPath = context.EnvironmentVariable("STATIQ_PROJECT_PATH");
    }
}
