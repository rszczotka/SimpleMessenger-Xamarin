using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleMessenger
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddContactPage : ContentPage
    {
        private DatabaseService _databaseService;

        public AddContactPage()
        {
            InitializeComponent();
            InitializeAsync();
        }
        private async void InitializeAsync()
        {
            _databaseService = new DatabaseService();
            await _databaseService.InitializeDatabase();
        }

        private async void SaveContactButton_OnClicked(object sender, EventArgs e)
        {
            var contact = new Contact
            {
                FirstName = firstNameEntry.Text,
                LastName = lastNameEntry.Text
            };


            await _databaseService.AddContact(contact);
            await Navigation.PopAsync();
        }
    }
}