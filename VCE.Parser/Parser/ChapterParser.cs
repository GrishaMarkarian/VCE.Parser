using HtmlAgilityPack;
using System;
using VCE.Parser.Enum;
using VCE.Parser.Helper;

namespace VCE.Parser.Parser;

public class ChapterParser
{
    private readonly HttpClientHelper _httpClientHelper;
    private readonly HttpClient _httpClient;
    public ChapterParser()
    {
        _httpClientHelper = new HttpClientHelper();
        _httpClient = _httpClientHelper.CreateHttpClient(Chapter.ChapterSite);
    }

    public async Task<List<string>?>? GetRequestAsync(string url)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string html = await response.Content.ReadAsStringAsync();
        if (html != null)
        {
            return ParseAds(html);
        }

        return null;
    }

    private List<string> ParseAds(string html)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        List<string> hrefLinks = new List<string>();
        var productDivs = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'product-name')]");

        if (productDivs != null)
        {
            foreach (var div in productDivs)
            {
                var linkNode = div.SelectSingleNode(".//a");
                if (linkNode != null)
                {
                    string href = linkNode.GetAttributeValue("href", string.Empty);
                    hrefLinks.Add(href);
                }
            }
        }

        return hrefLinks;
    }

    public async Task<int> GetCountPage(string url)
    {
        int countPage = 0;

        HttpResponseMessage response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string html = await response.Content.ReadAsStringAsync();

        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);

        var spanNode = doc.DocumentNode.SelectSingleNode("//span[@class='rows-total']");

        if (spanNode != null)
        {
            string spanText = spanNode.InnerText;
            countPage = Convert.ToInt32(spanText.Split(' ').Last()) / 30;
        }

        return countPage;
    }
}
