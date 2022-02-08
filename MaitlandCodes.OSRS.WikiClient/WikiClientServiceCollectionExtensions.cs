using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MaitlandCodes.OSRS.WikiClient.Tests")]
namespace MaitlandCodes.OSRS.WikiClient
{
    public static class WikiClientServiceCollectionExtensions
    {
        public static IServiceCollection AddOSRSWikiClients(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddHttpClient<OSRSStoreLocationClient>(http => http.BaseAddress = new Uri("https://oldschool.runescape.wiki"));
            serviceDescriptors.AddTransient<HtmlTableParser>();

            return serviceDescriptors;
        }
    }
}
