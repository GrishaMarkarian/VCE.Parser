
using HtmlAgilityPack;
using System;
using VCE.Parser.Enum;
using VCE.Parser.Helper;
using VCE.Parser.Models;

namespace VCE.Parser.Parser;

public class AnalogueParser
{
    private readonly HttpClientHelper _httpClientHelper;
    private readonly HttpClient _httpClient;
    public AnalogueParser()
    {
        _httpClientHelper = new HttpClientHelper();
        _httpClient = _httpClientHelper.CreateHttpClient(Chapter.ChapterSite);
    }

    public async Task GetRequestAsync(Part part, string url)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string html = await response.Content.ReadAsStringAsync();

        HtmlDocument htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        string href = CheckAnalogue(htmlDocument);
        if (href != null)
        {
            response = await _httpClient.GetAsync(href);
            response.EnsureSuccessStatusCode();
            html = await response.Content.ReadAsStringAsync();

            htmlDocument.LoadHtml(html);

            part.AnalogueParts = ParseAnalogues(htmlDocument);
        }
        else
        {
            part.AnalogueParts = ParseDetailAnalogues(htmlDocument);
        }
    }

    private List<AnaloguePart> ParseAnalogues(HtmlDocument htmlDocument)
    {
        var analogueParts = new List<AnaloguePart>();

        var productNodes = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'shop-product-area')]//div[contains(@class, 'single-product')]");

        if (productNodes != null)
        {
            foreach (var node in productNodes)
            {
                var analoguePart = new AnaloguePart();

                var productNameNode = node.SelectSingleNode(".//div[contains(@class, 'product-name')]/a");
                if (productNameNode != null)
                {
                    analoguePart.Title = productNameNode.InnerText.Trim();
                }

                var articleNode = node.SelectSingleNode(".//dl[contains(@class, 'dl-horizontal')]/dt[text()='Артикул:']/following-sibling::dd");
                if (articleNode != null)
                {
                    analoguePart.Name = articleNode.InnerText.Trim();
                }

                var manufacturerNode = node.SelectSingleNode(".//dl[contains(@class, 'dl-horizontal')]/dt[not(text()='Артикул:')][1]");
                if (manufacturerNode != null)
                {
                    analoguePart.Manufacturer = manufacturerNode.InnerText.Trim();
                }

                analogueParts.Add(analoguePart);
            }
        }

        return analogueParts;
    }


    private List<AnaloguePart> ParseDetailAnalogues(HtmlDocument htmlDocument)
    {
        var analogueParts = new List<AnaloguePart>();

        var containerNode = htmlDocument.DocumentNode.SelectSingleNode("//div[contains(@class, 'container-white mt-10 container-white--analogi')]");

        if (containerNode != null)
        {
            // XPath для поиска всех продуктов внутри контейнера
            var productNodes = containerNode.SelectNodes(".//div[contains(@class, 'single-product')]");

            if (productNodes != null)
            {
                foreach (var productNode in productNodes)
                {
                    // Извлечение названия продукта
                    var productNameNode = productNode.SelectSingleNode(".//div[contains(@class, 'product-name')]/a");
                    var productName = productNameNode?.InnerText.Trim() ?? "Не указано";

                    // Извлечение артикула
                    var articleNode = productNode.SelectSingleNode(".//dl[contains(@class, 'dl-horizontal')]/dt[text()='Артикул:']/following-sibling::dd");
                    var article = articleNode?.InnerText.Trim() ?? "Не указано";

                    // Извлечение бренда
                    var brandNode = productNode.SelectSingleNode(".//dl[contains(@class, 'dl-horizontal')]/dt[text()='PROFIT']/following-sibling::dd");
                    var brand = brandNode?.InnerText.Trim() ?? "Не указано";

                    analogueParts.Add(new AnaloguePart()
                    {
                        Title = productName,
                        Manufacturer = brand,
                        Name = article
                    });
                }
            }
        }

        return analogueParts;
    }


    private string? CheckAnalogue(HtmlDocument htmlDocument)
    {
        var xpath = "//span[@class='center-block load-product btn btn-primary hlk mb20']";
        var node = htmlDocument.DocumentNode.SelectSingleNode(xpath);

        if (node != null)
        {
            var dataHlk = node.GetAttributeValue("data-hlk", null);
            if (dataHlk != null)
            {
                return dataHlk;
            }
        }

        return null;

    }
}
