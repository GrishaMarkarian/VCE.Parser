
namespace VCE.ParserCars.Data;

public class CarsBrandData
{
    public string Brand { get; set; }
    public string CodeBrand { get; set; }

    public List<CarsBrandData> GetCarsBrandData()
    {
        var carsBrand = new List<CarsBrandData>()
        {
            new CarsBrandData() {Brand = "Acura", CodeBrand = "2548"},
            new CarsBrandData() {Brand = "Alfa Romeo", CodeBrand = "1281"},
            new CarsBrandData() {Brand = "Audi", CodeBrand = "1330"},
            new CarsBrandData() {Brand = "BMW", CodeBrand = "1407"},
            new CarsBrandData() {Brand = "Cadillac", CodeBrand = "2824"},
            new CarsBrandData() {Brand = "Chery", CodeBrand = "2451"},
            new CarsBrandData() {Brand = "Chevrolet", CodeBrand = "1278"},
            new CarsBrandData() {Brand = "Chrysler", CodeBrand = "2506"},
            new CarsBrandData() {Brand = "Citroen", CodeBrand = "1433"},
            new CarsBrandData() {Brand = "Dacia", CodeBrand = "1461"},
            new CarsBrandData() {Brand = "Daewoo", CodeBrand = "1277"},
            new CarsBrandData() {Brand = "Daihatsu", CodeBrand = "2599"},
            new CarsBrandData() {Brand = "Dodge", CodeBrand = "2553"},
            new CarsBrandData() {Brand = "Fiat", CodeBrand = "1480"},
            new CarsBrandData() {Brand = "Ford", CodeBrand = "1514"},
            new CarsBrandData() {Brand = "Geely", CodeBrand = "1548"},
            new CarsBrandData() {Brand = "Honda", CodeBrand = "1556"},
            new CarsBrandData() {Brand = "Hyundai", CodeBrand = "1580"},
            new CarsBrandData() {Brand = "Infiniti", CodeBrand = "1613"},
            new CarsBrandData() {Brand = "Isuzu", CodeBrand = "3197"},
            new CarsBrandData() {Brand = "Jaguar", CodeBrand = "2604"},
            new CarsBrandData() {Brand = "Jeep", CodeBrand = "2542"},
            new CarsBrandData() {Brand = "KIA", CodeBrand = "1626"},
            new CarsBrandData() {Brand = "Land Rover", CodeBrand = "1664"},
            new CarsBrandData() {Brand = "Lexus", CodeBrand = "1670"},
            new CarsBrandData() {Brand = "Mazda", CodeBrand = "1680"},
            new CarsBrandData() {Brand = "Mercedes", CodeBrand = "1702"},
            new CarsBrandData() {Brand = "Mini", CodeBrand = "3025"},
            new CarsBrandData() {Brand = "Mitsubishi", CodeBrand = "1737"},
            new CarsBrandData() {Brand = "Nissan", CodeBrand = "1761"},
            new CarsBrandData() {Brand = "Opel", CodeBrand = "1794"},
            new CarsBrandData() {Brand = "Peugeot", CodeBrand = "1825"},
            new CarsBrandData() {Brand = "Porsche", CodeBrand = "2600"},
            new CarsBrandData() {Brand = "Renault", CodeBrand = "1866"},
            new CarsBrandData() {Brand = "SAAB", CodeBrand = "3120"},
            new CarsBrandData() {Brand = "Seat", CodeBrand = "1928"},
            new CarsBrandData() {Brand = "Skoda", CodeBrand = "1946"},
            new CarsBrandData() {Brand = "Smart", CodeBrand = "1956"},
            new CarsBrandData() {Brand = "SsangYong", CodeBrand = "1963"},
            new CarsBrandData() {Brand = "Subaru", CodeBrand = "1971"},
            new CarsBrandData() {Brand = "Suzuki", CodeBrand = "1989"},
            new CarsBrandData() {Brand = "Toyota", CodeBrand = "2003"},
            new CarsBrandData() {Brand = "Volkswagen", CodeBrand = "2055"},
            new CarsBrandData() {Brand = "Volvo", CodeBrand = "2028"}   
        };

        return carsBrand;
    }
}
