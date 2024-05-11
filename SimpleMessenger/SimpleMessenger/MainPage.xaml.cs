﻿using System;
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

        private async void AddNewContactButton_Clicked(object sender, EventArgs e)
        {
            var addEditContactPage = new AddEditContactPage();
            addEditContactPage.Disappearing += (s, args) => LoadContacts();
            await Navigation.PushAsync(addEditContactPage);
        }
        private async void EditContactButton_Clicked(object sender, EventArgs e)
        {
            // Edit the contact
            var menuItem = sender as MenuItem;
            var contact = menuItem.CommandParameter as Contact;
            var addEditContactPage = new AddEditContactPage(contact.Id);
            addEditContactPage.Disappearing += (s, args) => LoadContacts();
            await Navigation.PushAsync(addEditContactPage);
        }
        private async void DeleteContactButton_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contact = menuItem.CommandParameter as Contact;
            bool answer = await DisplayAlert("Delete Contact", $"Are you sure you want to delete {contact.FirstName}?", "OK", "Cancel");
            if (answer)
            {
                // Delete the contact
                await _databaseService.RemoveContact(contact.Id);
                LoadContacts();
            }
        }
    }

}
