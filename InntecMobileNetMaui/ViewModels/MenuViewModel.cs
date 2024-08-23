using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels
{
    /// <summary>
    /// Menu principal
    /// </summary>
    class MenuViewModel : BaseViewModel
    {
        public Command LoadUserDataCommand { get; set; }
        private string userName;
        public string UserName { get => userName; set => SetProperty(ref userName, value); }
        internal ObservableCollection<HomeMenuItem> MainMenu
        {
            get; set;
        }
        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        public MenuViewModel()
        {
            Title = "Menu";
            UserName = Constants.UserName;
            LoadUserDataCommand = new Command(() => ExecuteLoadUserDataCommand());
            MainMenu = new ObservableCollection<HomeMenuItem>()
            {
                new HomeMenuItem { Id = Enumeradores.enumMenuItemType.Cards, Title="Mis tarjetas", ImgMenu="cards.png"},
                new HomeMenuItem { Id = Enumeradores.enumMenuItemType.MyData, Title= "Mis datos", ImgMenu="mydata.png"},
                
                //new HomeMenuItem { Id = Enumeradores.enumMenuItemType.InntecDescuentos, Title= "", ImgMenu="BenefitW.png"},
                new HomeMenuItem { Id = Enumeradores.enumMenuItemType.LogOut, Title = "Cerrar sesion", ImgMenu = "logout.png" }
            };
        }
        /// <summary>
        /// Datos del usuario
        /// </summary>
        private async void ExecuteLoadUserDataCommand()
        {
            if (Microsoft.Maui.Devices.DeviceInfo.Platform == DevicePlatform.Android)
                await Task.Delay(100).ConfigureAwait(true);

        }
    }
}
