namespace MaitlandCodes.OSRS.WikiClient.Models
{
    public class StoreLocation
    {
        public string Item { get; set; }

        public UriWithTitle Seller { get; set; }
        public UriWithTitle Location { get; set; }

        public int Stock { get; set; }
        public string RestockTime { get; set; }

        public int SellPrice { get; set; }
        public int BuyPrice { get; set; }

        public bool IsMembers { get; set; }
    }
}