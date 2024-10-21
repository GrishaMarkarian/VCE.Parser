
using HtmlAgilityPack;
using System;
using VCE.Parser.Enum;
using VCE.Parser.Helper;
using VCE.Parser.Models;

namespace VCE.Parser.Parser;

public class AnalogueParser
{
    private readonly HttpClientHelper _httpClientHelper;
    private HttpClient _httpClient;
    public AnalogueParser()
    {
        _httpClientHelper = new HttpClientHelper();
        _httpClient = _httpClientHelper.CreateHttpClient(Chapter.ChapterSite);
    }

    public async Task GetRequestAsync(Part part, string url)
    {
        string href = CheckAnalogue(part.HtmlDocument);
        if (href != null)
        {
            var response = await _httpClient.GetAsync(href);
            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();

            part.HtmlDocument.LoadHtml(html);

            part.AnalogueParts = ParseAnalogues(part.HtmlDocument);
        }
        else
        {
            part.AnalogueParts = ParseDetailAnalogues(part.HtmlDocument);
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
            var productNodes = containerNode.SelectNodes(".//div[contains(@class, 'single-product')]");

            if (productNodes != null)
            {
                foreach (var productNode in productNodes)
                {
                    var articleNode = productNode.SelectSingleNode(".//dl[contains(@class, 'dl-horizontal')]/dt[text()='Артикул:']/following-sibling::dd");
                    var article = articleNode?.InnerText.Trim() ?? "Не указано";

                    var brandNode = productNode.SelectSingleNode(".//dl[contains(@class, 'dl-horizontal')]/dt[not(text()='Артикул:')][1]");
                    var brand = brandNode?.InnerText.Trim() ?? "Не указано";

                    analogueParts.Add(new AnaloguePart()
                    {
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
