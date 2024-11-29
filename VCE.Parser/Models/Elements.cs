
namespace VCE.Parser.Models;

public class Elements
{
    public string Name { get; set; }
    public string Link { get; set; }
    public int Number { get; set; }

    public List<Elements> GetElementList()
    {
        var elements = new List<Elements>()
        {
            new Elements() { Name = "Воздушный фильтр", Link = "https://carsun.com.ua/vozdushnyj-filytr", Number = 1075 },

            new Elements() { Name = "Полуось в сборе", Link = "https://carsun.com.ua/poluos", Number = 201 },

            new Elements() { Name = "Капот", Link = "https://carsun.com.ua/kapot", Number = 1 },
            new Elements() { Name = "Подкрылок", Link = "https://carsun.com.ua/podkrilok", Number = 1 },
            new Elements() { Name = "Панель передняя", Link = "https://carsun.com.ua/panely-perednyaja", Number = 1 },
            new Elements() { Name = "Крыло переднее", Link = "https://carsun.com.ua/krylo", Number = 1 },
            new Elements() { Name = "Бампер передний/задний", Link = "https://carsun.com.ua/bamper", Number = 1 },
            new Elements() { Name = "Наружное зеркало", Link = "https://carsun.com.ua/zerkala", Number = 1 },
            new Elements() { Name = "Решетка радиатора", Link = "https://carsun.com.ua/reshetka-radiatora", Number = 1 },
            new Elements() { Name = "Ручка двери", Link = "https://carsun.com.ua/ruchka-dveri", Number = 1 },
            new Elements() { Name = "Усилитель бампера", Link = "https://carsun.com.ua/usilitel-bampera", Number = 1 },
            new Elements() { Name = "Автостекло", Link = "https://carsun.com.ua/steklo-lobovoe", Number = 1 },
            new Elements() { Name = "Облицовка бампера", Link = "https://carsun.com.ua/oblicovka-bampera", Number = 1 },
            new Elements() { Name = "Задняя арка", Link = "https://carsun.com.ua/arka-zadniya", Number = 1 },
            new Elements() { Name = "Порог", Link = "https://carsun.com.ua/porog", Number = 1 },
            new Elements() { Name = "Защита двигателя", Link = "https://carsun.com.ua/zaschita-dvigatelya", Number = 1 },
            new Elements() { Name = "Петля капота", Link = "https://carsun.com.ua/petlia-kapota", Number = 1 },
            new Elements() { Name = "Ресничка под фару", Link = "https://carsun.com.ua/resnichka-pod-faru", Number = 1 },
            new Elements() { Name = "Рем вставка двери", Link = "https://carsun.com.ua/rem-vstavka-dveri", Number = 1 },
            new Elements() { Name = "Трапеция стеклоочистителя", Link = "https://carsun.com.ua/trapecia-stekloochistitelya", Number = 1 },
            new Elements() { Name = "Щетка стеклоочистителя", Link = "https://carsun.com.ua/shhetki-stekloochistitelya", Number = 1 },
            new Elements() { Name = "Двигатель стеклоочистителя", Link = "https://carsun.com.ua/dvigatel-stekloochistitelya", Number = 1 },
            new Elements() { Name = "Насос омывателя", Link = "https://carsun.com.ua/nasos-omyvatelya", Number = 1 },
            new Elements() { Name = "Амортизатор багажника и капота", Link = "https://carsun.com.ua/amortizatory-bagazhnika-kapota", Number = 1 },
            new Elements() { Name = "Стеклоподъемник", Link = "https://carsun.com.ua/steklopodemnik", Number = 1 },
            new Elements() { Name = "Замок двери", Link = "https://carsun.com.ua/zamok-dveri", Number = 1 },
            new Elements() { Name = "Накладка на педаль", Link = "https://carsun.com.ua/nakladka-na-pedal", Number = 1 },
            new Elements() { Name = "Тросик газа", Link = "https://carsun.com.ua/trosik-gaza", Number = 1 },
            new Elements() { Name = "Тросик спидометра", Link = "https://carsun.com.ua/tropic-spidometra", Number = 1 },
            new Elements() { Name = "Трос замка капота", Link = "https://carsun.com.ua/tros-zamka-kapota", Number = 1 },

            new Elements() { Name = "Аккумуляторы", Link = "https://carsun.com.ua/akkumulyatori", Number = 1 },
            new Elements() { Name = "Генератор", Link = "https://carsun.com.ua/generator", Number = 1 },
            new Elements() { Name = "Реле регулятор генератора", Link = "https://carsun.com.ua/rele-regulyator-generatora", Number = 1 },
            new Elements() { Name = "Датчик температуры салона", Link = "https://carsun.com.ua/datchik-temperaturi-salona", Number = 1 },
            new Elements() { Name = "Датчик уровня охлаждающей жидкости", Link = "https://carsun.com.ua/datchik-urovnya-ohlazhdaushei-zhidkosti", Number = 1 },
            new Elements() { Name = "Бендикс стартера", Link = "https://carsun.com.ua/bendiks-startera", Number = 1 },
            new Elements() { Name = "Втягивающее реле стартера", Link = "https://carsun.com.ua/vtyagivajushheje-rele-startera", Number = 1 },
            new Elements() { Name = "Стартер", Link = "https://carsun.com.ua/starter", Number = 1 },
            new Elements() { Name = "Датчик уровня топлива", Link = "https://carsun.com.ua/datchik-urovnya-topliva", Number = 1 },
            new Elements() { Name = "Датчик скорости", Link = "https://carsun.com.ua/datchik-skorosti", Number = 1 },
            new Elements() { Name = "Шкив генератора", Link = "https://carsun.com.ua/shkiv-generatora", Number = 1 },
            new Elements() { Name = "Датчик уровня масла", Link = "https://carsun.com.ua/datchik-urovnya-masla", Number = 1 },
            new Elements() { Name = "Муфта генератора", Link = "https://carsun.com.ua/mufta-generatora", Number = 1 },
            new Elements() { Name = "Датчик температуры охлаждающей жидкости", Link = "https://carsun.com.ua/datchik-temperatury-ohlazhdajushhej", Number = 1 },
            new Elements() { Name = "Датчик включения вентилятора", Link = "https://carsun.com.ua/datchk-vkluchenia-ventilatora", Number = 1 },
            new Elements() { Name = "Мост диодный генератора", Link = "https://carsun.com.ua/most-diodnii-generatora", Number = 1 },
            new Elements() { Name = "Подрулевые переключатели", Link = "https://carsun.com.ua/podrulevyje-pereklyuchateli", Number = 1 },
            new Elements() { Name = "Кнопка стеклоподъемника", Link = "https://carsun.com.ua/knopka-steklopodemnika", Number = 1 },
            new Elements() { Name = "Датчик заднего хода", Link = "https://carsun.com.ua/datchik-zadnego-khoda", Number = 1 },
            new Elements() { Name = "Клапан холостого хода", Link = "https://carsun.com.ua/klapan-holostogo-hoda", Number = 1 },
            new Elements() { Name = "Датчик температуры выхлопных газов", Link = "https://carsun.com.ua/datchik-temperatury-vyhlopnyh-gazov", Number = 1 },
            new Elements() { Name = "Комплектующие стартера", Link = "https://carsun.com.ua/komplektuyuschie-startera", Number = 1 },
            new Elements() { Name = "Комплектующие генератора", Link = "https://carsun.com.ua/komplektuyuschie-generatora", Number = 1 },
            new Elements() { Name = "Кнопки та перемикачі", Link = "https://carsun.com.ua/knopki-ta-peremikachi", Number = 1 },
            new Elements() { Name = "Переходники, штекеры, кабели", Link = "https://carsun.com.ua/perehodniki-shtekery-kabeli", Number = 1 },
            new Elements() { Name = "Указатель поворота", Link = "https://carsun.com.ua/ukazatel-povorota", Number = 1 },
            new Elements() { Name = "Противотуманная фара", Link = "https://carsun.com.ua/protivotumannaya-fara", Number = 1 },
            new Elements() { Name = "Основная фара", Link = "https://carsun.com.ua/fara-osnovnaja", Number = 1 },
            new Elements() { Name = "Задний фонарь", Link = "https://carsun.com.ua/zadnij-fonar", Number = 1 },
            new Elements() { Name = "Автолампы", Link = "https://carsun.com.ua/avtolampi", Number = 1 },
            new Elements() { Name = "Выключатель стоп-сигнала", Link = "https://carsun.com.ua/vikluchatel-stop-signala", Number = 1 },
            new Elements() { Name = "Свечи зажигания", Link = "https://carsun.com.ua/svechi-zazhiganiia", Number = 1 },
            new Elements() { Name = "Катушка зажигания", Link = "https://carsun.com.ua/katushka-zazhiganija", Number = 1 },
            new Elements() { Name = "Провода высоковольтные", Link = "https://carsun.com.ua/provoda-vysokovolytnyje", Number = 1 },
            new Elements() { Name = "Трамблер", Link = "https://carsun.com.ua/trambler", Number = 1 },
            new Elements() { Name = "Комутатор системы зажигания", Link = "https://carsun.com.ua/komutator-zazhiganija", Number = 1 },
            new Elements() { Name = "Свеча накаливания", Link = "https://carsun.com.ua/svecha-nakalivaniia", Number = 1 },
            new Elements() { Name = "Бегунок распределителя зажигания", Link = "https://carsun.com.ua/begunok-raspredelitelya-zazhiganija", Number = 1 },
            new Elements() { Name = "Крышка распределителя зажигания", Link = "https://carsun.com.ua/kryshka-tramblera", Number = 1 },
            new Elements() { Name = "Контактная группа распределителя зажигания", Link = "https://carsun.com.ua/kontaktnaya-gruppa-raspredelitelya-zazhigania", Number = 1 },
            new Elements() { Name = "Реле свечей накаливания", Link = "https://carsun.com.ua/rele-svechey-nakalivaniya", Number = 1 },
            new Elements() { Name = "Замок зажигания", Link = "https://carsun.com.ua/zamok-zazhiganiya", Number = 1 },
        };

        return elements;
    }
}
