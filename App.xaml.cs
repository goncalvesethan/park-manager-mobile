using Microsoft.Extensions.DependencyInjection;
using ParkManagerMobile.Pages;

namespace ParkManagerMobile
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = serviceProvider.GetRequiredService<LoginPage>();
        }
    }
}
