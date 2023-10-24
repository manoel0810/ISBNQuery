using System;
using System.Collections.Generic;

namespace ISBNQuery
{
    /// <summary>
    /// <br>pt-br: Fornece informações sobre um objeto <b>Book</b> carregado</br>
    /// <br>en-us: Provides information about a loaded <b>Book</b> object</br>
    /// </summary>

    public class Book
    {
        // Mapeamento entre identificadores e campos
        private static readonly Dictionary<string, Action<Book, string>> Mapeamento = new Dictionary<string, Action<Book, string>>
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
        /// Define o valor do atributo n, com base na paridade de chave valor definida na classe Book
        /// </summary>
        /// <param name="KVP">Chave-valor</param>

        public void SetPropertie(KeyValuePair<string, string> KVP)
        {
            if (Mapeamento.TryGetValue(KVP.Key, out var acao))
            {
                acao(this, KVP.Value);
            }
        }

        /// <summary>
        /// <br>pt-br: Obtém ou define o nome do <b>Autor</b></br>
        /// <br>en-us: Gets or sets the name of the <b>Author</b></br>
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// <br>pt-br: Obtém ou define o <b>Título</b> do exemplar</br>
        /// <br>en-us: Gets or sets the <b>Title</b> of the copy</br>
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// <br>pt-br: Obtém ou define o <b>subtítulo</b> do exemplar</br>
        /// <br>pt-br: Gets or sets the <b>subtitle</b> of the copy</br>
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// <br>pt-br: Obtém ou define o <b>ISBN10</b> do exemplar</br>
        /// <br>en-us: Gets or sets the <b>ISBN10</b> of the copy</br>
        /// </summary>
        public string ISBN10 { get; set; }
        /// <summary>
        /// <br>pt-br: Obtém ou define o <b>ISBN13</b> do exemplar</br>
        /// <br>en-us: Gets or sets the <b>ISBN13</b> of the copy</br>
        /// </summary>
        public string ISBN13 { get; set; }
        /// <summary>
        /// <br>pt-br: Obtém ou define a <b>Data de Publicação</b> do exemplar</br>
        /// <br>en-us: Gets or sets the <b>Publication Date</b> of the copy</br> 
        /// </summary>
        public string Publish_Date { get; set; }
        /// <summary>
        /// <br>pt-br: Obtém ou define o <b>Source Records</b> do exemplar</br>
        /// <br>en-us: Gets or sets the <b>Source Records</b> of the copy</br>
        /// </summary>
        public string Source_Records { get; set; }
        /// <summary>
        /// <br>pt-br: Obtém ou define o <b>Publicador</b> do exemplar</br>
        /// <br>en-us: Gets or sets the <b>Publisher</b> of the copy</br>
        /// </summary>
        public string Publishers { get; set; }
        /// <summary>
        /// <br>pt-br: Obtém ou define o <b>Formato</b> no qual o exemplar está disponibilizado</br>
        /// <br>en-us: Gets or sets the <b>Format</b> in which the copy is available</br>
        /// </summary>
        public string Physical_Format { get; set; }
        /// <summary>
        /// <br>pt-br: Obtém ou define a data da <b>Última Revisão</b> do exemplar</br>
        /// <br>en-us: Gets or sets the copy's <b>Last Revision</b> date</br>
        /// </summary>
        public string Latest_Revision { get; set; }

        //------------------ New fields avaible ------------------------

        /// <summary>
        /// <br>Obtém um breve resumo do exemplar, quando disponível</br>
        /// <br>Get a brief summary of the copy, when available</br>
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// <br>Identifica o título original</br>
        /// <br>Identifies the original title</br>
        /// </summary>
        public string TranslatedFrom { get; set; }
        /// <summary>
        /// <br>Número de páginas</br>
        /// <br>Number of pages</br>
        /// </summary>
        public string NumberOfPages { get; set; }

        //Especial fields

        /// <summary>
        /// <br>Chave de pesquisa para query bib</br>
        /// <br>Search key for query bib</br>
        /// </summary>
        public string BibKey { get; set; }
        /// <summary>
        /// <br>Url para mais detalhes em https://openlibrary.org/</br>
        /// <br>Url for more details at https://openlibrary.org/</br>
        /// </summary>
        public string InfoUrl { get; set; }
        /// <summary>
        /// <br>Guarda a url para a capa do exemplar, quando disponível</br>
        /// <br>Save the url for the cover of the copy, when available</br>
        /// </summary>
        public string ThumbnailUrl { get; set; }
        /// <summary>
        /// <br><b>true</b>, quando há capa disponível</br>
        /// <br><b>true</b>, when cover is available</br>
        /// </summary>
        public bool HasCover => !string.IsNullOrWhiteSpace(ThumbnailUrl);
    }
}
