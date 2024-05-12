using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMessenger
{
    public class Message
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
