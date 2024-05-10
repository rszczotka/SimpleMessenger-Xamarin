﻿using SimpleMessenger;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class DatabaseService
{
    private SQLiteAsyncConnection _connection;

    public DatabaseService()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Contacts.db3");
        _connection = new SQLiteAsyncConnection(dbPath);
    }

    public async Task InitializeDatabase()
    {
        await _connection.CreateTableAsync<Contact>();
    }

    public async Task AddContact(Contact contact)
    {
        await _connection.InsertAsync(contact);
    }
    public async Task RemoveContact(int userId)
    {
        var contact = await _connection.Table<Contact>().Where(c => c.Id == userId).FirstOrDefaultAsync();
        if (contact != null)
        {
            await _connection.DeleteAsync(contact);
        }
    }
    public async Task<List<Contact>> GetAllContacts()
    {
        return await _connection.Table<Contact>().ToListAsync();
    }

}