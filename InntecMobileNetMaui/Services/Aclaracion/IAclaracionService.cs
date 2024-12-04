using InntecMobileNetMaui.Models.Aclaraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services.Aclaracion
{
    public interface IAclaracionService
    {
        Task<AclaracionModel> GuardarAclaracion(AclaracionModel aclaracionModel);
        Task<AclaracionModel> GuardarArchivos(Dictionary<string, byte[]> Archivos, int AclaracionId);
        Task ActualizarStatus(int AclaracionId);

        Task<List<AclaracionModel>> ObtenerAclaraciones();
        Task<List<DetalleAclaracionModel>> ObtenerAclaracionDetalle(int AclaracionId);
        Task GenerarPDFs(int AclaracionId, Dictionary<string, byte[]> Imagenes, DatosDocumento datosDocumento);

        Task<bool> VerificarArchivosAclaracion(int AclaracionId);
        Task<bool> VerificarPermisoCancelar(int AclaracionId);

        Task<CancelarAclaracionModel> CancelarAclaracion(int AclaracionId);
        Task<byte[]> DescargarDocumentos(int AclaracionId);
    }
}
