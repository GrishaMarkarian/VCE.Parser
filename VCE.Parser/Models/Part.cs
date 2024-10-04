
namespace VCE.Parser.Models;

public class Part
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Price { get; set; }
    public string AllCharacteristic { get; set; }
    public List<Cars> Cars { get; set; }
    public List<AnaloguePart> AnalogueParts { get; set; }

    public override string ToString()
    {
        string carsInfo = Cars != null && Cars.Count > 0
            ? string.Join("\n", Cars.Select(car => car.ToString()))
            : "No Cars available";

        string analoguePart = AnalogueParts != null && AnalogueParts.Count > 0
            ? string.Join("\n", AnalogueParts.Select(part => part.ToString()))
            : "No Part available";

        return $"Title: {Title}\n" +
               $"Name: {Name}\n" +
               $"Manufacturer: {Manufacturer}\n" +
               $"Price: {Price}\n" +
               $"Cars:\n{carsInfo}\n" +
               $"Analogue:\n{analoguePart}\n";
        ;
    }
}