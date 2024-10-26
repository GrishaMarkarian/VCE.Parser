using System.Threading.Tasks;
using System.Xml.Linq;
using VCE.Parser.Models;
using VCE.Parser.Parser;

Elements elements = new Elements();

await Parallel.ForEachAsync(elements.GetElementList(), async (element, token) =>
{
    try
    {
        CommonParser commonParser = new CommonParser();
        await commonParser.Pars(element.Name, element.Link);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        File.AppendAllText("C:\\Users\\Григорий\\Source\\Repos\\VCE.Parser\\VCE.Parser\\Data\\logs.txt", ex.ToString() + Environment.NewLine);
    }
});