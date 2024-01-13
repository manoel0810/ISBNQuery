using ISBNQuery;
using ISBNQuery.Erros;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace ISBNQueryTeste
{
    [TestClass]
    public class ISBNTestes
    {
        [TestMethod]
        public void ISBN13Search()
        {
            Assert.IsTrue(Query.SearchBook("978-8551005194") != null);
        }

        [TestMethod]
        public void ISBN10Search()
        {
            Assert.IsTrue(Query.SearchBook("8551005197") != null);
            Assert.IsTrue(Query.SearchBook("658021001X") != null);
        }

        [TestMethod]
        public void ISBNParse()
        {
            string isbn13 = "9786580210015";
            string isbn10 = "658021001X";

            Assert.AreEqual(isbn13, ISBNParser.ParseISBN(isbn10));
            Assert.AreEqual(isbn10, ISBNParser.ParseISBN(isbn13));
            Assert.ThrowsException<ArgumentNullException>(() => { ISBNParser.ParseISBN(null); });
            Assert.ThrowsException<FormatExceptionArgument>(() => { ISBNParser.ParseISBN("000000000"); });
        }

        [TestMethod]
        public void SearchCover()
        {
            Book book = Query.SearchBook("8551005197");
            Assert.IsInstanceOfType(Query.SearchCover(book, ImageSize.L), typeof(Image));

            Assert.ThrowsException<ArgumentNullException>(() => { Query.SearchCover(null, ImageSize.L); });
            Assert.ThrowsException<BookException>(() => { Query.SearchCover(new Book(), ImageSize.L); });
        }
    }
}
