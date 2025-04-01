namespace ParkManagerMobile.Pages
{
    public partial class MainFlyoutPage : FlyoutPage
    {
        public MainFlyoutPage()
        {
            InitializeComponent();
        }

        private void OnDashboardTapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new MainPage());
            IsPresented = false;
        }

        private async void OnLogoutTapped(object sender, EventArgs e)
        {
            Preferences.Remove("jwt_token");
            Application.Current.MainPage = new NavigationPage(new LoginPage(new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5296/api/")
            }));
        }
    }

}