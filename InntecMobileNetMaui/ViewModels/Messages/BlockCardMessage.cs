using CommunityToolkit.Mvvm.Messaging.Messages;
using InntecMobileNetMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Messages
{
    internal class BlockCardMessage : ValueChangedMessage<CardModel>
    {
        public BlockCardMessage(CardModel value) : base(value)
        {
        }
    }
}
