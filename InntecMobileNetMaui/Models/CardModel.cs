using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    public class CardModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Monitor de cambio de propiedades
        /// </summary>
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

        /// <summary>
        /// Desencadenador de asigancion de propiedades
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingStore"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <param name="onChanged"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Indice de tarjeta
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Identificador unico de tarjeta
        /// </summary>
        public int UsuarioCsmTarjetaId { get; set; }
        /// <summary>
        /// Saldo de la tarjeta
        /// </summary>
        public string Balance { get; set; }
        /// <summary>
        /// Numero de tarjeta
        /// </summary>
        public string Tarjeta { get; set; }
        [JsonIgnore]
        public string TarjetaF => Tarjeta.Substring(15, 4).Replace('X', '*');
        /// <summary>
        /// Vigencia de tarjeta
        /// </summary>
        public string Vigencia => Mes + "/" + Anio;
        public string TokenUnico { get; set; }
        public string Mes { get; set; }
        public string Anio { get; set; }
        /// <summary>
        /// Estatatus de la tarjeta
        /// </summary>
        private int estatus;
        /// <summary>
        /// Monitoreo de estatus
        /// </summary>
        public int Estatus
        {
            get => estatus;
            set
            {
                estatus = value;
                SetProperty(ref estatus, value);
                OnPropertyChanged(nameof(BVisible));
            }
        }
        /// <summary>
        /// Indicador del estatus de una tarjeta
        /// </summary>
        public bool BEstatus => EstatusDescripcion != null && EstatusDescripcion.Equals("Activa") ? true : false;

        /// <summary>
        /// Indicador Visual para tarjetas(Ocultar)
        ///      25 Bloqueada por inntec
        ///      27 Cerrada, Cancelada por el cliente
        ///      99 Prevencion
        ///      98 BloqueadaCombustible
        /// </summary>
        public bool BVisible => (Estatus == 25 || Estatus == 27 || Estatus == 99 || Estatus == 98) ? false : true;

        public bool EstatusEcommerce { get; set; }

        public bool? PuedeCambiarEcommerce { get; set; }

        /// <summary>
        /// Color que indica el estatus de la tarjeta(Encabezado)
        /// </summary>
        public string Color =>
            (EstatusDescripcion != null && EstatusDescripcion.Equals("Activa")) ? "#009945" :
            (EstatusDescripcion.Equals("Bloqueada")) ? "#E6B64F" : "#cc001f";

        /// <summary>
        /// Descripcion del estatus
        /// </summary>
        private string estatusDescripcion;
        /// <summary>
        /// Monitoreo del estatus
        /// </summary>
        public string EstatusDescripcion
        {
            get => estatusDescripcion;
            set
            {
                estatusDescripcion = value;
                SetProperty(ref estatusDescripcion, value);
                OnPropertyChanged(nameof(Color));
                OnPropertyChanged(nameof(BEstatus));
            }
        }
        /// <summary>
        /// Tipo de tarjeta
        /// </summary>
        public int Producto { get; set; }
        /// <summary>
        /// Identificador del producto
        /// </summary>
        public int ProductoID { get; set; }
        /// <summary>
        /// Imagen a mostrar
        /// </summary>
        public string ImgProd { get; set; }

        public string Marca => ((string.IsNullOrEmpty(ImgProd)) ? "" : (ImgProd.Contains("MC_")) ? "mastercard.png" : "carnet.png");

        public string ColorTarjeta => (string.IsNullOrEmpty(ImgProd)) ? "#85B2DC" : (ImgProd.Contains("MC_PRE")) ? "#313739" : (ImgProd.Contains("MC_REC") || ImgProd.Contains("MC_REC")) ? "#AF71DB" : (ImgProd.Contains("CR_DES") || ImgProd.Contains("CR_COM")) ? "#1A95C6" : (ImgProd.Contains("MC_VIA")) ? "#149A59" : "#85B2DC";

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// Saldo de la tarjeta
        /// </summary>
        public decimal saldo { get; set; }
        /// <summary>
        /// Mensaje
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Estatus de la respuesta del servidor
        /// </summary>
        public HttpStatusCode StatusCode { get; internal set; }

        /// <summary>
        /// Complemento para tarjeta de Combustible
        /// </summary>
        public ProductComplement Complemento { get; set; }


    }

    /// <summary>
    /// Complemento de tarjeta
    /// </summary>
    public class ProductComplement : INotifyPropertyChanged
    {
        /// <summary>
        /// Grupo al que pertenece la tarjeta
        /// </summary>
        public int ProductoGrupoId { get; set; }

        /// <summary>
        /// Indicador para existencia de QR
        /// </summary>
        private int? qrId;

        /// <summary>
        /// Monitor de cambio de propiedades
        /// </summary>
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

        /// <summary>
        /// Desencadenador de asigancion de propiedades
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingStore"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <param name="onChanged"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Monitoreo de QR para eliminar o agregar
        /// </summary>
        public int? QrId
        {
            get => qrId;
            set
            {
                qrId = value;
                SetProperty(ref qrId, value);
                OnPropertyChanged(nameof(LinkQr));
                OnPropertyChanged(nameof(UnLinkQr));
            }
        }
        /// <summary>
        /// Identificador de la tarjeta
        /// </summary>
        public int? TarjetaId { get; set; }
        /// <summary>
        /// Indicador para agregar un QR a tarjeta
        /// </summary>
        public bool LinkQr => (QrId == null || QrId == 0) ? true : false;

        /// <summary>
        /// Indicador para eliminar un QR a tarjeta
        /// </summary>
        public bool UnLinkQr => (QrId == null || QrId == 0) ? false : true;
    }
}
