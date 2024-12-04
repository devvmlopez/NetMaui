
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Location
{
   public class GetLocation 
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public async void ObtainLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                    {
                        DesiredAccuracy = GeolocationAccuracy.High,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                if (location == null)
                {
                   // labelLatLong.Text = "Unable to get location."; Estado de error 
                }
                else
                {
                    //labelLatLong.Text = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
                    Preferences.Default.Set("LatLocation", location.Latitude);
                    Preferences.Default.Set("LngLocation", location.Longitude);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
              
            }
           
        }

      
    }
}
