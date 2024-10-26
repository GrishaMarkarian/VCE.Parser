
using System.Text.Json;
using HtmlAgilityPack;

namespace VCE.Parser.Models;

public class Part
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Price { get; set; }
    public List<Cars> Cars { get; set; }
    public List<Complectation>? Complectations { get; set; } = new List<Complectation>();
    public List<AnaloguePart>? AnalogueParts { get; set; } = new List<AnaloguePart>();
    public HtmlDocument HtmlDocument { get; set; }
    public string AnaloguePartsString { get; set; }
    public string CarsString { get; set; }


    public void SetComplectationsParts()
    {
        if (Complectations.Count == 0)
        {
            CarsString = string.Empty;
        }
        else
        {
            CarsString = JsonSerializer.Serialize(Complectations);
        }
    }


    public void SetAnalogueParts()
    {
        if (AnalogueParts.Count == 0)
        {
            AnaloguePartsString = string.Empty;
        }
        else
        {
            AnaloguePartsString = JsonSerializer.Serialize(AnalogueParts);
        }
    }

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