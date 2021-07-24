using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace VismaInternship.UnitTests.RepositoryTests
{
    public class DatabaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanAddNewBook_ValidData_ReturnsTrue()
        {
            List<Models.Book> books = new List<Models.Book>();
            Repository.Database.ReadBooks(ref books);

            Models.Book b = new Models.Book(999, "Haris Poteris", "J. K. Rowling", "Fantastika", "Lietuvių", new DateTime(1997, 06, 27), "9780807286005", "Ailandas Eidintas", "2021-08-21");
            bool response = Repository.Database.Add(b);
            Repository.Database.Write(books);
            Assert.AreEqual(true, response);
            
        }
        [Test]
        public void CanBeDeleted_ValidData_ReturnsTrue()
        {
            Models.Book b = new Models.Book(999, "Haris Poteris", "J. K. Rowling", "Fantastika", "Lietuvių", new DateTime(1997, 06, 27), "9780807286005", "Ailandas Eidintas", "2021-08-21");
            Repository.Database.Add(b);
            int deletedCount = Repository.Database.DeleteBook(999);
            Assert.AreEqual(1, deletedCount);
        }
        [Test]
        public void CanBeDeleted_InvalidId_ReturnsZero()
        {

            int deletedCount = Repository.Database.DeleteBook(888);
            Assert.AreEqual(0, deletedCount);
        }
        [Test]
        public void CanBeBorrowed_ValidData_ReturnsTrue()
        {
            Models.Book b = new Models.Book(999, "Haris Poteris", "J. K. Rowling", "Fantastika", "Lietuvių", new DateTime(1997, 06, 27), "9780807286005", "Ailandas Eidintas", "2021-08-21");
            Repository.Database.Add(b);
            bool response = Repository.Database.Borrow(999, "Ailandas Eidintas", DateTime.Now.AddMonths(1));
            Repository.Database.DeleteBook(999);
            Assert.AreEqual(true, response);

        }
        [Test]
        public void CanBeBorrowed_InvalidReturnDate_ReturnsFalse()
        {
            Models.Book b = new Models.Book(999, "Haris Poteris", "J. K. Rowling", "Fantastika", "Lietuvių", new DateTime(1997, 06, 27), "9780807286005", "Ailandas Eidintas", "2021-08-21");
            Repository.Database.Add(b);
            bool response = Repository.Database.Borrow(999, "Ailandas Eidintas", DateTime.Now.AddMonths(3));
            Repository.Database.DeleteBook(999);
            Assert.AreEqual(false, response);
        }
        [Test]
        public void CanBeReturned_ValidData_ReturnsTrue()
        {
            Models.Book b = new Models.Book(999, "Haris Poteris", "J. K. Rowling", "Fantastika", "Lietuvių", new DateTime(1997, 06, 27), "9780807286005", "Ailandas Eidintas", "2021-08-21");
            Repository.Database.Add(b);
            bool response = Repository.Database.Borrow(999, "Ailandas Eidintas", DateTime.Now.AddMonths(1));
            if(response == false) { Assert.Fail(); }
            response = Repository.Database.ReturnBook(999);
            Repository.Database.DeleteBook(999);
            Assert.AreEqual(true, response);
        }
        [Test]
        public void CanBeReturned_InvalidId_ReturnsFalse()
        {
            bool response = Repository.Database.ReturnBook(999);
            Assert.AreEqual(false, response);
        }
    }
}
