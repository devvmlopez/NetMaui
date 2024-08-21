using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<LoginModel> DataLogin => DependencyService.Get<IDataStore<LoginModel>>();
        //public ICardsService<CardModel> DataCard => DependencyService.Get<ICardsService<CardModel>>();
        public IUserService<UserModel> DataUser => DependencyService.Get<IUserService<UserModel>>();
        //public Services.Viatics.IViaticsService<Models.Viatics.InfoNewRequest> DataViatics => DependencyService.Get<Services.Viatics.IViaticsService<Models.Viatics.InfoNewRequest>>();
        //public Services.Gas.IGasService<Guid> DataGas => DependencyService.Get<Services.Gas.IGasService<Guid>>();
        //public Services.notificacion.INotifyServices DataNotify = DependencyService.Get<Services.notificacion.INotifyServices>();
        //public Services.Promociones.IPromotionServices<Models.Promotions.StorePromotion> DataPromos = DependencyService.Get<Services.Promociones.IPromotionServices<Models.Promotions.StorePromotion>>();
        //public Services.Aclaracion.IAclaracionService DataAclaracion = DependencyService.Get<Services.Aclaracion.IAclaracionService>();

        public IReCaptchaService reCaptchaService = DependencyService.Get<IReCaptchaService>();

        bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }


        string tarjeta = string.Empty;
        public string Card
        {
            get => tarjeta;
            set => SetProperty(ref tarjeta, value);
        }

        string title = string.Empty;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }



        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
