
namespace VCE.Parser.Models;

public class Elements
{
    public string Name { get; set; }
    public string Link { get; set; }

    public List<Elements> GetElementList()
    {
        var elements = new List<Elements>()
        {
            new Elements() { Name = "Наконечник рулевой тяги", Link = "https://carsun.com.ua/nakonechnik_rulevoi_tyagi" }
        };

        return elements;
    }
}
