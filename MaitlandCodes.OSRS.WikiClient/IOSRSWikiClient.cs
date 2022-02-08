using MaitlandCodes.OSRS.WikiClient.Models;
using Refit;

namespace MaitlandCodes.OSRS.WikiClient
{
    public interface IOSRSWikiClient
    {
        [Get("/api.php?action=parse&format=json&title={title}&prop=text&text={text}")]
        Task<ParseActionResult> GetParseAction(string title, string text);
    }
}
