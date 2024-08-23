using CommunityToolkit.Mvvm.Messaging.Messages;
using InntecMobileNetMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Messages
{
    public class NewCardMessage : ValueChangedMessage<CardModel>
    {
        public NewCardMessage(CardModel value) : base(value)
        {
        }
    }
}
