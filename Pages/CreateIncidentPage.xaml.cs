using Android.App;
using AndroidX.Activity;
using Models;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ParkManagerMobile.Pages;

public partial class CreateIncidentPage : ContentPage
{
    private readonly HttpClient _httpClient;
    private List<Models.Device> _devices;

    public CreateIncidentPage(HttpClient httpClient)
    {
        InitializeComponent();
        _httpClient = httpClient;
        _ = LoadDevices();
    }

    private async Task LoadDevices()
    {
        try
        {
            _devices = await _httpClient.GetFromJsonAsync<List<Models.Device>>("devices");
            DevicePicker.ItemsSource = _devices;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", $"Erreur lors du chargement des postes : {ex.Message}", "OK");
        }
    }

    private async void OnSubmitIncident(object sender, EventArgs e)
    {
        if (DevicePicker.SelectedItem is not Models.Device selectedDevice ||
            TypePicker.SelectedItem is not string selectedType)
        {
            await DisplayAlert("Erreur", "Veuillez sélectionner un poste et un type d'incident.", "OK");
            return;
        }

        var userId = Preferences.Get("user_id", ""); // string
        var reportedId = int.TryParse(userId, out var id) ? id : 0;

        var newIncident = new Incident()
        {
            ReporterId = reportedId,
            DeviceId = selectedDevice.Id,
            Type = selectedType,
            Description = DescriptionEditor.Text
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("incidents", newIncident);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Succès", "L'incident a été créé.", "OK");
                await Navigation.PushAsync(new ListIncidentsPage(_httpClient));
            }
            else
            {
                await DisplayAlert("Erreur", "Impossible de créer l'incident.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", ex.Message, "OK");
        }
    }
}
