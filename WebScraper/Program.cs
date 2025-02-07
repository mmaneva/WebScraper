using HtmlAgilityPack;
public class WebScraper
{
    public static async Task Main(string[] args)
    {
        var url = "https://en.wikipedia.org/wiki/OpenAI";
        var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(url);

       

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        var divs = htmlDocument.DocumentNode.Descendants("div")
            .Where(node => node.GetAttributeValue("class", "")
            .Contains("mw-parser-output")).ToList();

        foreach ( var div in divs)
        {
            var paragraphs = div.Descendants("p").Where(p => !string.IsNullOrWhiteSpace(p.InnerText)).ToList();
            if(paragraphs.Count > 0)
            {
                Console.WriteLine(paragraphs[0].InnerText.Trim());
                break;
            }
                
            //Console.WriteLine(div.InnerText.Trim());
        }
    }
}