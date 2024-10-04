using HtmlAgilityPack;
using System;
using VCE.Parser.Enum;
using VCE.Parser.Helper;
using VCE.Parser.Models;

namespace VCE.Parser.Parser;

public class CarsParser
{
    private readonly HttpClientHelper _httpClientHelper;
    private readonly HttpClient _httpClient;
    public CarsParser()
    {
        _httpClientHelper = new HttpClientHelper();
        _httpClient = _httpClientHelper.CreateHttpClient(Chapter.ChapterCars);
    }

    public async Task GetRequestAsync(Part parseParts)
    {
        foreach (var partParse in parseParts.Cars)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(partParse.Link);
            response.EnsureSuccessStatusCode();
            string html = await response.Content.ReadAsStringAsync();
            if (html != null)
            {
                partParse.Complectations = ParseCars(html);
            }
        }        
    }


    private List<Complectation> ParseCars(string html)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var rows = htmlDoc.DocumentNode.SelectNodes("//table[@class='table table-striped']/tr");

        List<Complectation> complectations = new List<Complectation>();

        foreach (var row in rows)
        {
            var cells = row.SelectNodes("td");

            if (cells != null && cells.Count >= 6)
            {
                var complectation = new Complectation
                {
                    Body = cells[0].InnerText.Trim(),
                    Year = cells[1].InnerText.Trim(),
                    EngineCapacity = cells[2].InnerText.Trim(),
                    PowerEngine = cells[3].InnerText.Trim(),
                    TypeEngine = cells[4].InnerText.Trim(),
                    CodeEngine = cells[5].InnerText.Trim()
                };

                complectations.Add(complectation);
            }
        }

        return complectations;
    }

}
