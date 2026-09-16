using ISBNQuery.Models;
using System.Text.Json;

namespace ISBNQuery
{
    internal class Parser
    {
        public static Book ParseFromEdition(EditionJsonResponse edition, string searchIsbn)
        {
            var book = new Book
            {
                Title = edition.Title,
                SubTitle = edition.Subtitle,
                Publish_Date = edition.PublishDate,
                Physical_Format = edition.PhysicalFormat,
                NumberOfPages = edition.NumberOfPages?.ToString(),
                Description = edition.Description,
                EditionKey = edition.Key,
                WorkKey = edition.Works != null && edition.Works.Length > 0 ? edition.Works[0].Key : null,
                Publishers = edition.Publishers != null && edition.Publishers.Length > 0 ? string.Join(", ", edition.Publishers) : null,
                Subjects = edition.Subjects != null ? edition.Subjects.ToList() : null,
                CoverIds = edition.Covers != null ? edition.Covers.ToList() : null,
                Source_Records = edition.SourceRecords != null && edition.SourceRecords.Length > 0 ? string.Join(", ", edition.SourceRecords) : null
            };

            // ISBN-10 e ISBN-13
            if (edition.Isbn10 != null && edition.Isbn10.Length > 0)
                book.ISBN10 = edition.Isbn10[0];
            else if (searchIsbn.Length == 10)
                book.ISBN10 = searchIsbn;

            if (edition.Isbn13 != null && edition.Isbn13.Length > 0)
                book.ISBN13 = edition.Isbn13[0];
            else if (searchIsbn.Length == 13)
                book.ISBN13 = searchIsbn;

            // Thumbnail URL
            string keyForCover = !string.IsNullOrWhiteSpace(book.ISBN13) ? book.ISBN13! : (!string.IsNullOrWhiteSpace(book.ISBN10) ? book.ISBN10! : searchIsbn);
            book.ThumbnailUrl = $"https://covers.openlibrary.org/b/isbn/{keyForCover}-M.jpg";
            book.InfoUrl = edition.Key != null ? $"https://openlibrary.org{edition.Key}" : $"https://openlibrary.org/isbn/{searchIsbn}";
            book.BibKey = $"ISBN:{searchIsbn}";

            return book;
        }

        public static AuthorInfo ParseFromAuthor(AuthorJsonResponse dto)
        {
            return new AuthorInfo
            {
                Key = dto.Key,
                Name = dto.Name,
                PersonalName = dto.PersonalName,
                BirthDate = dto.BirthDate,
                DeathDate = dto.DeathDate,
                Bio = dto.Bio,
                PhotoIds = dto.Photos != null ? dto.Photos.ToList() : null,
                AlternateNames = dto.AlternateNames != null ? dto.AlternateNames.ToList() : null
            };
        }

        public static Book Parse(Rootobject @object)
        {
            Book book = new();
            string[] rootValues = { "bib_key", "info_url", "thumbnail_url" };
            foreach (string s in rootValues)
                book.SetPropertie(new KeyValuePair<string, string>(s, @object.Generic[s]!));

            string[] derivedValues = { "title", "physical_format", "key", "publish_date", "physical_dimensions", "number_of_pages", "description", "translation_of", "latest_revision", "revision" };
            foreach (string s in derivedValues)
                book.SetPropertie(new KeyValuePair<string, string>(s, @object.Generic?.Details?[s]!));

            // Especial fields
            book.SetPropertie(new KeyValuePair<string, string>("isbn_10", @object.Generic?.Details?.ISBN10 != null && @object.Generic.Details.ISBN10.Length > 0 ? @object.Generic.Details.ISBN10[0] : ""));
            book.SetPropertie(new KeyValuePair<string, string>("isbn_13", @object.Generic?.Details?.ISBN13 != null && @object.Generic.Details.ISBN13.Length > 0 ? @object.Generic.Details.ISBN13[0] : ""));
            book.SetPropertie(new KeyValuePair<string, string>("authors", @object.Generic?.Details?.Authors != null && @object.Generic.Details.Authors?.Length > 0 ? @object.Generic.Details.Authors[0].Name! : ""));
            book.SetPropertie(new KeyValuePair<string, string>("publishers", @object.Generic?.Details?.Publishers != null && @object.Generic.Details.Publishers.Length > 0 ? @object.Generic.Details.Publishers[0] : ""));

            return book;
        }

        static void ReplaceSubstring(ref string original, int startIndex, int length, string substitute)
        {
            if (startIndex < 0 || startIndex >= original.Length || length < 0 || startIndex + length > original.Length)
            {
                throw new ArgumentException("Índices inválidos");
            }

            original = original.Remove(startIndex, length).Insert(startIndex, substitute);
        }

        public static Book TryCreateObject(string JSON, int Deslocation)
        {
            ReplaceSubstring(ref JSON, 2, Deslocation, "Generic");
            var obj = JsonSerializer.Deserialize<Rootobject>(JSON);

            return Parse(obj!);
        }
    }
}
