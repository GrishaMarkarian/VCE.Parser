
namespace VCE.Parser.Parser;

public class CommonParser
{
    private readonly ChapterParser _chapterParser;
    private readonly PartParser _partParser;
    private readonly CarsParser _carsParser;
    private readonly AnalogueParser _analogueParser;

    public CommonParser()
    {
        _chapterParser = new ChapterParser();
        _partParser = new PartParser();
        _carsParser = new CarsParser();
        _analogueParser = new AnalogueParser();
    }


    public async Task Pars(string url)
    {
        int countPage = await _chapterParser.GetCountPage(url);
        for (int i = 0; i < countPage; i++)
        {
            string newUrl = $"{url}?page={i}";
            var ads = (await _chapterParser.GetRequestAsync(newUrl)).Take(1);
            if (ads != null)
            {
                foreach (var ad in ads)
                {
                    var parts = await _partParser.GetRequestAsync(ad);

                    await _carsParser.GetRequestAsync(parts);

                    await _analogueParser.GetRequestAsync(parts, ad);

                    Console.WriteLine(parts.ToString());
                }
            }
            
        }
    }

}
