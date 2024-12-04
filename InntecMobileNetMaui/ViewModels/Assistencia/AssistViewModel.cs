using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Assistencia
{
    class AssistViewModel : BaseViewModel
    {
        private AssistModel assistModel;

        public Command RegisterCommand { get; set; }

        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        /// <param name="loginModel">Usuario que inicio sesion</param>
        /// <param name="assistModel">datos para la asistencia</param>
        public AssistViewModel(LoginModel loginModel, Models.Assist.AssistModel assistModel)
        {
            this.assistModel = assistModel;

            RegisterCommand = new Command(async () => await ExecuteRegisterCommand().ConfigureAwait(true));
        }
        /// <summary>
        /// Registro a asistencia
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteRegisterCommand()
        {
            _ = await DataCard.RegisterAssist(assistModel).ConfigureAwait(true);
        }
    }
}
