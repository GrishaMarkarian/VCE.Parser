
namespace VCE.Parser.Models;

public class AnaloguePart
{
    public string Manufacturer { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"Name: {Name} Manufacturer: {Manufacturer}";
    }
}
