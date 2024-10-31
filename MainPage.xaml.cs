
using System.Net;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace walutownik
{
    public partial class MainPage : ContentPage
    {
        /***********************************************************************************************************************
         * nazwa klasy: Currency
         * parametr wejściowy: brak
         * wartości zwracane: brak
         * informacje: klasa która przechowuje dane dla waluty którą mozna podać w nastepnej cześci kodu
         * 
         * wykonano przez: Kamil szat
        ***********************************************************************************************************************/
        public class Currency
        {
            public string? table { get; set; }
            public string? currency { get; set; }
            public string? code { get; set; }
            public IList<Rate> rates { get; set; }


        }

        /************************************************************************************************************************
       * nazwa klasy: Rate
       * parametr wejściowy: brak
       * wartości zwracane: brak
       * informacje: klasa która zawera datę, kiedy ile kosztuje ealuta oraz zawera w sobie kupno i przedarz waluty 
       * 
       * wykonano przez: Kamil szat
      ***************************************************************************************************************************/
        public class Rate
        {
            public string? no { get; set; }
            public string? effectiveDate { get; set; }
            public double? bid { get; set; }
            public double? ask { get; set; }
        }


        public MainPage()
        {
            InitializeComponent();
        }

        /**************************************************************************************************************************
       * nazwa funkcji: OnCounterClicked(
       * 
       * parametr wejściowy: sender - obuiekt który wywołuje zdarzenie; EventArgs e - zawiera argumenty 
       * zwiazane ze zdarzeniami; (nie są używane w konkretnym celu 
       * 
       * wartości zwracane: string reprezentuje obiekt; using - reprezentuje tak jakby metodę pobieranai ceny:
       * table - zawiera tablelę walut; currency - zawiera nazwę waluty; effectiveDate - zawiera datę którą wybrał użytkownik
       * bid - zawiera cenę przy zakupie waluty; ask - zawiera cenę sprzedarzy waluty
       * 
       * informacje: funkcja zawiera kilka ważnych danych które podam w tym momęcie:
       * 1. string date - zawiera datę którą potem może wybrać użytkownik
       * 2. string url - zawiera link z wyszukiwanie obecnej ceny danej waluty którą wybrał użytkownik
       * 3. webClient.DownloadString(url) - pobiera walutę z liku podanego powyrzej
       * 4. Currency c = JsonSerializer.Deserialize<Currency>(json) - deserializuje i serializuje dane currency 
       * 5. picker.SelectedItem - zawiera listę w której są podane waluty które wybiera użytkownik by sprawdzij ich dane 
       * 6. brak internetu = aplikacja się uruchamia lecz brak możliwości sprawdzenia działania kodu
       * 
       * wykonano przez: Kamil szat
      ****************************************************************************************************************************/
        private void OnCounterClicked(object sender, EventArgs e)
        {
            string date = dpData.Date.ToString("yyyy-MM-dd");
            string url = "https://api.nbp.pl/api/exchangerates/rates/c/" + picker.SelectedItem + "/" + date + "/?format=json";
            string json = "";


            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }

            Currency c = JsonSerializer.Deserialize<Currency>(json);
            string s = $"nazwa waluty: {c.currency}\n";
            s += $"kod waluty: {c.code}\n";
            s += $"Data: {c.rates[0].effectiveDate}\n";
            s += $"Cena skupu: {c.rates[0].bid}\n";
            s += $"Cena sprzedarzy: {c.rates[0].ask}\n";

            cLabel.Text = s;




        }
    }

}
