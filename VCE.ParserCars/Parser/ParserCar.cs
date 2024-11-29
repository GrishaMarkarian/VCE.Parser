using HtmlAgilityPack;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VCE.ParserCars.Data;
using VCE.ParserCars.Models;

namespace VCE.ParserCars.Parser;

public class ParserCar
{
    private SQLRepository _sqlRepository;
    private CarsBrandData carsBrandData;
    private HttpClient _httpClient;

    public ParserCar()
    {
        _sqlRepository = new SQLRepository();
        carsBrandData = new CarsBrandData();
        _httpClient = new HttpClient();
        HttpConfig();
    }

    public void HttpConfig()
    {
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Mobile Safari/537.36");
        _httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
    }

    public async Task ParseCarRequest()
    {
        ParallelOptions parallelOptions = new()
        {
            MaxDegreeOfParallelism = 10
        };

        for (int year = 2013; year <= 2024; year++)
        {
            var carsList = new ConcurrentBag<Cars>();
            var carsBrands = carsBrandData.GetCarsBrandData();

            await Parallel.ForEachAsync(carsBrands, parallelOptions, async (carBrand, token) =>
            {
                var url = $"https://vce.com.ua/ajax/select_modif?year={year}&marka={carBrand.CodeBrand}&type_marka=&lang=";

                var html = await GetHttpRequest(url);

                var models = ParseModel(html);
                foreach (var model in models)
                {
                    url = $"https://vce.com.ua/ajax/select_modif?year={year}&marka={carBrand.CodeBrand}&model={model.Link}&type_marka=&lang=";
                    html = await GetHttpRequest(url);
                    var bodyTypes = ParseModelBodyType(html);
                    if (bodyTypes != null)
                    {
                        foreach (var bodyType in bodyTypes)
                        {
                            if (bodyTypes.Count > 1)
                            {
                                url = $"https://vce.com.ua/ajax/select_modif?year={year}&marka={carBrand.CodeBrand}&model={model.Link}&body={bodyType.Link}&type_marka=&lang=";
                                html = await GetHttpRequest(url);
                                var carEngines = ParseCarEngine(html);

                                if (carEngines != null)
                                {
                                    foreach (var carEngine in carEngines)
                                    {
                                        url = $"https://vce.com.ua/ajax/select_modif?year={year}&marka={carBrand.CodeBrand}&model={model.Link}&body={bodyType.Link}&type_marka=&lang=&litres-fuel={carEngine.Link}";
                                        html = await GetHttpRequest(url);
                                        var carModifies = ParseCarModify(html);

                                        if (carModifies != null)
                                        {
                                            foreach (var carModify in carModifies)
                                            {
                                                carsList.Add(new Cars() { Brand = carBrand.Brand, Model = model.Model, BodyType = bodyType.BodyType, EngineCapacity = carEngine.EngineCapacity, Modify = carModify.Modify, Year = year });
                                                Console.WriteLine($"Brand: {carBrand.Brand} Model: {model.Model} BodyType: {bodyType.BodyType} EngineCapacity: {carEngine.EngineCapacity} Modify: {carModify.Modify} Year {year}");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                url = $"https://vce.com.ua/ajax/select_modif?year={year}&marka={carBrand.CodeBrand}&model={model.Link}&type_marka=&lang=";
                                html = await GetHttpRequest(url);
                                var carEngines = ParseCarEngine(html);

                                if (carEngines != null)
                                {
                                    foreach (var carEngine in carEngines)
                                    {
                                        url = $"https://vce.com.ua/ajax/select_modif?year={year}&marka={carBrand.CodeBrand}&model={model.Link}&type_marka=&lang=&litres-fuel={carEngine.Link}";
                                        html = await GetHttpRequest(url);
                                        var carModifies = ParseCarModify(html);

                                        if (carModifies != null)
                                        {
                                            foreach (var carModify in carModifies)
                                            {
                                                carsList.Add(new Cars() { Brand = carBrand.Brand, Model = model.Model, BodyType = bodyType.BodyType, EngineCapacity = carEngine.EngineCapacity, Modify = carModify.Modify, Year = year });
                                                Console.WriteLine($"Brand: {carBrand.Brand} Model: {model.Model} BodyType: {bodyType.BodyType} EngineCapacity: {carEngine.EngineCapacity} Modify: {carModify.Modify} Year {year}");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });


            await _sqlRepository.SaveCarsAsync(carsList.ToList());
        }
    }

    private async Task<string> GetHttpRequest(string url)
    {
        string htmlContent = string.Empty;
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            htmlContent = await response.Content.ReadAsStringAsync();
        }

        return htmlContent;
    }

    private List<CarsModel> ParseModel(string html)
    {
        var models = new List<CarsModel>();
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var aTags = htmlDoc.DocumentNode.SelectNodes("//a[@data-name='model']");

        if (aTags != null)
        {
            foreach (var aTag in aTags)
            {
                string text = aTag.InnerText.Trim();
                string dataVal = aTag.GetAttributeValue("data-val", "");

                models.Add(new CarsModel() { Model = text, Link = dataVal });
            }
        }

        return models;
    }

    private List<CarsBodyType> ParseModelBodyType(string html)
    {
        var models = new List<CarsBodyType>();
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var aTags = htmlDoc.DocumentNode.SelectNodes("//a[@data-name='body']");
        if (aTags != null)
        {
            foreach (var aTag in aTags)
            {
                string text = aTag.InnerText.Trim();
                string dataVal = aTag.GetAttributeValue("data-val", "");

                models.Add(new CarsBodyType() { BodyType = text, Link = dataVal });
            }
        }

        return models;
    }


    private List<CarEngine> ParseCarEngine(string html)
    {
        var carEngine = new List<CarEngine>();
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var aTags = htmlDoc.DocumentNode.SelectNodes("//a[@data-name='litres-fuel']");
        if (aTags != null)
        {
            foreach (var aTag in aTags)
            {
                string text = aTag.InnerText.Trim();
                string dataVal = aTag.GetAttributeValue("data-val", "");

                carEngine.Add(new CarEngine() { EngineCapacity = text, Link = dataVal });
            }
        }

        return carEngine;
    }

    private List<CarModify> ParseCarModify(string html)
    {
        var carModify = new List<CarModify>();
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var dropdownItems = doc.DocumentNode.SelectNodes("//li[@class='dropdown open ']");

        if (dropdownItems != null)
        {
            foreach (var dropdownItem in dropdownItems)
            {
                var modelHeaderNode = dropdownItem.SelectSingleNode(".//li[@class='dropdown-header']");
                string modelHeader = modelHeaderNode != null ? modelHeaderNode.InnerText.Trim() : "Неизвестно";

                var modItems = dropdownItem.SelectNodes(".//ul[@class='dropdown-menu']/li/a");

                if (modItems != null)
                {
                    foreach (var modItem in modItems)
                    {
                        var modText = modItem.InnerText.Trim();        
                        carModify.Add(new CarModify() { Modify = CleanString(modText.Trim())});
                    }
                }
            }
        }
        

        return carModify;
    }


    string CleanString(string input)
    {
        string noNewLines = input.Replace("\n", " ");
        string noExtraSpaces = Regex.Replace(noNewLines, @"\s+", " ");
        return noExtraSpaces.Trim();
    }
}
