using InntecMobileNetMaui.Models.Gas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Gas
{
    class ActivationRequestReportDetailViewModel : BaseViewModel
    {
        private ActivationRequestReportModel param;

        public ActivationRequestReportModel Param
        {
            get => param;
            set => SetProperty(ref param, value);
        }
        /// <summary>
        /// Inicializar respuesta de activacion de tarjeta
        /// </summary>
        /// <param name="param"></param>
        public ActivationRequestReportDetailViewModel(ActivationRequestReportModel param)
        {
            Title = param.Folio;
            this.param = param;
        }
    }
}
