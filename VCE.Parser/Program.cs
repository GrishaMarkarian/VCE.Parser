using System.Threading.Tasks;
using System.Xml.Linq;
using VCE.Parser.Models;
using VCE.Parser.Parser;

Elements elements = new Elements();

var parallelOptions = new ParallelOptions
{
    MaxDegreeOfParallelism = 9
};

await Parallel.ForEachAsync(elements.GetElementList(), parallelOptions, async (element, token) =>
{
    try
    {
        CommonParser commonParser = new CommonParser();
        await commonParser.Pars(element.Name, element.Link, element.Number);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        File.AppendAllText("C:\\Users\\Григорий\\Source\\Repos\\VCE.Parser\\VCE.Parser\\Data\\logs.txt", ex.ToString() + Environment.NewLine);
    }
});