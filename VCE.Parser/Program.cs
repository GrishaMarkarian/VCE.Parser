using VCE.Parser.Models;
using VCE.Parser.Parser;

Elements elements = new Elements();
foreach (var element in elements.GetElementList())
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
}