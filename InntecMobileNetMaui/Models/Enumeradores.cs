using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    public static class Enumeradores
    {
        /// <summary>
        /// Tipo de validacion para REGEX
        /// </summary>
        public enum EnumValidar
        {
            Contrasenia, Correo, CampoTexto, Telefono_Celular, RFC
        }
        /// <summary>
        /// Lista para el menu principal
        /// </summary>
        public enum enumMenuItemType
        {
            Cards,
            MyData,
            Help,
            ViaticRequest,
            ViaticList,
            ViaticChecking,
            LinkedUser,
            UnLinkUser,
            ActivateCard,
            ActivationRequest,
            MainGas,
            InntecDescuentos,
            ConsultaAclaracion,
            LogOut
        }

        /// <summary>
        /// Estatus para solicitudes de viaticos
        /// </summary>
        public enum enumStatusSolicitud
        {
            NUEVA = 1,
            EN_REVISION = 2,
            EN_ESPERA = 3,
            AUTORIZADA = 4,
            POR_COMPROBAR = 5,
            COMPROBADA = 6,
            CERRADA_COMPROBADA = 7,
            CANCELADA = 8
        }

        /// <summary>
        /// Estatus para Comprobaciones de viaticos
        /// </summary>
        public enum enumComprobacionEstatus
        {
            Todo = 0,
            Por_Comprobar = 1,
            Comprobado_automatico = 2,
            Comprobado_manual = 3,
            Comprobacion_finalizada = 4,
            No_comprobable = 5
        }
        /// <summary>
        /// Tipos de tarjetas (Productos)
        /// </summary>
        public enum enumProductoID
        {
            VIATICOS = 44,
            COMBUSTIBLE_CHIP_PREMIUM = 48,
            COMBUSTIBLE_CHIP_DIESEL = 50,
            COMBUSTIBLE_CHIP_GAS_LP = 52,
            COMBUSTIBLE_CHIP_TURBOSINA = 54
        }

        /// <summary>
        /// Grupos de tarjetas
        /// </summary>
        public enum enumProductoGrupo
        {
            COMBUSTIBLES = 3,
            VIATICOS = 16
        }

        /// <summary>
        /// Estatus de las solicitudes de activacion
        /// </summary>
        public enum enumSolicitudActivacionEstatus
        {
            Todo = 0,
            Registrada = 1,
            Aprobada = 2,
            Declinada = 3,
            Finalizo = 4
        }

        /// <summary>
        /// Preguntas Frecuentes
        /// </summary>
        public enum enumFAQs
        {
            DESPENSA, COMBUSTIBLE, PREMIUM, VIATICOS, TOKEN
        }

        ///<summary>
        /// Manejo de la capara
        /// </summary>
        public enum CameraOptions
        {
            Rear,
            Front
        }

        /// <summary>
        /// Estatus para aclaraciones.
        /// </summary>
        public enum EnumAclaracionEstatus
        {
            Recibida = 1,
            EnRevision,
            EnProceso,
            CobroAclaración,
            Ganada,
            Improcedente,
            CanceladaporUsuario,
            Perdida,
            Finalizada
        }
    }
}
