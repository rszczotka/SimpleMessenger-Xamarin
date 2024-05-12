using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMessenger
{
    public class Contact
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $" {Id} {FirstName} {LastName}";
            }
        }
    }
}
