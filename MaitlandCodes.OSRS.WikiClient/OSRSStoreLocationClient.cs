using HtmlAgilityPack;
using MaitlandCodes.OSRS.WikiClient.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaitlandCodes.OSRS.WikiClient
{
    public class OSRSStoreLocationClient
    {
        private readonly IOSRSWikiClient wikiClient;
        private readonly HtmlTableParser htmlTableParser;

        public OSRSStoreLocationClient(HttpClient httpClient)
        {
            this.wikiClient = RestService.For<IOSRSWikiClient>(httpClient);
            this.htmlTableParser = new HtmlTableParser();
        }

        public async Task<IEnumerable<StoreLocation>> GetStoreLocations(string item)
        {
            var parseResult = await this.wikiClient.GetParseAction(item, $"{{{{Store locations list|{item}}}}}");
            var html = parseResult.Parse.Text.Html;
            var storeLocations = this.ParseStoreLocations(html).ToList();

            return storeLocations;
        }

        private IEnumerable<StoreLocation> ParseStoreLocations(string html)
        {
            var tableDict = this.htmlTableParser.Parse(html);

            foreach (var t in tableDict)
            {
                int numberInStock;
                int priceSoldAt;
                int priceBoughtAt;

                bool isMembers = t["Members?.alt"]== "Member icon.png";

                int.TryParse(t["Number in stock"], NumberStyles.Any, CultureInfo.InvariantCulture, out numberInStock);
                int.TryParse(t["Price sold at"], NumberStyles.Any, CultureInfo.InvariantCulture, out priceSoldAt);
                int.TryParse(t["Price bought at"], NumberStyles.Any, CultureInfo.InvariantCulture, out priceBoughtAt);

                var storeLocation = new StoreLocation
                {
                    Seller = new UriWithTitle
                    {
                        Title = t["Seller"],
                        Uri = t["Seller.href"]
                    },
                    Location = new UriWithTitle
                    {
                        Title = t["Location"],
                        Uri = t["Location.href"]
                    },
                    Stock = numberInStock,
                    BuyPrice = priceBoughtAt,
                    SellPrice = priceSoldAt,
                    IsMembers = isMembers,
                    RestockTime = t["Restock time"]
                };

                yield return storeLocation;
            }
        }
    }
}
