//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

//namespace SimpleMessenger
//{
//    [XamlCompilation(XamlCompilationOptions.Compile)]
//    public partial class ChatPage : ContentPage
//    {
//        public ChatPage(Contact contact)
//        {
//            Title = "Chat";

//            var nameLabel = new Label();
//            nameLabel.SetBinding(Label.TextProperty, "Name");

//            var backButton = new Button { Text = "Back" };
//            backButton.Clicked += async (s, e) =>
//            {
//                // Navigate back.
//                await Navigation.PopAsync();
//            };

//            Content = new StackLayout
//            {
//                Children = { nameLabel, backButton }
//            };

//            BindingContext = contact;
//        }
//    }
//}