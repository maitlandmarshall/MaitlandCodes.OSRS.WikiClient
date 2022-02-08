using HtmlAgilityPack;
using System.Text;
using System.Web;

namespace MaitlandCodes.OSRS.WikiClient
{
    internal class HtmlTableParser
    {
        public IEnumerable<IDictionary<string, string>> Parse(string html)
        {
            var agility = new HtmlDocument();
            agility.LoadHtml(html);

            var headers = this.GetHeaders(agility).ToList();

            if (headers.Any() == false)
                yield break;

            // For each tr, except the first, use the headers to extract the values
            var rows = agility.DocumentNode.Descendants("tr").Skip(1).ToList();

            foreach (var row in rows)
            {
                var cells = row.Elements("td").ToList();
                var result = new Dictionary<string, string>();

                for (int i = 0; i < headers.Count; i++)
                {
                    var h = headers[i];
                    var headerCell = cells[i];

                    // Extract the raw text value
                    result[h] = HttpUtility.HtmlDecode(headerCell.InnerText).Trim();

                    // Enumerate through descendant attributes and add to dict with [key].[attributeName]
                    foreach (var d in headerCell.Descendants())
                    {
                        foreach (var a in d.Attributes)
                        {
                            var value = HttpUtility.HtmlDecode(a.Value).Trim();
                            var attributeKey = $"{h}.{a.Name}";

                            result[attributeKey] = value;
                        }
                    }
                }

                yield return result;
            }
        }

        private IEnumerable<string> GetHeaders(HtmlDocument agility)
        {
            // Get the table where the data lives in
            var tbody = agility.DocumentNode.Descendants("tbody").FirstOrDefault();

            if (tbody is null)
                yield break;

            // Enumerate the headers into keys
            var headers = tbody.Descendants("th");

            foreach (var h in headers)
            {
                var sb = new StringBuilder();

                foreach (var t in h.ChildNodes)
                {
                    if (string.IsNullOrEmpty(t.InnerText))
                    {
                        sb.Append(" ");
                    }
                    else
                    {
                        sb.Append(t.InnerText);
                    }
                }

                yield return sb.ToString();
            }
        }
    }
}
