using ISBNQuery;
using ISBNQuery.Erros;
using ISBNQuery.Models;
using SkiaSharp;

namespace ISBNQueryTests
{
    public class QueryTests
    {
        [Fact]
        public async Task ISBN13SearchAsync()
        {
            Book book = await Query.SearchBookAsync("978-8551005194");
            Assert.NotNull(book);
            Assert.False(string.IsNullOrWhiteSpace(book.Title));
        }

        [Fact]
        public async Task ISBN10SearchAsync()
        {
            Book book1 = await Query.SearchBookAsync("8551005197");
            Assert.NotNull(book1);

            Book book2 = await Query.SearchBookAsync("658021001X");
            Assert.NotNull(book2);
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
            Book book = await Query.SearchBookAsync("8551005197");
            var response = await Query.SearchCover(book, ImageSize.L, default);

            Assert.IsType<SKImage>(response);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => { await Query.SearchCover(null!, ImageSize.L, default); });
            await Assert.ThrowsAsync<BookException>(async () => { await Query.SearchCover(new Book { ThumbnailUrl = null }, ImageSize.L, default); });
        }

        [Fact]
        public async Task SearchCoverByInfoAsync()
        {
            var coverInfo = new CoverInfo(CoverKeyType.Isbn, "9788551005194", ImageSize.M);
            var image = await Query.SearchCoverAsync(coverInfo);
            Assert.NotNull(image);
        }

        [Fact]
        public async Task SearchAuthorAsync()
        {
            // J.K. Rowling OL26320A na Open Library
            AuthorInfo author = await Query.SearchAuthorAsync("OL26320A");
            Assert.NotNull(author);
            Assert.False(string.IsNullOrWhiteSpace(author.Name));
        }

        [Fact]
        public async Task SearchAuthorsByNameAsync()
        {
            var authors = await Query.SearchAuthorsByNameAsync("Tolkien");
            Assert.NotNull(authors);
            Assert.NotEmpty(authors);
        }
    }
}
