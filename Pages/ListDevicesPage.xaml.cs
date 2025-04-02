using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;
using Models;

namespace ParkManagerMobile.Pages
{
    public partial class ListDevicesPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public ListDevicesPage(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;

            _ = LoadDevices();
        }

        private async void OnRefreshTapped(object sender, EventArgs e)
        {
            await LoadDevices();
        }

        private async Task LoadDevices()
        {
            try
            {
                var devices = await _httpClient.GetFromJsonAsync<List<Models.Device>>("devices");

                if (devices is not null)
                {
                    foreach(var device in devices)
                    {
                        device.Park = await _httpClient.GetFromJsonAsync<Park>($"parks/{device.ParkId}");
                        device.Room = await _httpClient.GetFromJsonAsync<Room>($"rooms/{device.RoomId}");
                    }
                    DevicesCollectionView.ItemsSource = devices;
                }
                else
                {
                    await DisplayAlert("Erreur", "Aucun appareil trouvé", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
        }

        private void OnDeviceSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Models.Device selectedDevice)
            {
                Navigation.PushAsync(new DeviceDetailPage(selectedDevice));
                DevicesCollectionView.SelectedItem = null;
            }
        }
    }

}