
namespace VCE.Parser.Models;

public class Cars
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Link { get; set; }
    public List<Complectation>? Complectations { get; set; }

    public override string ToString()
    {
        string complectationInfo = Complectations != null && Complectations.Count > 0
            ? string.Join("\n", Complectations.Select(c => c.ToString()))
            : "No Complectations available";

        return $"Brand: {Brand}\n" +
               $"Model: {Model}\n" +
               $"Link: {Link}\n" +
               $"Complectations:\n{complectationInfo}\n";
    }
}
