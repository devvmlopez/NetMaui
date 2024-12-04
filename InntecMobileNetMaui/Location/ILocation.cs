using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Location
{
    public interface ILocation
    {
        double Latitude { get; }
        double Longitude { get; }

        //void ObtainLocation();
        event EventHandler<ILocationEventArgs> locationObtained;
        Task ShowMessage(string title, string message, string button);
        Task<Microsoft.Maui.Devices.Sensors.Location> ObtainLocation();
    }

    public interface ILocationEventArgs
    {
        double lat { get; set; }
        double lng { get; set; }
    }
}
