using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace VismaInternship.Repository
{
    public static class Database
    {
        private static string databasePath = Directory.GetCurrentDirectory() + "\\books.json";
        private static int maxBorrowMonthCount = 2;
        public static bool Add(Models.Book book)
        {
            List<Models.Book> books = new List<Models.Book>();
            bool response = ReadBooks(ref books);
            books.Add(book);
            Write(books);

            return true;
        }
        public static bool Write<T>(T books)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(books, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                File.WriteAllText(databasePath, json);
                return true;
            }
            catch(Exception exc)
            {
                return false;
            }
            
        }
        public static bool ReadBooks(ref List<Models.Book> books)
        {
            try
            {
                using (StreamReader r = new StreamReader(databasePath))
                {
                    string json = r.ReadToEnd();
                    books = JsonConvert.DeserializeObject<List<Models.Book>>(json);
                }
                return true;

            }catch(Exception exc)
            {
                return false;
            }
        }

        public static bool Borrow(int bookId,string fullname, DateTime returnDate)
        {
            DateTime today = DateTime.Now;
            if (returnDate > today.AddMonths(maxBorrowMonthCount) || returnDate.Date <= today.Date) { return false; }
           
            List<Models.Book> books = new List<Models.Book>();
            bool response = ReadBooks(ref books);
            if (response == false) { return false; }
            int borrowCount = GetBorrowCount(books, fullname);

            if(borrowCount >= 3) { return false; }
            
            foreach(Models.Book book in books)
            {
                if (book.ID == bookId && book.borrowedBy == null) //In case someone already took it
                {
                    book.borrowedBy = fullname;
                    book.borrowedTill = returnDate.ToString("yyyy-MM-dd");
                }
            }
            Write(books);
            return true;

        }
        public static bool ReturnBook(int bookId)
        {

            List<Models.Book> books = new List<Models.Book>();
            bool response = ReadBooks(ref books);
            if (response == false) { return false; }
            bool isFoundAndDeleted = false;
            DateTime today = DateTime.Now;
            foreach (Models.Book book in books)
            {
                if(book.ID == bookId)
                {
                    DateTime returnDate = DateTime.MinValue;
                    if(DateTime.TryParse(book.borrowedTill, out returnDate))
                    {
                        if (today.Date > DateTime.Parse(book.borrowedTill))//Late
                        {
                            Console.WriteLine("a funny message");
                        }
                    }
                    isFoundAndDeleted = true;
                    book.borrowedBy = null;
                    book.borrowedTill = null;
                }
            }
            Write(books);
            return isFoundAndDeleted;
        }
        public static int DeleteBook(int ID)
        {
            List<Models.Book> books = new List<Models.Book>();
            bool response = ReadBooks(ref books);
            int removedCounter = 0;
            if (books.Count > 0)
            {
                
                removedCounter = books.RemoveAll(x => x.ID == ID);
                Write(books);
                return removedCounter;
            }
            else
            {
                return removedCounter;
            }
        }
        private static int GetBorrowCount(List<Models.Book> books, string fullname)
        {
            int borrowCount = 0;
            foreach(Models.Book book in books)
            {
                if(book.borrowedBy == fullname)
                {
                    borrowCount++;
                }
            }
            return borrowCount;
        }
      
    }
}
