using ISBNQuery.Models;

namespace ISBNQuery
{
    /// <summary>
    /// <br>pt-br: Fornece informações sobre um objeto <b>Book</b> (Livro/Exemplar) retornado pela Open Library API</br>
    /// <br>en-us: Provides information about a loaded <b>Book</b> object returned by Open Library API</br>
    /// </summary>
    public class Book
    {
        private static readonly Dictionary<string, Action<Book, string>> Mapeamento = new()
        {
            { "authors", (obj, valor) => obj.Author = valor },
            { "title", (obj, valor) => obj.Title = valor },
            { "subtitle", (obj, valor) => obj.SubTitle = valor },
            { "isbn_10", (obj, valor) => obj.ISBN10 = valor },
            { "isbn_13", (obj, valor) => obj.ISBN13 = valor },
            { "publish_date", (obj, valor) => obj.Publish_Date = valor },
            { "source_records", (obj, valor) => obj.Source_Records = valor },
            { "publishers", (obj, valor) => obj.Publishers = valor },
            { "physical_format", (obj, valor) => obj.Physical_Format = valor },
            { "latest_revision", (obj, valor) => obj.Latest_Revision = valor },
            { "description", (obj, valor) => obj.Description = valor },
            { "translation_of", (obj, valor) => obj.TranslatedFrom = valor },
            { "number_of_pages", (obj, valor) => obj.NumberOfPages = valor },
            { "bib_key", (obj, valor) => obj.BibKey = valor },
            { "info_url", (obj, valor) => obj.InfoUrl = valor },
            { "thumbnail_url", (obj, valor) => obj.ThumbnailUrl = valor }
        };

        internal static Dictionary<string, Action<Book, string>> GetMap() => Mapeamento;

        /// <summary>
        /// Define o valor de uma propriedade com base na paridade chave-valor
        /// </summary>
        public void SetPropertie(KeyValuePair<string, string> KVP)
        {
            if (Mapeamento.TryGetValue(KVP.Key, out var acao))
            {
                acao(this, KVP.Value);
            }
        }

        /// <summary>
        /// Nome do Autor principal (ou autores concatenados por vírgula)
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// Título do exemplar
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Subtítulo do exemplar
        /// </summary>
        public string? SubTitle { get; set; }

        /// <summary>
        /// Código ISBN-10
        /// </summary>
        public string? ISBN10 { get; set; }

        /// <summary>
        /// Código ISBN-13
        /// </summary>
        public string? ISBN13 { get; set; }

        /// <summary>
        /// Data de Publicação
        /// </summary>
        public string? Publish_Date { get; set; }

        /// <summary>
        /// Registros de origem (Source Records)
        /// </summary>
        public string? Source_Records { get; set; }

        /// <summary>
        /// Editora(s) / Publicador(es)
        /// </summary>
        public string? Publishers { get; set; }

        /// <summary>
        /// Formato Físico (Brochura, Capa Dura, eBook, etc.)
        /// </summary>
        public string? Physical_Format { get; set; }

        /// <summary>
        /// Data ou versão da última revisão
        /// </summary>
        public string? Latest_Revision { get; set; }

        /// <summary>
        /// Descrição ou resumo do livro
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Título original de onde foi traduzido
        /// </summary>
        public string? TranslatedFrom { get; set; }

        /// <summary>
        /// Número de páginas
        /// </summary>
        public string? NumberOfPages { get; set; }

        /// <summary>
        /// Chave Bib (ex: ISBN:9788551005194)
        /// </summary>
        public string? BibKey { get; set; }

        /// <summary>
        /// URL de detalhes em openlibrary.org
        /// </summary>
        public string? InfoUrl { get; set; }

        /// <summary>
        /// URL para a imagem de capa (Thumbnail)
        /// </summary>
        public string? ThumbnailUrl { get; set; }

        /// <summary>
        /// Identificador da Edição na Open Library (OLID / Edition Key, ex: OL12345M)
        /// </summary>
        public string? EditionKey { get; set; }

        /// <summary>
        /// Identificador da Obra na Open Library (Work Key, ex: OL56789W)
        /// </summary>
        public string? WorkKey { get; set; }

        /// <summary>
        /// Assuntos / Tópicos associados ao livro
        /// </summary>
        public IReadOnlyList<string>? Subjects { get; set; }

        /// <summary>
        /// Lista detalhada de autores associados ao livro
        /// </summary>
        public IReadOnlyList<AuthorInfo>? AuthorsList { get; set; }

        /// <summary>
        /// IDs de capas disponíveis na Open Library Covers API
        /// </summary>
        public IReadOnlyList<long>? CoverIds { get; set; }

        /// <summary>
        /// Indica se há capa disponível para este livro
        /// </summary>
        public bool HasCover => !string.IsNullOrWhiteSpace(ThumbnailUrl) || (CoverIds != null && CoverIds.Count > 0) || !string.IsNullOrWhiteSpace(ISBN13) || !string.IsNullOrWhiteSpace(ISBN10);
    }
}
