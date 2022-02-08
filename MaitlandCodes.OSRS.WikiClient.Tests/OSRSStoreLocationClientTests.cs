using Microsoft.VisualStudio.TestTools.UnitTesting;
using MaitlandCodes.OSRS.WikiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace MaitlandCodes.OSRS.WikiClient.Tests
{
    [TestClass()]
    public class OSRSStoreLocationClientTests
    {
        [TestMethod()]
        public async Task GetStoreLocationsTest()
        {
            var storeLocationClient = new OSRSStoreLocationClient(new HttpClient
            {
                BaseAddress = new Uri("https://oldschool.runescape.wiki")
            });

            var storeLocations = await storeLocationClient.GetStoreLocations("Iron Arrow");
        }
    }
}