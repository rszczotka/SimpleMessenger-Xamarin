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

           await _databaseService.RemoveContact(3);

            

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
        //private async void AddNewContact(object sender, EventArgs e)
        private async void AddNewContact(string FirstName, string LastName)
        {
            Contact newContact = new Contact();
            newContact.FirstName = FirstName;
            newContact.LastName = LastName;

            await _databaseService.AddContact(newContact);

            LoadContacts();
        }
    }

}
