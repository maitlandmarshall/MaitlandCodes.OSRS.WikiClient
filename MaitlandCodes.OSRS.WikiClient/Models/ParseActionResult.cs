namespace MaitlandCodes.OSRS.WikiClient.Models
{
    using System.Text.Json.Serialization;

    public partial class ParseActionResult
    {
        [JsonPropertyName("parse")]
        public Parse Parse { get; set; }
    }

    public partial class Parse
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("pageid")]
        public long PageId { get; set; }

        [JsonPropertyName("text")]
        public Text Text { get; set; }
    }

    public partial class Text
    {
        [JsonPropertyName("*")]
        public string Html { get; set; }
    }
}
