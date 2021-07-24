using NUnit.Framework;
using System;

namespace VismaInternship.UnitTests.ModelTests
{
    class BookTest
    {
        public class Test
        {
            [SetUp]
            public void Setup()
            {
            }

            [Test]
            public void CanBeCreated_ValidData_ReturnsTrue()
            {
                Models.Book b = new Models.Book(1, "Haris Poteris", "J. K. Rowling", "Fantastika", "Lietuvių", new DateTime(1997, 06, 27), "9780807286005", "Ailandas Eidintas", "2021-08-21");
                if(b.ID != -1)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
                
            }
            [Test]
            public void CanBeCreated_WithoutBorrowing_ReturnsTrue()
            {
                Models.Book b = new Models.Book(1, "Haris Poteris", "J. K. Rowling", "Fantastika", "Lietuvių", new DateTime(1997, 06, 27), "9780807286005", null,null);
                if (b.ID != -1)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }
        }
    }
}
