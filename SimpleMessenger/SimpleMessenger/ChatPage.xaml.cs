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
            MessageListView.ItemsSource = messages;
            NoMessagesLabel.IsVisible = !messages.Any();
        }

        private void EditMessageButton_Clicked(object sender, EventArgs e)
        {

        }
        private void DeleteMessageButton_Clicked(object sender, EventArgs e)
        {

        }
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}