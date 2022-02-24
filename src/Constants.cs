namespace coryknox.dev
{
    public class Constants
    {
        public const string SiteTitle = "Cory Knox";
        public const string SiteUri = "https://coryknox.dev";
        public const string BlogTitle = "Ramblings? Perhaps!";
        public const string BlogPath = "blog";
        public const string ResumeUri = "https://resume.coryknox.dev";
        public const int PostsPerPage = 10;
        public static readonly Navigation[] Navigation = {
            new Navigation{
                Title = "Blog",
                Link = $"/{BlogPath}",
                ToolTip = BlogTitle
            },
            new Navigation{
                Title = "Resume",
                Link = "https://resume.coryknox.dev",
                ToolTip = "Resume"
            },
            new Navigation{
                Title = "About Me",
                Link = "/about",
                ToolTip = "All about me..."
            }
        };
    }
}
