namespace ISBNQuery.Models
{
    /// <summary>
    /// Tipo de chave utilizada para buscar capas ou fotos na Open Library Covers API.
    /// </summary>
    public enum CoverKeyType
    {
        /// <summary>
        /// ISBN
        /// </summary>
        Isbn,
        /// <summary>
        /// Open Library ID
        /// </summary>
        Olid,
        /// <summary>
        /// ID
        /// </summary>
        Id,
        /// <summary>
        /// OCLC
        /// </summary>
        Oclc,
        /// <summary>
        /// LCCN
        /// </summary>
        Lccn
    }

    /// <summary>
    /// Contém informações de requisição/resposta para capas de livros e fotos de autores.
    /// </summary>
    public class CoverInfo
    {
        /// <summary>
        /// Tipo de chave (Isbn, Olid, Id, etc.)
        /// </summary>
        public CoverKeyType KeyType { get; set; }

        /// <summary>
        /// Valor da chave de busca (ex: 9788551005194, OL26320A, 240727)
        /// </summary>
        public string KeyValue { get; set; } = string.Empty;

        /// <summary>
        /// Tamanho solicitado da imagem (S, M, L)
        /// </summary>
        public ImageSize Size { get; set; }

        /// <summary>
        /// URL direta para download da imagem na Open Library Covers API
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Indica se a requisição se refere a foto de autor (true) ou capa de livro (false)
        /// </summary>
        public bool IsAuthorPhoto { get; set; }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public CoverInfo() { }

        /// <summary>
        /// Construtor com inicialização da URL da capa do livro
        /// </summary>
        public CoverInfo(CoverKeyType keyType, string keyValue, ImageSize size, bool isAuthorPhoto = false)
        {
            KeyType = keyType;
            KeyValue = keyValue;
            Size = size;
            IsAuthorPhoto = isAuthorPhoto;

            string keyPrefix = keyType.ToString().ToLowerInvariant();
            string category = isAuthorPhoto ? "a" : "b";
            char sizeChar = (char)size;
            Url = $"https://covers.openlibrary.org/{category}/{keyPrefix}/{keyValue}-{sizeChar}.jpg";
        }
    }
}
