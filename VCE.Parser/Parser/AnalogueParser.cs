using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using VCE.Parser.Enum;
using VCE.Parser.Helper;
using VCE.Parser.Models;

namespace VCE.Parser.Parser;

public class AnalogueParser
{
    private readonly HttpClientHelper _httpClientHelper;
    private HttpClient _httpClient;
    private IWebDriver _driver;
    public AnalogueParser()
    {
        _driver = new ChromeDriver();
        _httpClientHelper = new HttpClientHelper();
        _httpClient = _httpClientHelper.CreateHttpClient(Chapter.ChapterSite);
    }

    public async Task GetRequestAsync(Part part, string url)
    {
        Console.WriteLine(url);
        string href = CheckAnalogue(part.HtmlDocument);
        if (!string.IsNullOrEmpty(href))
        {
            var response = await _httpClient.GetAsync(href);
            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();

            part.HtmlDocument.LoadHtml(html);

            part.AnalogueParts = ParseAnalogues(href);

            
        }
        else
        {
            part.AnalogueParts = ParseDetailAnalogues(part.HtmlDocument);
        }

        Console.WriteLine("");
    }

    public List<AnaloguePart> ParseAnalogues(string url)
    {
        Console.WriteLine("ParseAnalogues");
        var analogueParts = new List<AnaloguePart>();

        _driver.Navigate().GoToUrl(url);

        // Locate product nodes
        var productNodes = _driver.FindElements(By.XPath("//div[contains(@class, 'shop-product-area')]//div[contains(@class, 'single-product')]"));

        foreach (var node in productNodes)
        {
            var analoguePart = new AnaloguePart();

            try
            {
                // Locate article node
                var articleNode = node.FindElement(By.XPath(".//dl[contains(@class, 'dl-horizontal')]/dt[text()='Артикул:']/following-sibling::dd"));
                analoguePart.Name = articleNode.Text.Trim();
            }
            catch (NoSuchElementException)
            {
                analoguePart.Name = "Не указано"; // Default if not found
            }

            try
            {
                // Locate manufacturer node
                var manufacturerNode = node.FindElement(By.XPath(".//dl[contains(@class, 'dl-horizontal')]/dt[not(text()='Артикул:')][1]"));
                analoguePart.Manufacturer = manufacturerNode.Text.Trim();
            }
            catch (NoSuchElementException)
            {
                analoguePart.Manufacturer = "Не указано"; // Default if not found
            }

            analogueParts.Add(analoguePart);
        }

        Console.WriteLine(analogueParts.Count);
        return analogueParts;
    }


    private List<AnaloguePart> ParseDetailAnalogues(HtmlDocument htmlDocument)
    {
        Console.WriteLine("ParseDetailAnalogues");

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
                return dataHlk + "?sort=rating&views=grid";
            }
        }

        return null;

    }
}
