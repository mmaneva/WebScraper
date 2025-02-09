using CsvHelper;
using HtmlAgilityPack;
using System.Globalization;

public class Product 
{
    public string Name { get; set; }
    public string Price { get; set; }
}

public class WebScraper
{
    public static async Task Main(string[] args)
    {
        var url = "";
        var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(url);

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        var products = new List<Product>();

        var productNodes = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'product-card')]");

        if (productNodes != null)
        {
            foreach (var productNode in productNodes)
            {
                try
                {
                    var nameNode = productNode.SelectSingleNode("");
                    var priceNode = productNode.SelectSingleNode("");

                    if (nameNode != null && priceNode != null)
                    {
                        var product = new Product
                        {
                            Name = nameNode.InnerText.Trim(),
                            Price = priceNode.InnerText.Trim()
                        };
                        products.Add(product);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        else
        {
            Console.WriteLine("No products found on the page.");
        }

        
    }

    public static void SaveToCsv(List<Product> products)
    {
        var filePath = "products.csv";

        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(products);
        }

        Console.WriteLine($"Data saved to {filePath}");
    }

        
}