
using System.Text.RegularExpressions;

namespace VCE.Parser.Models;

public class Complectation
{
    public string Body { get; set; }
    public string Year { get; set; }
    public string EngineCapacity { get; set; }
    public string PowerEngine { get; set; }
    public string TypeEngine { get; set; }
    public string CodeEngine { get; set; }


    public override string ToString()
    {
        return Regex.Replace($"{Body},{Year},{EngineCapacity},{PowerEngine},{TypeEngine},{CodeEngine}"
            .Replace("\n", " ")
            .Replace("\r", " ")
            .Trim(), @"\s{2,}", " ");
    }
}
