using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Viatics;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.ViewModels.Viatics;
using InntecMobileNetMaui.Views.Alerts;
using Microsoft.Maui.Storage;
using Mopups.Services;
using System.Data.SqlTypes;

namespace InntecMobileNetMaui.Views.Viatics;

public partial class ViaticsCheckDetailPage : ContentPage
{
    private ViaticsCheckDetailViewModel viewModel;
    /// <summary>
    /// Inicializacion de objetos
    /// </summary>
    /// <param name="param">detalles de consumos</param>
    public ViaticsCheckDetailPage(ConsumptionDetail param)
    {
        InitializeComponent();
        BindingContext = this.viewModel = new ViaticsCheckDetailViewModel(param);

        if (param.StatusComprobacionId == (int)Enumeradores.enumComprobacionEstatus.No_comprobable ||
            param.StatusComprobacionId == (int)Enumeradores.enumComprobacionEstatus.Comprobacion_finalizada)
            stkXml.IsVisible = stkPdf.IsVisible = stkComp.IsVisible = false;

        if (param.ExisteComp)
            stkComp.IsVisible = false;
        if (param.ExistePdf)
            stkPdf.IsVisible = false;
        if (param.ExisteXml)
            stkXml.IsVisible = false;
        
    }
    /// <summary>
    /// Cargar XML
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void XML_Clicked(object sender, EventArgs e)
    {
        PermissionStatus status = await Permissions.RequestAsync<Permissions.StorageWrite>();
        try
        {
            PickOptions pickOptions = new PickOptions();
            pickOptions.PickerTitle = "Selecciona tu archivo XML";
            var fileXML = await FilePicker.PickAsync(pickOptions);

            if (fileXML != null)
            {
                if (!fileXML.FileName.ToUpper().EndsWith("XML"))
                {
                    InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "Alerta!";
                    InformativeViewModel.Instance.Message = "El archivo no tiene la extencion correcta.";
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                    return;
                }
                TxtXML.Text = fileXML.FileName;

                MemoryStream streamXML = new MemoryStream();
                fileXML.OpenReadAsync().Result.CopyTo(streamXML);
                Models.Viatics.FileResult fileResult = await viewModel.SendXML(streamXML.ToArray(), fileXML.FileName).ConfigureAwait(true);

                if (fileResult.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "Alerta!";
                    InformativeViewModel.Instance.Message = fileResult.Msg;
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);

                }
                else
                {

                    InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Error;
                    InformativeViewModel.Instance.Title = "Error!";
                    InformativeViewModel.Instance.Message = fileResult.Msg;
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                }

            }
        }
        catch (FileNotFoundException)
        {

            InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Informative;
            InformativeViewModel.Instance.Title = "Alerta!";
            InformativeViewModel.Instance.Message = "El archivo ya no se encuentra en la ruta que se selecciono.";
            await MopupService.Instance.PushAsync(InformativeAlert.Instance);
            TxtXML.Text = string.Empty;
        }
    }
    /// <summary>
    /// Cargar PDF
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void PDF_Clicked(object sender, EventArgs e)
    {
        PermissionStatus status = await Permissions.RequestAsync<Permissions.StorageWrite>();
        try
        {
            PickOptions pickOptions = new PickOptions();
            pickOptions.FileTypes = FilePickerFileType.Pdf;
            pickOptions.PickerTitle = "Selecciona tu archivo PDF";
            var filePDF = await FilePicker.PickAsync(pickOptions);
            if (filePDF != null)
            {
                if (!filePDF.FileName.ToUpper().EndsWith("PDF"))
                {

                    InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "Alerta!";
                    InformativeViewModel.Instance.Message = "El archivo no tiene la extencion correcta.";
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                    return;
                }
                TxtPDF.Text = filePDF.FileName;
                MemoryStream streamPDF = new MemoryStream();
                filePDF.OpenReadAsync().Result.CopyTo(streamPDF);
                Models.Viatics.FileResult fileResult = await viewModel.SendPDF(streamPDF.ToArray(), filePDF.FileName).ConfigureAwait(true);
                if (fileResult.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                   
                    InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "Alerta!";
                    InformativeViewModel.Instance.Message = fileResult.Msg;
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                }
                else
                {
                   
                    InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Error;
                    InformativeViewModel.Instance.Title = "Error!";
                    InformativeViewModel.Instance.Message = fileResult.Msg;
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                }
            }
        }
        catch (FileNotFoundException)
        {
            
            InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Informative;
            InformativeViewModel.Instance.Title = "Alerta!";
            InformativeViewModel.Instance.Message = "El archivo ya no se encuentra en la ruta que se selecciono.";
            await MopupService.Instance.PushAsync(InformativeAlert.Instance);
            TxtPDF.Text = string.Empty;
        }
    }
    /// <summary>
    /// Cargar Comprobante
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Comprobante_Clicked(object sender, EventArgs e)
    {
        PermissionStatus status = await Permissions.RequestAsync<Permissions.StorageWrite>();
        try
        {
            PickOptions pickOptions = new PickOptions();
            pickOptions.FileTypes = FilePickerFileType.Images;
            pickOptions.PickerTitle = "Selecciona tu archivo de imagen";
            var fileIMG = await FilePicker.PickAsync(pickOptions);

            if (fileIMG != null)
            {
                TxtIMG.Text = fileIMG.FileName;
                MemoryStream streamIMG = new MemoryStream();
                fileIMG.OpenReadAsync().Result.CopyTo(streamIMG);
                Models.Viatics.FileResult fileResult = await viewModel.SendVOUCHER(streamIMG.ToArray(), fileIMG.FileName).ConfigureAwait(true);

                if (fileResult.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {

                    InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "Alerta!";
                    InformativeViewModel.Instance.Message = fileResult.Msg;
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                }
                else
                {   // Hay que mostrar un error relacionado a que tiene que subir un archivo PDF
                    InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Error;
                    InformativeViewModel.Instance.Title = "Error!";
                    InformativeViewModel.Instance.Message = fileResult.Msg;
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                }

            }
        }
        catch (FileNotFoundException)
        {

            InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Informative;
            InformativeViewModel.Instance.Title = "Alerta!";
            InformativeViewModel.Instance.Message = "El archivo ya no se encuentra en la ruta que se selecciono.";
            await MopupService.Instance.PushAsync(InformativeAlert.Instance);

            TxtIMG.Text = string.Empty;
        }
    }
    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }

}