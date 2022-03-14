using Statiq.App;
using Statiq.Web;

await Bootstrapper
	.Factory
	.CreateWeb(args)
	.RunAsync();
NetlifyRedirects.AddRedirects("https://knoxy.ca/*  https://coryknox.dev/:splat 301!");
