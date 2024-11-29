using System.Collections.Concurrent;
using System.IO;
using VCE.Parser.DB;
using VCE.Parser.Models;

namespace VCE.Parser.Parser;

public class CommonParser
{
    public object syncHandler = new object();
    private readonly Repository _repository;
    private readonly ChapterParser _chapterParser;
    private readonly PartParser _partParser;
    private readonly CarsParser _carsParser;
    private readonly AnalogueParser _analogueParser;

    public CommonParser()
    {
        _repository = new Repository();
        _chapterParser = new ChapterParser();
        _partParser = new PartParser();
        _carsParser = new CarsParser();
        _analogueParser = new AnalogueParser();
    }


    public async Task Pars(string category, string url, int pages)
    {
        int countPage = await _chapterParser.GetCountPage(url);
        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = 30
        };

        Dictionary<string, string> adsLink = new Dictionary<string, string>();
        ConcurrentBag<Part> concurrentParts = new ConcurrentBag<Part>();

        for (int i = pages; i <= countPage; i++)
        {
            string newUrl = $"{url}?page={i}";
            var ads = (await _chapterParser.GetRequestAsync(newUrl));
            var newAds = new List<string>();
            if (ads != null)
            {
                foreach (var ad in ads)
                {
                    if (!adsLink.ContainsKey(ad))
                    {
                        adsLink.Add(ad, "1");
                        newAds.Add(ad);
                    }
                }

                int counter = 0;
                foreach (var ad in newAds)
                {
                    try
                    {
                        var parts = await _partParser.GetRequestAsync(ad);

                        parts.Type = category;

                        await _carsParser.GetRequestAsync(parts);

                        await _analogueParser.GetRequestAsync(parts, ad);

                        concurrentParts.Add(parts);

                        counter++;

                        if (counter == 10)
                        {
                            counter = 0;
                            await SavePartAsync(concurrentParts.ToList());
                            concurrentParts.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                }

            }

            File.AppendAllText("C:\\Users\\Григорий\\Source\\Repos\\VCE.Parser\\VCE.Parser\\Data\\logs.txt", $"Category: {category} Page - {i}/{countPage}" + Environment.NewLine);
            Console.WriteLine($"Category: {category} Page - {i}/{countPage}");

        }
    }

    private async Task SavePartAsync(List<Part> parts)
    {
        var newParts = new List<Part>();

        foreach (var part in parts)
        {
            if (part != null && IsValidPart(part))
            {
                part.SetAnalogueParts();
                part.SetComplectationsParts();


                newParts.Add(part);
            }
        }

        if (newParts.Any())
        {
            await _repository.SavePartsAsync(newParts);
        }
    }

    private bool IsValidPart(Part part)
    {
        part.Title = part.Title ?? "Default Title";
        part.Type = part.Type ?? "Default Type";
        part.Name = part.Name ?? "Default Name";
        part.Manufacturer = part.Manufacturer ?? "Default Manufacturer";
        part.Price = part.Price ?? "0";
        part.AnalogueParts = part.AnalogueParts ?? new List<AnaloguePart>();
        part.Cars = part.Cars ?? new List<Cars>();

        return !string.IsNullOrEmpty(part.Title) &&
               !string.IsNullOrEmpty(part.Type) &&
               !string.IsNullOrEmpty(part.Name) &&
               !string.IsNullOrEmpty(part.Manufacturer) &&
               !string.IsNullOrEmpty(part.Price);
    }
}
