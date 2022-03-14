public static class NetlifyRedirects
{
    public static void AddRedirects(string redirectToAdd, string redirectsFile = "output/_redirects")
    {
        File.AppendAllText(redirectsFile, "\n");
        File.AppendAllText(redirectsFile, redirectToAdd);
    }
}
