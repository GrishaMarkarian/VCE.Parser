using HtmlAgilityPack;
using VCE.Parser.Enum;
using VCE.Parser.Helper;
using VCE.Parser.Models;

namespace VCE.Parser.Parser;

public class PartParser
{
    private readonly HttpClientHelper _httpClientHelper;
    private readonly HttpClient _httpClient;
    public PartParser()
    {
        _httpClientHelper = new HttpClientHelper();
        _httpClient = _httpClientHelper.CreateHttpClient(Chapter.ChapterSite);
    }

    public async Task<Part> GetRequestAsync(string url)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string html = await response.Content.ReadAsStringAsync();
        if (html != null)
        {
            return ParseParts(html);
        }

        return null;
    }

    private Part ParseParts(string html)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        Part? part = null;
        string? title = ParseTitle(htmlDoc);
        string? manufact = ParseManufact(htmlDoc);
        string? name = ParseName(htmlDoc);
        string? allCharacteristic = ParseAllCharacteristic(htmlDoc);
        string? price = ParsePrice(htmlDoc);
        List<Cars> cars = ParseCars(htmlDoc);

        part = new Part()
        {
            Title = title,
            Manufacturer = manufact,
            Name = name,
            AllCharacteristic = allCharacteristic,
            Price = price,
            Cars = cars        
        };

        return part;
    }

    private List<Cars>? ParseCars(HtmlDocument htmlDocument)
    {
        var ddNodes = htmlDocument.DocumentNode.SelectNodes("//dd");
        var carsList = new List<Cars>();

        foreach (var dd in ddNodes)
        {
            var brandNode = dd.SelectSingleNode(".//strong");
            if (brandNode != null)
            {
                string brandName = brandNode.InnerText.TrimEnd(':');

                var modelLinks = dd.SelectNodes(".//span[@class='magnific-ajax' and @data-mfp-src]");
                if (modelLinks != null)
                {
                    foreach (var modelLink in modelLinks)
                    {
                        var modelName = modelLink.InnerText.Trim();
                        var ajaxUrl = modelLink.GetAttributeValue("data-mfp-src", string.Empty);

                        carsList.Add(new Cars
                        {
                            Brand = brandName,
                            Model = modelName,
                            Link = ajaxUrl,
                        });
                    }
                }
            }
        }

        return carsList;
    }

    private string? ParseTitle(HtmlDocument htmlDocument)
    {
        string title = null;
        var titleNode = htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='title-item']");

        if (titleNode != null)
        {
            title = titleNode.InnerText.Trim();
        }

        return title;
    }
    private string? ParseManufact(HtmlDocument htmlDocument)
    {
        string manufact = null;
        var manufacturerNode = htmlDocument.DocumentNode.SelectSingleNode("//dt[text()='Производитель']/following-sibling::dd/strong[@itemprop='brand']");

        if (manufacturerNode != null)
        {
            manufact = manufacturerNode.InnerText.Trim();
        }

        return manufact;
    }
    private string? ParseName(HtmlDocument htmlDocument)
    {
        string name = null;
        var catalogNumberNode = htmlDocument.DocumentNode.SelectSingleNode("//dt[text()='Каталожный номер']/following-sibling::dd/strong[@itemprop='mpn']");

        if (catalogNumberNode != null)
        {
            name = catalogNumberNode.InnerText.Trim();
        }

        return name;
    }
    private string? ParseAllCharacteristic(HtmlDocument htmlDocument)
    {
        string listCharacteristic = string.Empty;
        var dtNodes = htmlDocument.DocumentNode.SelectNodes("//dt");
        var ddNodes = htmlDocument.DocumentNode.SelectNodes("//dd");

        if (dtNodes != null && ddNodes != null && dtNodes.Count == ddNodes.Count)
        {
            for (int i = 0; i < dtNodes.Count; i++)
            {
                string characteristic = dtNodes[i].InnerText.Trim();

                string value = ddNodes[i].InnerText.Trim();

                listCharacteristic += ($"{characteristic}:{value}\n");
            }

            return listCharacteristic;
        }

        return null;       
    }
    private string? ParsePrice(HtmlDocument htmlDocument)
    {
        string price = null;
        var priceNode = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='price-val']");

        if (priceNode != null)
        {
            price = priceNode.InnerText.Trim();
        }

        return price;
    }
}
