using System;  // Bruger systembiblioteker
using System.Collections.Generic;  // Bruger systemet til at administrere lister
using System.ComponentModel;  // Bruger systemet til at understøtte komponenter
using System.Data;  // Bruger systemet til at arbejde med data
using System.Drawing;  // Bruger systemet til at arbejde med grafik
using System.Linq;  // Bruger systemet til at understøtte LINQ-forespørgsler
using System.Text;  // Bruger systemet til at arbejde med tekststrengfunktioner
using System.Threading.Tasks;  // Bruger systemet til at arbejde med asynkrone opgaver
using System.Windows.Forms;  // Bruger systemet til at oprette Windows Forms-applikationer
using DVI_Access_Lib;  // Bruger DVI Access Library til at få adgang til specifik funktionalitet, som er givet fra vores lære
using System.Text.Json;  // Bruger systembiblioteket til JSON-håndtering
using DVI_Gui;  // Bruger DVI Demotest-namespace til yderligere funktionalitet
using System.Net.Http;  // Tilføjet for HTTP-anmodninger
using Newtonsoft.Json.Linq;  // Tilføjet for JSON-håndtering med JObject

namespace DVI_Gui
{
    public partial class Vin_Gui : Form
    {
        private readonly DVI_Climate climate;  // Felt til at håndtere klimaobjektet
        private readonly DVI_Stock stock;
        private Timer tempUpdateTimer;  // Timer til opdatering af temperatur og luftfugtighed
        private Timer stockUpdateTimer;  // Timer til opdatering af lageroplysninger
        private Timer clockTimer;  // Timer til opdatering af klokkeslæt
        private static readonly string apiKey = "75d8f6ab23a442c3db3fed72690699fc";
        private static readonly string city = "Aalborg";
        private static readonly string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city},dk&appid={apiKey}&units=metric";

        public Vin_Gui()
        {
            InitializeComponent();  // Initialiserer komponenter på formen

            // Initialiserer klimaobjektet med den relevante server URL
            climate = new DVI_Climate("http://docker.data.techcollege.dk:5051");
            stock = new DVI_Stock("http://docker.data.techcollege.dk:5051");

            // Initialiserer timeren for opdatering hvert 5. minut
            InitializeTimer();
            InitializeClockTimer();


            // Opdaterer temperatur og luftfugtighed ved start
            UpdateTemperatureAndHumidity();
            UpdateStockInfo();
            FetchAndDisplayWeatherData();
        }

        // Initialiserer timeren og indstiller intervallet
        private void InitializeTimer()
        {
            tempUpdateTimer = new Timer();
            tempUpdateTimer.Interval = 60000;  // 1 minut
            tempUpdateTimer.Tick += async (sender, e) => {
                await UpdateTemperatureAndHumidity();
                await FetchAndDisplayWeatherData();  // Tilføj dette for at opdatere vejret
            };
            tempUpdateTimer.Start();

            stockUpdateTimer = new Timer();
            stockUpdateTimer.Interval = 300000;  // 5 minutter
            stockUpdateTimer.Tick += async (sender, e) => await UpdateStockInfo();
            stockUpdateTimer.Start();
        }

        // Event handler-metode for Timerens Tick-hændelse, kaldt når timeren tikker
        private async void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // Kalder metoden til at opdatere temperatur og luftfugtighed asynkront
            await UpdateTemperatureAndHumidity();
            await UpdateStockInfo();
        }

        private void InitializeClockTimer()
        {
            clockTimer = new Timer();
            clockTimer.Interval = 1000; // 1 second
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            UpdateClocks();
        }

        private void UpdateClocks()
        {
            DateTime DateTime = DateTime.Now;

            // Dansk tid
            DateTime denmarkTime = DateTime;
            denmarkClockLabel.Text = "Denmark: " + denmarkTime.ToString("HH:mm:ss");

            // Florida tid 
            DateTime floridaTime = DateTime.AddHours(-6);
            floridaClockLabel.Text = "Florida, USA: " + floridaTime.ToString("HH:mm:ss");

            // Japan tid
            DateTime japanTime = DateTime.AddHours(7);
            japanClockLabel.Text = "Japan: " + japanTime.ToString("HH:mm:ss");
        }

        // Asynkron metode til at opdatere temperatur og luftfugtighed
        private async Task UpdateTemperatureAndHumidity()
        {
            try
            {
                Console.WriteLine("Henter temperatur og luftfugtighedsdata...");

                // Henter temperaturen asynkront ved hjælp af DVI_Climate-objektets CurrTemp-metode
                float temp = await Task.Run(() => climate.CurrTemp());

                // Henter luftfugtigheden asynkront ved hjælp af DVI_Climate-objektets CurrHum-metode
                float hum = await Task.Run(() => climate.CurrHum());

                // Henter minimum og maksimum temperatur- og luftfugtighedsgrænser
                float minTemp = await Task.Run(() => climate.MinTemp());
                float maxTemp = await Task.Run(() => climate.MaxTemp());
                float minHum = await Task.Run(() => climate.MinHum());
                float maxHum = await Task.Run(() => climate.MaxHum());

                // Udskriver de hentede data til konsollen
                Console.WriteLine($"Hentede data: Temperatur - {temp}°C, Luftfugtighed - {hum}%");

                // Opdaterer labels på Windows-formen med de hentede data
                temperatureLabel.Text = $"Temperatur: {temp.ToString("F2")}°C";
                humidityLabel.Text = $"Luftfugtighed: {hum.ToString("F2")}%";

                // Tjekker om temperaturen er uden for grænserne og viser en advarselsmeddelelse, hvis det er tilfældet
                if (temp < minTemp || temp > maxTemp)
                {
                    MessageBox.Show($"Alarm! Temperatur er uden for grænserne: {temp}°C", "Klimaadvarsel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Tjekker om luftfugtigheden er uden for grænserne og viser en advarselsmeddelelse, hvis det er tilfældet
                if (hum < minHum || hum > maxHum)
                {
                    MessageBox.Show($"Alarm! Luftfugtighed er uden for grænserne: {hum}%", "Klimaadvarsel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Viser fejlmeddelelse, hvis der er en fejl ved hentning af temperatur/luftfugtighed
                MessageBox.Show($"Fejl ved hentning af temperatur/luftfugtighedsdata: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Fejl ved hentning af temperatur/luftfugtighedsdata: {ex.Message}");
            }
        }
        private async Task UpdateStockInfo()
        {
            try
            {
                Console.WriteLine("Henter lagerdata...");

                List<Wine> winesOverThreshold = await Task.Run(() => stock.StockOverThreshold());
                List<Wine> winesUnderThreshold = await Task.Run(() => stock.StockUnderThreshold());
                List<Wine> winesOnStock = await Task.Run(() => stock.WinesOnStock());

                winesOverMaxListBox.Items.Clear();
                winesUnderMinListBox.Items.Clear();
                winesOnStockListBox.Items.Clear();

                foreach (var wine in winesOverThreshold) // Hviser vin som er over max gransen
                {
                    winesOverMaxListBox.Items.Add($"{wine.WineName}");
                    winesOverMaxListBox.Items.Add($"Lager: {wine.NumInStock} - Pris: {wine.PurchasePrice:C}");
                    winesOverMaxListBox.Items.Add(" ");

                }

                foreach (var wine in winesUnderThreshold) // Hviser vin som er under min gransen
                {
                    winesUnderMinListBox.Items.Add($"{wine.WineName}");
                    winesUnderMinListBox.Items.Add($"Lager: {wine.NumInStock} - Pris: {wine.PurchasePrice:C}");
                    winesUnderMinListBox.Items.Add(" ");
                }

                foreach (var wine in winesOnStock) // Hviser hvad vin der er på lager
                {
                    winesOnStockListBox.Items.Add($"{wine.WineName}");
                    winesOnStockListBox.Items.Add($"Lager: {wine.NumInStock} - Pris: {wine.PurchasePrice:C}");
                    winesOnStockListBox.Items.Add(" ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved hentning af lagerdata: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Fejl ved hentning af lagerdata: {ex.Message}");
            }
        }

        private async Task FetchAndDisplayWeatherData()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject weatherData = JObject.Parse(responseBody);

                    double temperature = weatherData["main"]["temp"].ToObject<double>();
                    int humidity = weatherData["main"]["humidity"].ToObject<int>();

                    Console.WriteLine($"Temperatur in {city}: {temperature}°C");
                    Console.WriteLine($"Luftfugtighed in {city}: {humidity}%");

                    // Update the labels on the form with the weather data
                    TempUdLb.Text = $"Udenfor Temperatur: {temperature}°C";
                    HumUdLb.Text = $"Udenfor Luftfugtighed: {humidity}%";
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Anmodningsfejl: {e.Message}");
                    MessageBox.Show($"Fejl ved hentning af vejrdata.: {e.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // Event handler-metode kaldt når Vin_Gui-formen indlæses
        private void Vin_Gui_Load(object sender, EventArgs e)
        {
            // Eventuel initialiseringskode ved indlæsning af formen
        }

        // Event handler-metode til at håndtere klik på Label1 (som navngivet i Windows Forms designer)
        private void Label1_Click(object sender, EventArgs e)
        {
            // Håndter klik på etiketten, hvis nødvendigt , men vi har ikke at man at man kan klikke på den med
        }

        private void humidityLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void LagerTExt_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void winesOverMaxListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_3(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void winesUnderMinListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_4(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void floridaClockLabel_Click(object sender, EventArgs e)
        {

        }
    }
}

