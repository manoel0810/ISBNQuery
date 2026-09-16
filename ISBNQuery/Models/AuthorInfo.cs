namespace ISBNQuery.Models
{
    /// <summary>
    /// Fornece informações detalhadas sobre um Autor obtido da Open Library API.
    /// </summary>
    public class AuthorInfo
    {
        /// <summary>
        /// Chave de identificação da Open Library (ex: /authors/OL26320A ou OL26320A)
        /// </summary>
        public string? Key { get; set; }

        /// <summary>
        /// Nome do autor
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Nome pessoal / de batismo do autor, quando disponível
        /// </summary>
        public string? PersonalName { get; set; }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        public string? BirthDate { get; set; }

        /// <summary>
        /// Data de falecimento, quando aplicável
        /// </summary>
        public string? DeathDate { get; set; }

        /// <summary>
        /// Biografia do autor
        /// </summary>
        public string? Bio { get; set; }

        /// <summary>
        /// Nomes alternativos ou pseudônimos
        /// </summary>
        public IReadOnlyList<string>? AlternateNames { get; set; }

        /// <summary>
        /// IDs de fotos disponíveis na Open Library Covers API
        /// </summary>
        public IReadOnlyList<long>? PhotoIds { get; set; }

        /// <summary>
        /// Quantidade total de obras registradas
        /// </summary>
        public int? WorkCount { get; set; }

        /// <summary>
        /// Principal obra associada
        /// </summary>
        public string? TopWork { get; set; }

        /// <summary>
        /// Indica se o autor possui foto disponível na Open Library
        /// </summary>
        public bool HasPhoto => PhotoIds != null && PhotoIds.Count > 0;
    }
}
