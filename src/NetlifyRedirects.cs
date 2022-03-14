public static class NetlifyRedirects
{
    public static void AddRedirects(string redirectsToAddFile = "myRedirects", string redirectsFile = "output/_redirects")
    {
        File.AppendAllLines(redirectsFile, File.ReadAllLines(redirectsToAddFile));
    }
}
