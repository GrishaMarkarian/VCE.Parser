
using System.Text.RegularExpressions;

namespace VCE.Parser.Models;

public class Complectation
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Body { get; set; }
    public string YearStart { get; set; }
    public string YearEnd { get; set; }
    public string EngineCapacity { get; set; }
    public string PowerEngine { get; set; }
    public string TypeEngine { get; set; }
    public string CodeEngine { get; set; }

    public override string ToString()
    {
        return Regex.Replace($"{Brand}{Model}{Body},{YearStart}-{YearEnd},{EngineCapacity},{PowerEngine},{TypeEngine},{CodeEngine}"
            .Replace("\n", " ")
            .Replace("\r", " ")
            .Trim(), @"\s{2,}", " ");
    }
}
