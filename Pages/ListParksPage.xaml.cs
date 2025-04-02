using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;
using Models;

namespace ParkManagerMobile.Pages
{
    public partial class ListParksPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public ListParksPage(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;

            _ = LoadParks();
        }

        private async void OnRefreshTapped(object sender, EventArgs e)
        {
            await LoadParks();
        }

        private async Task LoadParks()
        {
            try
            {
                var parks = await _httpClient.GetFromJsonAsync<List<Park>>("parks");
                var devices = await _httpClient.GetFromJsonAsync<List<Models.Device>>("devices");

                if (parks is not null)
                {
                    ParksCollectionView.ItemsSource = parks;
                }
                else
                {
                    await DisplayAlert("Erreur", "Aucun parc trouvé", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
        }

        private void OnParkSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Park selectedPark)
            {
                Navigation.PushAsync(new ParkDetailPage(selectedPark, _httpClient));
                ParksCollectionView.SelectedItem = null;
            }
        }
    }

}