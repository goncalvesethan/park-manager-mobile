namespace ParkManagerMobile.Pages
{
    public partial class MainFlyoutPage : FlyoutPage
    {
        private readonly HttpClient _httpClient;

        public MainFlyoutPage(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;

            // Définir manuellement la page par défaut (MainPage avec httpClient)
            Detail = new NavigationPage(new MainPage(_httpClient));
        }

        private void OnDashboardTapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new MainPage(_httpClient));
            IsPresented = false;
        }

        private void OnDevicesTapped(object sender, EventArgs e)
        {
            var devicesPage = new ListDevicesPage(_httpClient);
            Detail = new NavigationPage(devicesPage);
            IsPresented = false;
        }

        private void OnParksTapped(object sender, EventArgs e)
        {
            var parksPage = new ListParksPage(_httpClient);
            Detail = new NavigationPage(parksPage);
            IsPresented = false;
        }

        private void OnRoomsTapped(object sender, EventArgs e)
        {
            var roomsPage = new ListRoomsPage(_httpClient);
            Detail = new NavigationPage(roomsPage);
            IsPresented = false;
        }

        private void OnIncidentsTapped(object sender, EventArgs e)
        {
            var incidentsPage = new ListIncidentsPage(_httpClient);
            Detail = new NavigationPage(incidentsPage);
            IsPresented = false;
        }

        private void OnLogoutTapped(object sender, EventArgs e)
        {
            Preferences.Remove("jwt_token");

            var newClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5296/api/")
            };

            Application.Current.MainPage = new NavigationPage(new LoginPage(newClient));
        }
    }

}