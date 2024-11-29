using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
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
        try
        {
            string href = CheckAnalogue(part.HtmlDocument);
            if (!string.IsNullOrEmpty(href))
            {
                var response = await ExecuteHttpRequestWithRetry(href);

                if (response != null && response.IsSuccessStatusCode)
                {
                    var html = await response.Content.ReadAsStringAsync();
                    part.HtmlDocument.LoadHtml(html);
                    part.AnalogueParts = ParseAnalogues(href);
                }
                else
                {
                    Console.WriteLine("Не удалось получить успешный ответ.");
                    part.AnalogueParts = new List<AnaloguePart>(); // Пустой список, если запрос не успешен
                }
            }
            else
            {
                part.AnalogueParts = ParseDetailAnalogues(part.HtmlDocument);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при выполнении запроса: {e.Message}");
        }

    }

    private async Task<HttpResponseMessage?> ExecuteHttpRequestWithRetry(string href, int maxRetries = 3)
    {
        int retries = 0;
        while (retries < maxRetries)
        {
            try
            {
                var response = await _httpClient.GetAsync(href);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Попытка {retries + 1}: Ошибка HTTP-запроса - {e.Message}");
                if (retries == maxRetries - 1)
                {
                    Console.WriteLine("Достигнут лимит попыток.");
                    return null;
                }
                await Task.Delay(1000); // Пауза перед повтором
            }
            retries++;
        }
        return null;
    }

    public List<AnaloguePart> ParseAnalogues(string url)
    {
        var analogueParts = new List<AnaloguePart>();

        try
        {
            _driver.Navigate().GoToUrl(url);

            var productNodes = _driver.FindElements(By.XPath("//div[contains(@class, 'shop-product-area')]//div[contains(@class, 'single-product')]"));

            foreach (var node in productNodes)
            {
                var analoguePart = new AnaloguePart();

                try
                {
                    var articleNode = node.FindElement(By.XPath(".//dl[contains(@class, 'dl-horizontal')]/dt[text()='Артикул:']/following-sibling::dd"));
                    analoguePart.Name = articleNode.Text.Trim();
                }
                catch (NoSuchElementException)
                {
                    analoguePart.Name = "Не указано";
                }

                try
                {
                    var manufacturerNode = node.FindElement(By.XPath(".//dl[contains(@class, 'dl-horizontal')]/dt[not(text()='Артикул:')][1]"));
                    analoguePart.Manufacturer = manufacturerNode.Text.Trim();
                }
                catch (NoSuchElementException)
                {
                    analoguePart.Manufacturer = "Не указано";
                }

                analogueParts.Add(analoguePart);
            }
        }
        catch (WebDriverException e)
        {
            Console.WriteLine($"Ошибка Selenium: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Неизвестная ошибка при парсинге аналогов: {e.Message}");
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
                return dataHlk + "?sort=rating&views=grid";
            }
        }

        return null;

    }
}
