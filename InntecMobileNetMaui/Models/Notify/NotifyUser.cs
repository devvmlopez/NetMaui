using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Notify
{
    public class NotifyUser : INotifyPropertyChanged
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

        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        private int notificacionEstatusId;
        public int NotificacionEstatusId
        {
            get => notificacionEstatusId;
            set
            {
                notificacionEstatusId = value;
                SetProperty(ref notificacionEstatusId, value);
                OnPropertyChanged(nameof(Leida));
            }
        }

        public string Estatus { get; set; }

        public string Leida => (NotificacionEstatusId == 5) ? "Rojo.png" : "";

        public int NotificacionId { get; set; }
        public int NotificacionEnviadaId { set; get; }

        public bool LetraNegra { get; set; }

        public string ColorLetra => (LetraNegra) ? "#000000" : "#FFFFFF";
        public bool Franja => !LetraNegra;
        public string Imagen { get; set; }
        public double Alto { set; get; }
        public double Dencidad { set; get; }
    }
}