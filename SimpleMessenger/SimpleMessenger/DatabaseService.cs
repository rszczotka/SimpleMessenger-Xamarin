using SimpleMessenger;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;


public class DatabaseService
{
    private SQLiteAsyncConnection _connection;

    public DatabaseService()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SimpleMessenger.db3");
        _connection = new SQLiteAsyncConnection(dbPath);
    }
    public void DeleteDatabase()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SimpleMessenger.db3");
        if (File.Exists(dbPath))
        {
            File.Delete(dbPath);
        }
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
    public async Task DeleteContact(int userId)
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
    public async Task AddMessage(Message message)
    {
        await _connection.InsertAsync(message);
    }
    public async Task DeleteMessage(Message message)
    {
        var found_message = await _connection.Table<Message>().Where(c => c.Id == message.Id).FirstOrDefaultAsync();        
        if (found_message != null)
        {
            await _connection.DeleteAsync(found_message);
        }
    }




    //additional methods for debugging
    public async Task FillTestChatHistory()
    {
        var messages = new List<Message>
        {
            new Message { Text = "Hello", Date = DateTime.Now, SenderId = 0, ReceiverId = 1 },
            new Message { Text = "Hi", Date = DateTime.Now, SenderId = 1, ReceiverId = 0 },
            new Message { Text = "How are you?", Date = DateTime.Now, SenderId = 0, ReceiverId = 1 },
            new Message { Text = "I'm fine, thank you", Date = DateTime.Now, SenderId = 1, ReceiverId = 0 },
            new Message { Text = "Goodbye", Date = DateTime.Now, SenderId = 0, ReceiverId = 1 },
            new Message { Text = "Bye", Date = DateTime.Now, SenderId = 1, ReceiverId = 0 }
        };

        await _connection.InsertAllAsync(messages);
    }
    public async Task PrintAllMessages()
    {
        var messages = await _connection.Table<Message>().ToListAsync();

        foreach (var message in messages)
        {
            Debug.WriteLine($"Id: {message.Id}, Text: {message.Text}, Date: {message.Date}, SenderId: {message.SenderId}, ReceiverId: {message.ReceiverId}");
        }
    }
    
    public async Task PrintAllContacts()
    {
        var contacts = await _connection.Table<Contact>().ToListAsync();

        foreach (var contact in contacts)
        {
            Debug.WriteLine($"Id: {contact.Id}, FirstName: {contact.FirstName}, LastName: {contact.LastName}");
        }
    }
    public async Task ClearAllChatHistory()
    {
        await _connection.DeleteAllAsync<Message>();
    }

}
