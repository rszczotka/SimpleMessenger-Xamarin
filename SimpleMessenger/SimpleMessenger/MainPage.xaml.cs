using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using SQLite;

namespace SimpleMessenger
{
    public partial class MainPage : ContentPage
    {
        private DatabaseService _databaseService;

        public MainPage()
        {
            InitializeComponent();
            InitializeAsync();
        }
        private async void InitializeAsync()
        {
            _databaseService = new DatabaseService();
            await _databaseService.InitializeDatabase();

            LoadContacts();
        }

        private async void LoadContacts()
        {
            var contacts = await _databaseService.GetAllContacts();

            if (contacts.Count == 0)
            {
                ContactListView.ItemsSource = new List<string> { "No contacts found" };
            }
            else
            {
                ContactListView.ItemsSource = contacts;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddContactPage());
        }
    }

}
