using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;
using Models;

namespace ParkManagerMobile.Pages
{
    public partial class DeviceDetailPage : ContentPage
    {
        public DeviceDetailPage(Models.Device selectedDevice)
        {
            InitializeComponent();
            BindingContext = selectedDevice;
        }
    }

}