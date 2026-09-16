using ISBNQuery;
using ISBNQuery.Models;
using SkiaSharp;
using System.Text;

namespace ISBNQueryDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "ISBNQuery Demo (.NET 10.0)";

            PrintHeader("DEMONSTRAÇÃO DO ISBNQUERY v2.0 - RUNTIME .NET 10.0");

            try
            {
                // 1. Validação e Conversão de ISBN
                TestIsbnParsing();

                // 2. Consulta de Livro por ISBN-13
                await TestBookSearchAsync("978-8551005194");

                // 3. Consulta de Livro por ISBN-10
                await TestBookSearchAsync("8551005197");

                // 4. Consulta de Autor por Chave OpenLibrary (OLID)
                await TestAuthorSearchByKeyAsync("OL26320A");

                // 5. Pesquisa de Autor por Nome
                await TestAuthorSearchByNameAsync("Tolkien");

                // 6. Download e Manipulação de Capas de Livros
                await TestCoverDownloadAsync("9788551005194");

                // 7. Download de Foto de Autor
                await TestAuthorPhotoDownloadAsync("OL26320A");

                PrintHeader("TODOS OS TESTES DE FUNCIONALIDADES FORAM CONCLUÍDOS COM SUCESSO!");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[ERRO CRÍTICO]: {ex.Message}");
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
        }

        private static void TestIsbnParsing()
        {
            PrintSubHeader("1. VALIDAÇÃO E CONVERSÃO DE ISBN");

            string isbn10 = "658021001X";
            string isbn13 = "9786580210015";

            string convertedTo13 = ISBNParser.ParseISBN(isbn10);
            string convertedTo10 = ISBNParser.ParseISBN(isbn13);

            Console.WriteLine($"  ISBN-10 Original...: {isbn10}");
            Console.WriteLine($"  Convertido para 13.: {convertedTo13} (Esperado: {isbn13})");
            Console.WriteLine($"  ISBN-13 Original...: {isbn13}");
            Console.WriteLine($"  Convertido para 10.: {convertedTo10} (Esperado: {isbn10})");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ✓ Conversão de ISBN validada com sucesso!");
            Console.ResetColor();
        }

        private static async Task TestBookSearchAsync(string isbn)
        {
            PrintSubHeader($"2. CONSULTA DE LIVRO POR ISBN: {isbn}");

            Console.WriteLine($"  Buscando livro na Open Library API para ISBN {isbn}...");
            var startTime = DateTime.Now;
            Book book = await Query.SearchBookAsync(isbn);
            var elapsed = DateTime.Now - startTime;

            Console.WriteLine($"  Tempo de requisição: {elapsed.TotalMilliseconds:N0} ms\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  Título...........: {book.Title}");
            if (!string.IsNullOrWhiteSpace(book.SubTitle))
                Console.WriteLine($"  Subtítulo........: {book.SubTitle}");

            Console.WriteLine($"  Autor(es)........: {book.Author}");
            Console.WriteLine($"  Editora(s).......: {book.Publishers}");
            Console.WriteLine($"  Data Publicação..: {book.Publish_Date}");
            Console.WriteLine($"  ISBN-10..........: {book.ISBN10}");
            Console.WriteLine($"  ISBN-13..........: {book.ISBN13}");
            Console.WriteLine($"  Formato Físico...: {book.Physical_Format}");
            Console.WriteLine($"  Nº de Páginas....: {book.NumberOfPages}");
            Console.WriteLine($"  Possui Capa?.....: {book.HasCover}");
            Console.WriteLine($"  Edition Key (OLID): {book.EditionKey}");
            Console.WriteLine($"  Work Key.........: {book.WorkKey}");

            if (book.Subjects != null && book.Subjects.Count > 0)
            {
                Console.WriteLine($"  Assuntos/Tópicos.: {string.Join(", ", book.Subjects.Take(5))}");
            }

            if (!string.IsNullOrWhiteSpace(book.Description))
            {
                string shortDesc = book.Description.Length > 150 ? book.Description.Substring(0, 150) + "..." : book.Description;
                Console.WriteLine($"  Descrição........: {shortDesc.Replace("\n", " ")}");
            }

            Console.ResetColor();
        }

        private static async Task TestAuthorSearchByKeyAsync(string authorKey)
        {
            PrintSubHeader($"3. CONSULTA DE AUTOR POR CHAVE: {authorKey}");

            Console.WriteLine($"  Buscando autor {authorKey} na Open Library...");
            AuthorInfo author = await Query.SearchAuthorAsync(authorKey);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  Chave OLID.......: {author.Key}");
            Console.WriteLine($"  Nome.............: {author.Name}");
            if (!string.IsNullOrWhiteSpace(author.PersonalName))
                Console.WriteLine($"  Nome Pessoal.....: {author.PersonalName}");
            Console.WriteLine($"  Data Nascimento..: {author.BirthDate ?? "N/A"}");
            Console.WriteLine($"  Data Falecimento.: {author.DeathDate ?? "N/A"}");
            Console.WriteLine($"  Possui Fotos?....: {author.HasPhoto}");

            if (author.PhotoIds != null && author.PhotoIds.Count > 0)
            {
                Console.WriteLine($"  Foto IDs.........: {string.Join(", ", author.PhotoIds)}");
            }

            if (!string.IsNullOrWhiteSpace(author.Bio))
            {
                string bioText = author.Bio.Length > 150 ? author.Bio.Substring(0, 150) + "..." : author.Bio;
                Console.WriteLine($"  Biografia........: {bioText.Replace("\n", " ")}");
            }

            Console.ResetColor();
        }

        private static async Task TestAuthorSearchByNameAsync(string nameQuery)
        {
            PrintSubHeader($"4. PESQUISA DE AUTORES POR NOME: \"{nameQuery}\"");

            var authors = await Query.SearchAuthorsByNameAsync(nameQuery);
            Console.WriteLine($"  Total de autores encontrados: {authors.Count}\n");

            foreach (var a in authors.Take(4))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"  • [{a.Key}] {a.Name} (Nascido: {a.BirthDate ?? "N/A"}) - Obras: {a.WorkCount ?? 0} | Principal: {a.TopWork ?? "N/A"}");
            }
            Console.ResetColor();
        }

        private static async Task TestCoverDownloadAsync(string isbn)
        {
            PrintSubHeader($"5. DOWNLOAD E MANIPULAÇÃO DE CAPA: ISBN {isbn}");

            var coverInfo = new CoverInfo(CoverKeyType.Isbn, isbn, ImageSize.L);
            Console.WriteLine($"  URL da Capa......: {coverInfo.Url}");

            using SKImage coverImage = await Query.SearchCoverAsync(coverInfo);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  ✓ Imagem baixada com sucesso!");
            Console.WriteLine($"  Dimensões........: {coverImage.Width} x {coverImage.Height} pixels");

            string fileName = "test_book_cover.png";
            using var data = coverImage.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(fileName);
            data.SaveTo(stream);
            Console.WriteLine($"  ✓ Capa salva localmente como '{fileName}'");
            Console.ResetColor();
        }

        private static async Task TestAuthorPhotoDownloadAsync(string authorKey)
        {
            PrintSubHeader($"6. DOWNLOAD DE FOTO DE AUTOR: {authorKey}");

            using SKImage authorPhoto = await Query.SearchAuthorPhotoAsync(authorKey, ImageSize.L);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  ✓ Foto do Autor baixada com sucesso!");
            Console.WriteLine($"  Dimensões........: {authorPhoto.Width} x {authorPhoto.Height} pixels");

            string fileName = "test_author_photo.png";
            using var data = authorPhoto.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(fileName);
            data.SaveTo(stream);
            Console.WriteLine($"  ✓ Foto salva localmente como '{fileName}'");
            Console.ResetColor();
        }

        private static void PrintHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n====================================================================================================");
            Console.WriteLine($"  {title}");
            Console.WriteLine("====================================================================================================\n");
            Console.ResetColor();
        }

        private static void PrintSubHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n--- {title} ---");
            Console.ResetColor();
        }
    }
}
