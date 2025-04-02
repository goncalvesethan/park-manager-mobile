using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;
using Models;

namespace ParkManagerMobile.Pages
{
    public partial class IncidentDetailPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        private readonly Incident _incident;

        public IncidentDetailPage(Incident selectedIncident, HttpClient httpClient)
        {
            InitializeComponent();
            _incident = selectedIncident;
            _httpClient = httpClient;
            BindingContext = _incident;

            _ = LoadDeviceInformations();
        }

        private async Task LoadDeviceInformations()
        {
            try
            {
                var device = await _httpClient.GetFromJsonAsync<Models.Device>($"devices/{_incident.DeviceId}");
                
                if(device != null)
                {
                    device.Park = await _httpClient.GetFromJsonAsync<Park>($"parks/{device.ParkId}");
                    device.Room = await _httpClient.GetFromJsonAsync<Room>($"rooms/{device.RoomId}");
                }

                DeviceFrame.BindingContext = device;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
        }

        private async Task ResolveIncident()
        {
            try
            {
                var response = await _httpClient.PatchAsync($"incidents/{_incident.Id}/close", null);
                response.EnsureSuccessStatusCode();
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
        }

        private async void ResolveIncidentClicked(object sender, EventArgs e)
        {
            await ResolveIncident();
        }
    }

}