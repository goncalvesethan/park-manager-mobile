using System.Net.Http.Json;
using Models;

namespace ParkManagerMobile
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public MainPage(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
            _ = LoadDashboard();
        }

        private async Task LoadDashboard()
        {
            try
            {
                var parks = await _httpClient.GetFromJsonAsync<List<Park>>("parks");
                var rooms = await _httpClient.GetFromJsonAsync<List<Room>>("rooms");
                var devices = await _httpClient.GetFromJsonAsync<List<Models.Device>>("devices");
                var incidents = await _httpClient.GetFromJsonAsync<List<Incident>>("incidents");

                var stats = new DashboardStats
                {
                    ParksCount = parks?.Count ?? 0,
                    RoomsCount = rooms?.Count ?? 0,
                    DevicesCount = devices?.Count ?? 0,
                    OnlineDevicesCount = devices?.Count(d => d.IsOnline) ?? 0,
                    IncidentsCount = incidents?.Count ?? 0,
                    OpenIncidentsCount = incidents?.Count(i => i.Status?.ToLower() == "open") ?? 0
                };

                BindingContext = stats;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
        }

    }
    public class DashboardStats
    {
        public int ParksCount { get; set; }
        public int RoomsCount { get; set; }
        public int DevicesCount { get; set; }
        public int OnlineDevicesCount { get; set; }
        public int IncidentsCount { get; set; }
        public int OpenIncidentsCount { get; set; }
    }
}
