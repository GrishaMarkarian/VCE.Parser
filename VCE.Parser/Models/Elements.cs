
namespace VCE.Parser.Models;

public class Elements
{
    public string Name { get; set; }
    public string Link { get; set; }

    public List<Elements> GetElementList()
    {
        //var elements = new List<Elements>()
        //{
        //    new Elements() { Name = "Наконечник рулевой тяги", Link = "https://carsun.com.ua/nakonechnik_rulevoi_tyagi" },
        //    new Elements() { Name = "Рулевая тяга", Link = "https://carsun.com.ua/rulevaia_tyaga" },
        //    new Elements() { Name = "Насос гидроусилителя руля", Link = "https://carsun.com.ua/nasos_gidrousilitelya_rulya" },
        //    new Elements() { Name = "Рулевая рейка", Link = "https://carsun.com.ua/rulevaja_rejka" },
        //    new Elements() { Name = "Пыльник рулевой рейки", Link = "https://carsun.com.ua/pilnik-rulevoi-reiki" },
        //    new Elements() { Name = "Ремкомплект рулевой рейки", Link = "https://carsun.com.ua/remkomplekt_rulevoi_reiki" },
        //    new Elements() { Name = "Шаровая опора", Link = "https://carsun.com.ua/sharova_opora" },
        //    new Elements() { Name = "Подшипник ступицы", Link = "https://carsun.com.ua/podshipnik_stupici" },
        //    new Elements() { Name = "Сайлентблок рычага", Link = "https://carsun.com.ua/sajlentblok_rychaga" },
        //    new Elements() { Name = "Ступица", Link = "https://carsun.com.ua/stupica" },
        //};


        var elements = new List<Elements>()
        {
            new Elements() { Name = "Поворотный кулак", Link = "https://carsun.com.ua/povorotnyy-kulak" },
            new Elements() { Name = "Рычаг подвески", Link = "https://carsun.com.ua/rich_podveski" },
            new Elements() { Name = "Пружина подвески", Link = "https://carsun.com.ua/pruzhina_pidviski" },
            new Elements() { Name = "Амортизаторы", Link = "https://carsun.com.ua/amortizatori" },
            new Elements() { Name = "Втулка рессоры", Link = "https://carsun.com.ua/vtulka_ressori" },
            new Elements() { Name = "Гайка крепления колеса", Link = "https://carsun.com.ua/gain_kreplenia_kolesa" },
            new Elements() { Name = "Болт крепления колеса", Link = "https://carsun.com.ua/bolt_kreplenia_kolesa" },
            new Elements() { Name = "Пыльник амортизатора", Link = "https://carsun.com.ua/pilnik-amortizatora" },
            new Elements() { Name = "Комплект пыльника и отбойника", Link = "https://carsun.com.ua/komplekt_pilnika_i_otboinika" },
            new Elements() { Name = "Стойка стабилизатора", Link = "https://carsun.com.ua/stoika_stabilizatora" },
            new Elements() { Name = "Подшипник опоры амортизатора", Link = "https://carsun.com.ua/podshipnik_opori_amortizatora" },
            new Elements() { Name = "Тарелка пружины", Link = "https://carsun.com.ua/tarelka_pruzhini" },
            new Elements() { Name = "Пнемостойка", Link = "https://carsun.com.ua/pnevmostoika" },
            new Elements() { Name = "Колесные шпильки", Link = "https://carsun.com.ua/kolesnye_shpilki" },
            new Elements() { Name = "Отбойник амортизатора", Link = "https://carsun.com.ua/otbojniki_amortizatorov" },
        };

        return elements;
    }
}
