using InntecMobileNetMaui.Models.Viatics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Viatics
{
    class ViaticsCheckDetailViewModel : BaseViewModel
    {
        private ConsumptionDetail param;
        public ConsumptionDetail Param
        {
            get => param;
            set => SetProperty(ref param, value);
        }
        public ViaticsCheckDetailViewModel(ConsumptionDetail param)
        {
            Title = param.Folio;
            this.Param = param;

        }

        /// <summary>
        /// Envio de XML
        /// </summary>
        /// <param name="fileXML">Archivo</param>
        /// <param name="fileName">Nombre del archivo</param>
        /// <returns></returns>
        internal async Task<Models.Viatics.FileResult> SendXML(byte[] fileXML, string fileName)
        {
            param.FileName = fileName;
            return await DataViatics.sendFile(fileXML, "xml", param).ConfigureAwait(true);
        }
        /// <summary>
        /// Envio de PDF
        /// </summary>
        /// <param name="filePDF">Archivo</param>
        /// <param name="fileName">Nombre del archivo</param>
        /// <returns></returns>
        internal async Task<Models.Viatics.FileResult> SendPDF(byte[] filePDF, string fileName)
        {
            param.FileName = fileName;
            return await DataViatics.sendFile(filePDF, "pdf", param).ConfigureAwait(true);
        }
        /// <summary>
        /// Envio de comprobante
        /// </summary>
        /// <param name="fileIMG">Archivo</param>
        /// <param name="fileName">Nombre del archivo</param>
        /// <returns></returns>
        internal async Task<Models.Viatics.FileResult> SendVOUCHER(byte[] fileIMG, string fileName)
        {
            param.FileName = fileName;
            return await DataViatics.sendFile(fileIMG, "comp", param).ConfigureAwait(true);
        }
    }
}
