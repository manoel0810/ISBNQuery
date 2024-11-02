using ISBNQuery;
using ISBNQuery.Erros;
using SkiaSharp;

namespace ISBNQueryCoreTests
{
    public class ISBNQueryTests
    {
        [Fact]
        public async void ISBN13SearchAsync()
        {
            Assert.True(await Query.SearchBook("978-8551005194") != null);
        }

        [Fact]
        public async Task ISBN10SearchAsync()
        {
            Assert.True(await Query.SearchBook("8551005197") != null);
            Assert.True(await Query.SearchBook("658021001X") != null);
        }

        [Fact]
        public void ISBNParse()
        {
            string isbn13 = "9786580210015";
            string isbn10 = "658021001X";

            Assert.Equal(isbn13, ISBNParser.ParseISBN(isbn10));
            Assert.Equal(isbn10, ISBNParser.ParseISBN(isbn13));
            Assert.Throws<ArgumentNullException>(() => { ISBNParser.ParseISBN(null!); });
            Assert.Throws<FormatExceptionArgument>(() => { ISBNParser.ParseISBN("000000000"); });
        }

        [Fact]
        public async Task SearchCoverAsync()
        {
            Book book = await Query.SearchBook("8551005197");
            var response = await Query.SearchCover(book, ImageSize.L, default);

            Assert.IsType<SKImage>(response);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => { await Query.SearchCover(null!, ImageSize.L, default); });
            await Assert.ThrowsAsync<BookException>(async () => { await Query.SearchCover(new Book(), ImageSize.L, default); });
        }
    }
}