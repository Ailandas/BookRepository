using System;
using System.Collections.Generic;
using ConsoleTables;
using System.IO;
using System.Text;
using CommandDotNet;
using System.Linq;

namespace VismaInternship
{
    public class BookLibrary
    {
        public void Add(string name, string author, string category, string language, DateTime publicationDate, string isbn)
        {
            List<Models.Book> books = new List<Models.Book>();
            bool resp = Repository.Database.ReadBooks(ref books);
            if (resp == false) { return; }
            int bookId = 1;
            if (books.Count > 0)
            {
                bookId = books[books.Count - 1].ID + 1;
            }
            Models.Book book = new Models.Book(bookId, name, author, category, language, publicationDate, isbn,null,null);
            bool response = Repository.Database.Add(book);
        }
        public void List(string filter, string filterValue)
        {
            List<Models.Book> books = new List<Models.Book>();
            bool response = Repository.Database.ReadBooks(ref books);
            if (response == false) { return; }
            if (filter != null)
            {
                FilterList(filter, filterValue, ref books);
            }
            ConsoleTable
                .From(books)
                .Configure(o => o.NumberAlignment = Alignment.Right)
                .Write(Format.Alternative);
        }
        public void Delete(int ID)
        {
            if (ID == -1) { return; }
            int response = Repository.Database.DeleteBook(ID);
            if (response == 0) { return; }
        }
        public void Borrow(int BookId, string fullname, DateTime returnDate)
        {
            if (BookId == -1 || fullname == null || returnDate == DateTime.MinValue) { return; }
            bool response = Repository.Database.Borrow(BookId,fullname,returnDate);
            if(response == false) { return; }
        }
        public void ReturnBook(int BookId)
        {
            if (BookId == -1) { return; }
            bool response = Repository.Database.ReturnBook(BookId);
            if(response == false) { return; }
        }
        private void FilterList(string filter, string filterValue, ref List<Models.Book> books)
        {
            switch (filter.ToLower())
            {
                case "name":
                    books = books.Where(x => x.name.ToLower() == filterValue.ToLower()).ToList();
                    break;
                case "author":
                    books = books.Where(x => x.author.ToLower() == filterValue.ToLower()).ToList();
                    break;
                case "category":
                    books = books.Where(x => x.category.ToLower() == filterValue.ToLower()).ToList();
                    break;
                case "language":
                    books = books.Where(x => x.language.ToLower() == filterValue.ToLower()).ToList();
                    break;
                case "publicationdate":
                    books = books.Where(x => x.publicationDate.ToString("yyyy-MM-dd") == filterValue).ToList();
                    break;
                case "isbn":
                    books = books.Where(x => x.isbn.ToLower() == filterValue.ToLower()).ToList();
                    break;
                case "taken":
                    books = books.Where(x => x.borrowedBy != null).ToList();
                    break;
                case "available":
                    books = books.Where(x => x.borrowedBy == null).ToList();
                    break;
                default:
                    break;
            }
        }

       
    }
}
