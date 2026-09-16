using System.Text.Json;
using System.Text.Json.Serialization;

namespace ISBNQuery.Models
{
    /// <summary>
    /// Converter para manipular campos da OpenLibrary que podem vir como string ou como objeto {"type": "...", "value": "..."}.
    /// </summary>
    public class StringOrObjectJsonConverter : JsonConverter<string>
    {
        /// <summary>
        /// Lê o valor do JSON e retorna uma string, independentemente de o valor ser uma string simples ou um objeto com a propriedade "value".
        /// </summary>
        /// <param name="reader">O leitor JSON.</param>
        /// <param name="typeToConvert">O tipo a ser convertido.</param>
        /// <param name="options">As opções do serializador JSON.</param>
        /// <returns>A string resultante da conversão.</returns>
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return reader.GetString();
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                using var doc = JsonDocument.ParseValue(ref reader);
                if (doc.RootElement.TryGetProperty("value", out var val))
                {
                    return val.GetString();
                }
            }

            return null;
        }

        /// <summary>
        /// Escreve o valor da string no JSON. Se o valor for nulo, escreve null; caso contrário, escreve a string.
        /// </summary>
        /// <param name="writer">O escritor JSON.</param>
        /// <param name="value">O valor a ser escrito.</param>
        /// <param name="options">As opções do serializador JSON.</param>
        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }

    /// <summary>
    /// DTO para edição de livro (Open Library Edition Endpoint /isbn/{isbn}.json ou /books/{id}.json)
    /// </summary>

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class EditionJsonResponse
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("subtitle")]
        public string? Subtitle { get; set; }

        [JsonPropertyName("authors")]
        public AuthorKeyRef[]? Authors { get; set; }

        [JsonPropertyName("publishers")]
        public string[]? Publishers { get; set; }

        [JsonPropertyName("publish_date")]
        public string? PublishDate { get; set; }

        [JsonPropertyName("physical_format")]
        public string? PhysicalFormat { get; set; }

        [JsonPropertyName("number_of_pages")]
        public int? NumberOfPages { get; set; }

        [JsonPropertyName("description")]
        [JsonConverter(typeof(StringOrObjectJsonConverter))]
        public string? Description { get; set; }

        [JsonPropertyName("isbn_10")]
        public string[]? Isbn10 { get; set; }

        [JsonPropertyName("isbn_13")]
        public string[]? Isbn13 { get; set; }

        [JsonPropertyName("covers")]
        public long[]? Covers { get; set; }

        [JsonPropertyName("works")]
        public WorkKeyRef[]? Works { get; set; }

        [JsonPropertyName("subjects")]
        public string[]? Subjects { get; set; }

        [JsonPropertyName("source_records")]
        public string[]? SourceRecords { get; set; }
    }

    /// <summary>
    /// Referência de autor em uma edição
    /// </summary>
    public class AuthorKeyRef
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }
    }

    /// <summary>
    /// Referência de obra em uma edição
    /// </summary>
    public class WorkKeyRef
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }
    }

    /// <summary>
    /// DTO para obra (Open Library Work Endpoint /works/{id}.json)
    /// </summary>
    public class WorkJsonResponse
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        [JsonConverter(typeof(StringOrObjectJsonConverter))]
        public string? Description { get; set; }

        [JsonPropertyName("subjects")]
        public string[]? Subjects { get; set; }

        [JsonPropertyName("covers")]
        public long[]? Covers { get; set; }
    }

    /// <summary>
    /// DTO para autor (Open Library Author Endpoint /authors/{id}.json)
    /// </summary>
    public class AuthorJsonResponse
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("personal_name")]
        public string? PersonalName { get; set; }

        [JsonPropertyName("birth_date")]
        public string? BirthDate { get; set; }

        [JsonPropertyName("death_date")]
        public string? DeathDate { get; set; }

        [JsonPropertyName("bio")]
        [JsonConverter(typeof(StringOrObjectJsonConverter))]
        public string? Bio { get; set; }

        [JsonPropertyName("photos")]
        public long[]? Photos { get; set; }

        [JsonPropertyName("alternate_names")]
        public string[]? AlternateNames { get; set; }

        [JsonPropertyName("links")]
        public AuthorLink[]? Links { get; set; }
    }

    /// <summary>
    /// Link associado ao autor
    /// </summary>
    public class AuthorLink
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    /// <summary>
    /// Resposta de pesquisa de livros (/search.json)
    /// </summary>
    public class BookSearchJsonResponse
    {
        [JsonPropertyName("numFound")]
        public int NumFound { get; set; }

        [JsonPropertyName("docs")]
        public BookSearchDoc[]? Docs { get; set; }
    }

    /// <summary>
    /// Documento de resultado de pesquisa de livros
    /// </summary>
    public class BookSearchDoc
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("author_name")]
        public string[]? AuthorName { get; set; }

        [JsonPropertyName("author_key")]
        public string[]? AuthorKey { get; set; }

        [JsonPropertyName("isbn")]
        public string[]? Isbn { get; set; }

        [JsonPropertyName("cover_i")]
        public long? CoverI { get; set; }

        [JsonPropertyName("first_publish_year")]
        public int? FirstPublishYear { get; set; }
    }

    /// <summary>
    /// Resposta de pesquisa de autores (/search/authors.json)
    /// </summary>
    public class AuthorSearchJsonResponse
    {
        [JsonPropertyName("numFound")]
        public int NumFound { get; set; }

        [JsonPropertyName("docs")]
        public AuthorSearchDoc[]? Docs { get; set; }
    }

    /// <summary>
    /// Documento de resultado de pesquisa de autor
    /// </summary>
    public class AuthorSearchDoc
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("birth_date")]
        public string? BirthDate { get; set; }

        [JsonPropertyName("top_work")]
        public string? TopWork { get; set; }

        [JsonPropertyName("work_count")]
        public int? WorkCount { get; set; }
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
