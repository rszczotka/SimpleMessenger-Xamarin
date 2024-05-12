using SimpleMessenger;
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
        await _connection.CreateTableAsync<Message>();
    }

    public async Task AddContact(Contact contact)
    {
        await _connection.InsertAsync(contact);
    }
    public async Task EditContact(Contact contact)
    {
        var existingContact = await _connection.Table<Contact>().Where(c => c.Id == contact.Id).FirstOrDefaultAsync();
        if (existingContact != null)
        {
            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            await _connection.UpdateAsync(existingContact);
        }
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
    public async Task<Contact> GetContactById(int userId)
    {
        return await _connection.Table<Contact>().Where(c => c.Id == userId).FirstOrDefaultAsync();
    }
    public async Task<List<Message>> GetChatHistory(int userId)
    {
        return await _connection.Table<Message>().Where(m => m.ReceiverId == userId || m.SenderId == userId).ToListAsync();
    }
    public async Task FillTestChatHistory()
    {
        var messages = new List<Message>
        {
            new Message { Text = "Hello", Date = DateTime.Now, SenderId = 1, ReceiverId = 2 },
            new Message { Text = "Hi", Date = DateTime.Now, SenderId = 2, ReceiverId = 1 },
            new Message { Text = "How are you?", Date = DateTime.Now, SenderId = 1, ReceiverId = 2 },
            new Message { Text = "I'm fine, thank you", Date = DateTime.Now, SenderId = 2, ReceiverId = 1 },
            new Message { Text = "Goodbye", Date = DateTime.Now, SenderId = 1, ReceiverId = 2 },
            new Message { Text = "Bye", Date = DateTime.Now, SenderId = 2, ReceiverId = 1 }
        };

        await _connection.InsertAllAsync(messages);
    }
}
