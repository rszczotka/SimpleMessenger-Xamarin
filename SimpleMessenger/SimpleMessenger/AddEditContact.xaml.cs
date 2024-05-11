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
    public partial class AddEditContactPage : ContentPage
    {
        private DatabaseService _databaseService;

        public AddEditContactPage(int userId = 0)
        {
            InitializeComponent();
            InitializeAsync();
            if (userId != 0)
            {
                FillContactDetails(userId);
            }
        }
        private async void InitializeAsync()
        {
            _databaseService = new DatabaseService();
            await _databaseService.InitializeDatabase();
        }

        private async void FillContactDetails(int userId)
        {
            Contact contact = await _databaseService.GetContactById(userId);
            firstNameEntry.Text = contact.FirstName;
            lastNameEntry.Text = contact.LastName;
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