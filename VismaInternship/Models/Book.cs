using System;
using System.Collections.Generic;
using System.Text;

namespace VismaInternship.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public string category { get; set; }
        public string language { get; set; }
        public DateTime publicationDate { get; set; }
        public string isbn { get; set; }
        public string borrowedBy { get; set; }
        public string borrowedTill { get; set; }

        public Book(int ID,string name, string author, string category, string language, DateTime publicationDate, string isbn, string borrowedBy, string borrowedTill)
        {
            this.ID = ID;
            this.name = name;
            this.author = author;
            this.category = category;
            this.language = language;
            this.publicationDate = publicationDate;
            this.isbn = isbn;
            this.borrowedBy = borrowedBy;
            this.borrowedTill = borrowedTill;

        }
    }
}
