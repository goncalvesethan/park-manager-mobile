using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;
using Models;
using Microsoft.Maui.Controls;

namespace ParkManagerMobile.Pages
{
    public partial class ListRoomsPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public ListRoomsPage(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;

            _ = LoadRooms();
        }

        private async void OnRefreshTapped(object sender, EventArgs e)
        {
            await LoadRooms();
        }

        private async Task LoadRooms()
        {
            try
            {
                var rooms = await _httpClient.GetFromJsonAsync<List<Room>>("rooms");
                var parks = await _httpClient.GetFromJsonAsync<List<Park>>("parks");

                if (rooms is not null)
                {
                    foreach (var room in rooms)
                    {
                        room.Park = await _httpClient.GetFromJsonAsync<Park>($"parks/{room.ParkId}");

                    }
                    RoomsCollectionView.ItemsSource = rooms;
                }
                else
                {
                    await DisplayAlert("Erreur", "Aucune salle trouvée", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
        }

        private void OnRoomSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Room selectedRoom)
            {
                Navigation.PushAsync(new RoomDetailPage(selectedRoom, _httpClient));
                RoomsCollectionView.SelectedItem = null;
            }
        }
    }

}