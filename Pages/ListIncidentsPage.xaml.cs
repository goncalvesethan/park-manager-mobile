using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;
using Models;

namespace ParkManagerMobile.Pages
{
    public partial class ListIncidentsPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public ListIncidentsPage(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;

            _ = LoadIncidents();
        }

        private async void OnRefreshTapped(object sender, EventArgs e)
        {
            await LoadIncidents();
        }

        private void OnCreateTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateIncidentPage(_httpClient));
        }

        private async Task LoadIncidents()
        {
            try
            {
                var incidents = await _httpClient.GetFromJsonAsync<List<Incident>>("incidents");

                if (incidents is not null)
                {
                    foreach(var incident in incidents)
                    {
                        incident.Device = await _httpClient.GetFromJsonAsync<Models.Device>($"devices/{incident.DeviceId}");
                        incident.Device.Park = await _httpClient.GetFromJsonAsync<Park>($"parks/{incident.Device.ParkId}");
                        incident.Reporter = await _httpClient.GetFromJsonAsync<User>($"users/{incident.ReporterId}");
                    }
                    IncidentsCollectionView.ItemsSource = incidents;
                }
                else
                {
                    await DisplayAlert("Erreur", "Aucun incident trouvé", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
        }

        private void OnIncidentSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Incident selectedIncident)
            {
                Navigation.PushAsync(new IncidentDetailPage(selectedIncident, _httpClient));
                IncidentsCollectionView.SelectedItem = null;
            }
        }
    }

}