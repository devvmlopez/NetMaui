using InntecMobileNetMaui.Models.Viatics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Viatics.Search
{
    public class SearchCityViewModel : BaseViewModel
    {
        public List<InfoCity> infoCiudades { get; private set; }
        public ObservableCollection<InfoCity> filterCity { get; set; }

        private string _Filter;
        public string Filter { get => _Filter; set => SetProperty(ref _Filter, value); }

        public Command FillListCommand;
        public SearchCityViewModel(List<InfoCity> infoCiudades)
        {
            this.infoCiudades = infoCiudades;
            filterCity = new ObservableCollection<InfoCity>();
            FillListCommand = new Command(() => FillListCommandExcecute());
        }

        private void FillListCommandExcecute()
        {
            filterCity.Clear();

            if (string.IsNullOrEmpty(Filter))
            {
                foreach (InfoCity item in infoCiudades)
                {
                    filterCity.Add(item);
                }
            }
            else
            {
                foreach (InfoCity item in infoCiudades.Where(item => item.Nombre.ToUpper().Contains(Filter.ToUpper())))
                {
                    filterCity.Add(item);
                }
            }
        }
    }
}
