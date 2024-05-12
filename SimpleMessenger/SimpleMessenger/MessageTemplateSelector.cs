using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SimpleMessenger
{
    public class MessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate IncomingDataTemplate { get; set; }
        public DataTemplate OutgoingDataTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = item as Message;
            if (message == null)
                return null;

            // Replace "currentUserId" with the ID of the current user
            return message.SenderId == currentUserId ? OutgoingDataTemplate : IncomingDataTemplate;
        }
    }

}
