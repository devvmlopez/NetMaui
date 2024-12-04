using InntecMobileNetMaui.Models.Promotions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Promotions
{
    public class PromotionsListViewModel : BaseViewModel
    {
        int comercioId;
        public Command LoadPromotionsCommand;
        public Command LoadPromoPageCommand { get; set; }
        public ObservableCollection<PromotionModel> promociones { get; set; }


        public PromotionsListViewModel(int comercioId)
        {

            this.comercioId = comercioId;
            promociones = new ObservableCollection<PromotionModel>();
            LoadPromotionsCommand = new Command(async
                () => await ExecuteLoadPromotionsCommand());

            LoadPromoPageCommand = new Command((args) => ExecuteLoadPromoPageCommand(args));
        }

        private void ExecuteLoadPromoPageCommand(object args)
        {

             Launcher.Default.OpenAsync(((Models.Promotions.PromotionModel)args).UrlPromo);

        }


        private async Task ExecuteLoadPromotionsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;      

            var PromotionResult = await DataPromos.getPromotion(comercioId);

            //PromotionResult.Add(new PromotionModel { UrlImage = "muestra68.png" });
            //PromotionResult.Add(new PromotionModel { UrlImage = "muestra68.png" });
            //PromotionResult.Add(new PromotionModel { UrlImage = "muestra68.png" });
            //PromotionResult.Add(new PromotionModel { UrlImage = "muestra68.png" });
            await Task.Delay(1000);
            foreach (PromotionModel item in PromotionResult)
            {
                promociones.Add(item);
            }

            IsBusy = false;
        }
    }
}
