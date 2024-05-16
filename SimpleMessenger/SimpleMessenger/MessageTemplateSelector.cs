using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SimpleMessenger
{
    public class MessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OutgoingMessageTemplate { get; set; }
        public DataTemplate IncomingMessageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = item as Message;
            if (message == null)
                return null;

            return message.SenderId == 0 ? OutgoingMessageTemplate : IncomingMessageTemplate;
        }
    }

}
