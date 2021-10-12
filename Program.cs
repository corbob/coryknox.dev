using System.Threading.Tasks;
using Statiq.Web;
using Statiq.App;

namespace coryknox.dev
{
	public class Program
	{
		public static async Task<int> Main(string[] args) =>
			await Bootstrapper
				.Factory
				.CreateWeb(args)
				.RunAsync();
	}
}
