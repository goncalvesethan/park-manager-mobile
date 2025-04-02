using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;
using Models;

namespace ParkManagerMobile.Pages
{
    public partial class ParkDetailPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        private readonly Park _park;

        public ParkDetailPage(Park selectedPark, HttpClient httpClient)
        {
            InitializeComponent();
            _park = selectedPark;
            _httpClient = httpClient;
            BindingContext = _park;

            _ = LoadDevices();
        }

        private async Task LoadDevices()
        {
            try
            {
                var allDevices = await _httpClient.GetFromJsonAsync<List<Models.Device>>("devices");
                var parkDevices = allDevices
                    .Where(d => d.ParkId == _park.Id)
                    .ToList();

                foreach (var device in parkDevices)
                {
                    device.Room = await _httpClient.GetFromJsonAsync<Room>($"rooms/{device.RoomId}");
                }

                DevicesCollectionView.ItemsSource = parkDevices;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
        }
    }

}