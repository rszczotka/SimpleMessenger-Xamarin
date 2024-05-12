using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleMessenger
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        private DatabaseService _databaseService;
        public ChatPage(Contact contact)
        {
            this.Title = contact.FullName;
            InitializeComponent();
            InitializeAsync();
            LoadMessages(contact);


        }
        private async void InitializeAsync()
        {
            _databaseService = new DatabaseService();
            await _databaseService.InitializeDatabase();
        }

        private async void LoadMessages(Contact contact)
        {
            var messages = await _databaseService.GetChatHistory(contact.Id);

            if (messages.Count == 0)
            {
                MessageListView.ItemsSource = new List<string> { "No messages found" };
            }
            else
            {
                MessageListView.ItemTemplate = new MessageTemplateSelector
                {
                    IncomingDataTemplate = (DataTemplate)Resources["IncomingDataTemplate"],
                    OutgoingDataTemplate = (DataTemplate)Resources["OutgoingDataTemplate"]
                };

            }
        }

    }
}