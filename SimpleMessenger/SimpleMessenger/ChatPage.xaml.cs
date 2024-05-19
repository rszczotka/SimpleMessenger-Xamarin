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
        private Contact _contact;
        public ChatPage(Contact contact)
        {
            _contact = contact; 
            this.Title = _contact.FullName;
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
            MessageListView.ItemsSource = messages;
            NoMessagesLabel.IsVisible = !messages.Any();
        }

        private void EditMessageButton_Clicked(object sender, EventArgs e)
        {

        }
        private void DeleteMessageButton_Clicked(object sender, EventArgs e)
        {

        }
        private async void SendMessageButton_Clicked(object sender, EventArgs e)
        {
            var messageText = MessageEntry.Text;

            if (!string.IsNullOrEmpty(messageText))
            {
                var message = new Message
                {
                    Text = messageText,
                    SenderId = 0,
                    ReceiverId = _contact.Id,
                };

                await _databaseService.AddMessage(message);

                MessageEntry.Text = string.Empty;

                LoadMessages(_contact);
            }
        }

        private void AddImageButton_Clicked(object sender, EventArgs e)
        {
            // Handle the image icon click event here
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}