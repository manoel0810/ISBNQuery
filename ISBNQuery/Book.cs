namespace ISBNQuery
{
    /// <summary>
    /// <br>pt-br: Fornece informações sobre um objeto <b>Book</b> carregado</br>
    /// <br>en-us: Provides information about a loaded <b>Book</b> object</br>
    /// </summary>

    public class Book
    {
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
    }
}
