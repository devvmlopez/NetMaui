using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Linq.Expressions;

namespace InntecMobileNetMaui.ViewModels.Alerts
{
    public partial class InformativeViewModel : BaseAlertViewModel
    {
        private static InformativeViewModel instance = null;

        public static InformativeViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InformativeViewModel();
                }
                return instance;
            }
        }

        public enum messageType
        {
            Informative = 1,
            Error,
            Alert,
            Message
        }

        #region Properties

        [ObservableProperty]
        private string _title;

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                SetProperty(ref _message, value);
                OnPropertyChanged(nameof(AnimationPlaying));
            }
        }

        private messageType _messageType;
        public messageType MessageType
        {
            get => _messageType;
            set
            {
                SetProperty(ref _messageType, value);
                OnPropertyChanged(nameof(Ico));
            }
        }

        public bool AnimationPlaying { get => (string.IsNullOrEmpty(Message) ? false : true); }
        public string Ico { get => (MessageType == messageType.Informative) ? "done.gif" : ((MessageType == messageType.Message)) ? "" : "error.gif"; }


        #endregion

        public InformativeViewModel()
        {
        }
    }
}
