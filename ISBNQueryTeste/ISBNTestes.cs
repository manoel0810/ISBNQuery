using ISBNQuery;
using ISBNQuery.Erros;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Threading.Tasks;

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
        public async Task ISBN10SearchAsync()
        {
            Assert.IsTrue(await Query.SearchBook("8551005197") != null);
            Assert.IsTrue(await Query.SearchBook("658021001X") != null);
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
        public async Task SearchCoverAsync()
        {
            Book book = await Query.SearchBook("8551005197");
            Assert.IsInstanceOfType(await Query.SearchCover(book, ImageSize.L), typeof(Image));

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => { await Query.SearchCover(null, ImageSize.L); });
            await Assert.ThrowsExceptionAsync<BookException>(async () => { await Query.SearchCover(new Book(), ImageSize.L); });
        }
    }
}
