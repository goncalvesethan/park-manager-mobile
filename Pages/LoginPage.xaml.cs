using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace ParkManagerMobile.Pages
{
    public partial class LoginPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public LoginPage(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text;
            var password = PasswordEntry.Text;

            var loginData = new { email, password };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("auth/login", loginData);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<TokenResponse>(responseBody)?.Token;

                    if (!string.IsNullOrEmpty(token))
                    {
                        Preferences.Set("jwt_token", token);

                        Application.Current.MainPage = new MainFlyoutPage(_httpClient);

                    }
                }
                else
                {
                    MessageLabel.Text = "Échec de la connexion.";
                    MessageLabel.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                MessageLabel.Text = $"Erreur : {ex.Message}";
                MessageLabel.IsVisible = true;
            }
        }

        private class TokenResponse
        {
            [JsonProperty("token")]
            public string Token { get; set; }
        }
    }

}