
namespace VCE.Parser.Models;

public class AnaloguePart
{
    public string Title { get; set; }
    public string Manufacturer { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"Title: {Title} Manufacturer: {Manufacturer} Name: {Name}";
    }
}
