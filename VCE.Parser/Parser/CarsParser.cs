using HtmlAgilityPack;
using System;
using System.Text.RegularExpressions;
using VCE.Parser.Enum;
using VCE.Parser.Helper;
using VCE.Parser.Models;

namespace VCE.Parser.Parser;

public class CarsParser
{
    private readonly HttpClientHelper _httpClientHelper;
    private HttpClient _httpClient;
    public CarsParser()
    {
        _httpClientHelper = new HttpClientHelper();
        _httpClient = _httpClientHelper.CreateHttpClient(Chapter.ChapterCars);
    }

    public async Task GetRequestAsync(Part parseParts)
    {
        _httpClient = _httpClientHelper.CreateHttpClient(Chapter.ChapterCars);
        foreach (var carParse in parseParts.Cars)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(carParse.Link);
            response.EnsureSuccessStatusCode();
            string html = await response.Content.ReadAsStringAsync();
            if (html != null)
            {
                var complecatations = ParseCars(html, carParse);
                if (complecatations != null)
                {
                    parseParts.Complectations.AddRange(complecatations);
                }
            }
        }        
    }


    private List<Complectation> ParseCars(string html, Cars cars)
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
                var years = (cells[1].InnerText.Trim()).Split("-");

                var complectation = new Complectation
                {
                    Brand = cars.Brand,
                    Model = cars.Model,
                    Body = cells[0].InnerText.Trim(),
                    YearStart= ParseDigit(years[0]),
                    YearEnd = ParseDigit(years[1]),
                    EngineCapacity = cells[2].InnerText.Trim(),
                    PowerEngine = cells[3].InnerText.Trim(),
                    CodeEngine = cells[5].InnerText.Trim()
                };

                complectations.Add(complectation);
            }
        }

        return complectations;
    }

    private string ParseDigit(string year)
    {
        return Regex.Replace(year, @"[^\d]", "");
    }

    private string ParseEngineType(string engineType)
    {
        if (engineType.Length <= 1)
        {
            return "Дизельный двигатель";
        }

        return engineType;
    }

    private string ParseBodyType(string bodyType)
    {
        return bodyType;
    }
}

