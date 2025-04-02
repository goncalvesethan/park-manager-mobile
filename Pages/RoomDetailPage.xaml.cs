using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;
using Models;

namespace ParkManagerMobile.Pages
{
    public partial class RoomDetailPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        private readonly Room _room;

        public RoomDetailPage(Room selectedRoom, HttpClient httpClient)
        {
            InitializeComponent();
            _room = selectedRoom;
            _httpClient = httpClient;
            BindingContext = _room;

            _ = LoadDevices();
        }

        private async Task LoadDevices()
        {
            try
            {
                var allDevices = await _httpClient.GetFromJsonAsync<List<Models.Device>>("devices");
                var roomDevices = allDevices
                    .Where(d => d.RoomId == _room.Id)
                    .ToList();

                foreach (var device in roomDevices)
                {
                    device.Park = await _httpClient.GetFromJsonAsync<Park>($"parks/{device.ParkId}");
                }

                DevicesCollectionView.ItemsSource = roomDevices;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
        }
    }

}