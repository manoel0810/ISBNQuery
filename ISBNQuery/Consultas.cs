using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace ISBNQuery
{
    /// <summary>
    ///     <br>pt-br: Esta classe possue os métodos necessários para se obter as informações dos ISBN-10 e ISBN-13</br>
    ///     <br>en-us: This class has the methods necessary to obtain information from ISBN-10 and ISBN-13</br>
    /// </summary>

    public class Consultas
    {
        private static readonly List<string> Tags = new List<string>
        {
            "authors",
            "title",
            "isbn_10",
            "isbn_13",
            "publish_date",
            "source_records",
            "publishers",
            "physical_format",
            "latest_revision"
         };


        /// <summary>
        /// <br>pt-br: Tenta recuperar de forma online as informações associadas a um <b>ISBN</b> e antes verifica a conexão com a internet no host <b>google.com</b></br>
        /// <br>en-us: Attempts to retrieve the information associated with an <b>ISBN-13</b> online and first check the internet connection at the host <b>google.com</b></br>
        /// </summary>
        /// <param name="ISBN"></param>
        /// <param name="InternetCheck"></param>
        /// <returns></returns>

        public static Book ConsultarISBN(string ISBN, bool InternetCheck)
        {
            string COD = ISBN.Replace(".", "").Replace("-", "").Replace(" ", "");
            if (COD.Length.Equals(10))
                return ConsultarISBN10(ISBN, InternetCheck);
            else if (COD.Length.Equals(13))
                return ConsultarISBN13(ISBN, InternetCheck);
            else
                MessageBox.Show("The ISBN code isn't ISBN10 or ISBN13", "ISBN Check", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return null;
        }

        /// <summary>
        /// <br>pt-br: Tenta recuperar de forma online as informações associadas a um <b>ISBN-13</b> e antes verifica a conexão com a internet no host <b>google.com</b></br>
        /// <br>en-us: Attempts to retrieve the information associated with an <b>ISBN-13</b> online and first check the internet connection at the host <b>google.com</b></br>
        /// </summary>
        /// <param name="ISBN13">Recebe o código <b>ISBN-13</b> que será verificado</param>
        /// <param name="InternetCheck">Se <b>true</b>, faz a verificação da internet</param>
        /// <returns>Retorna um objeto do tipo <b>Book</b></returns>

        public static Book ConsultarISBN13(string ISBN13, bool InternetCheck)
        {
            ISBN13 = ISBN13.Replace("-", "").Replace(".", "");

            if (InternetCheck)
                if (!Validacoes.CheckInternet())
                {
                    MessageBox.Show("No internet access detected", "InternetCheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                else
                    return ConsultarISBN13(ISBN13);
            else
                return ConsultarISBN13(ISBN13);
        }

        /// <summary>
        /// <br>pt-br: Tenta recuperar de forma online as informações associadas a um <b>ISBN-13</b> e antes verifica a conexão com a internet no <b>Url</b> passado</br>
        /// <br>en-us: Attempts to retrieve the information associated with an <b>ISBN-13</b> online and first checks the internet connection in the past <b>Url</b></br>
        /// </summary>
        /// <param name="ISBN13">Recebe o código <b>ISBN-13</b> que será verificado</param>
        /// <param name="Url">Recebe o <b>Url</b> que servirá de base para o teste de conexão</param>
        /// <param name="Timeout">Tempo de aguardo para a conexão em ms</param>
        /// <returns>Retorna um objeto do tipo <b>Book</b></returns>

        public static Book ConsultarISBN13(string ISBN13, string Url, int Timeout = 3000)
        {
            ISBN13 = ISBN13.Replace("-", "").Replace(".", "");

            if (!Validacoes.CheckInternet(Url, Timeout))
            {
                MessageBox.Show("No internet access detected", "InternetCheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
                return ConsultarISBN13(ISBN13);
        }

        /// <summary>
        /// <br>pt-br: Tenta recuperar de forma online as informações associadas a um <b>ISBN-13</b></br>
        /// <br>en-us: Attempts to retrieve the information associated with an <b>ISBN-13</b> online</br>
        /// </summary>
        /// <param name="ISBN13">Recebe o código <b>ISBN-13</b> que será verificado</param>
        /// <returns>Retorna um objeto do tipo <b>Book</b></returns>

        public static Book ConsultarISBN13(string ISBN13)
        {
            ISBN13 = ISBN13.Replace("-", "").Replace(".", "");

            if (Validacoes.CheckISBN13(ISBN13) == Validacoes.ReturnType.ISBN13_VALIDO)
            {
                try
                {
                    using (WebClient Client = new WebClient())
                    {
                        if (Validacoes.WindowsVersion().Contains("Windows 7"))
                        {
                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        }

                        string Content = Client.DownloadString($"https://openlibrary.org/isbn/{ISBN13}.json").Replace("\"", "'").Replace("',", "'|").Replace("],", "]|").Replace("},", "}|");
                        Book book = TryCreateObject(Tags, Content);
                        return book;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("404"))
                    {
                        MessageBox.Show("O servidor remoto não encontrou o ISBN passado ou está offline", "Consultas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return null;
                    }

                    MessageBox.Show($"Ocorreu um erro no processo de criação do objeto 'Book'\nERRO:{ex.Message}", "Error - Deserialização json", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Não foi possível construir o objeto 'Book'", "Erro - Object", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// <br>pt-br: Tenta recuperar de forma online as informações associadas a um <b>ISBN-10</b> e antes verifica a conexão com a internet no host <b>google.com</b></br>
        /// <br>en-us: Attempts to retrieve the information associated with an <b>ISBN-10</b> online and first checks the internet connection at the host <b>google.com</b></br>
        /// </summary>
        /// <param name="ISBN10">Recebe o código <b>ISBN-10</b> que será verificado</param>
        /// <param name="InternetCheck">Se <b>true</b>, faz a verificação da internet</param>
        /// <returns>Retorna um objeto do tipo <b>Book</b></returns>

        public static Book ConsultarISBN10(string ISBN10, bool InternetCheck)
        {
            ISBN10 = ISBN10.Replace("-", "").Replace(".", "");

            if (InternetCheck)
                if (!Validacoes.CheckInternet())
                {
                    MessageBox.Show("No internet access detected", "InternetCheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                else
                    return ConsultarISBN10(ISBN10);
            else
                return ConsultarISBN10(ISBN10);
        }

        /// <summary>
        /// <br>pt-br: Tenta recuperar de forma online as informações associadas a um <b>ISBN-10</b> e antes verifica a conexão com a internet no <b>Url</b> passado</br>
        /// <br>en-us: Attempts to retrieve the information associated with an <b>ISBN-10</b> online and first checks the internet connection in the past <b>Url</b></br>
        /// </summary>
        /// <param name="ISBN10">Recebe o código <b>ISBN-10</b> que será verificado</param>
        /// <param name="Url">Recebe o <b>Url</b> que servirá de base para o teste de conexão</param>
        /// <param name="Timeout">Tempo de aguardo para a conexão em ms</param>
        /// <returns>Retorna um objeto do tipo <b>Book</b></returns>

        public static Book ConsultarISBN10(string ISBN10, string Url, int Timeout = 3000)
        {
            ISBN10 = ISBN10.Replace("-", "").Replace(".", "");

            if (!Validacoes.CheckInternet(Url, Timeout))
            {
                MessageBox.Show("No internet access detected", "InternetCheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
                return ConsultarISBN10(ISBN10);
        }

        /// <summary>
        /// <br>pt-br: Tenta recuperar de forma online as informações associadas a um <b>ISBN-10</b></br>
        /// <br>en-us: Attempts to retrieve the information associated with an <b>ISBN-10</b> online</br>
        /// </summary>
        /// <param name="ISBN10">Recebe o código <b>ISBN-10</b> que será verificado</param>
        /// <returns>Retorna um objeto do tipo <b>Book</b></returns>

        public static Book ConsultarISBN10(string ISBN10)
        {
            ISBN10 = ISBN10.Replace("-", "").Replace(".", "");

            if (Validacoes.CheckISBN10(ISBN10) == Validacoes.ReturnType.ISBN10_VALIDO)
            {
                try
                {
                    using (WebClient Client = new WebClient())
                    {
                        if (Validacoes.WindowsVersion().Contains("Windows 7"))
                        {
                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        }

                        string Content = Client.DownloadString($"https://openlibrary.org/isbn/{ISBN10}.json").Replace("\"", "'").Replace("',", "'|").Replace("],", "]|").Replace("},", "}|");
                        Book book = TryCreateObject(Tags, Content);
                        return book;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("404"))
                    {
                        MessageBox.Show("O servidor remoto não encontrou o ISBN passado ou está offline", "Consultas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return null;
                    }

                    MessageBox.Show($"Ocorreu um erro no processo de criação do objeto 'Book'\nERRO:{ex.Message}", "Error - Deserialização json", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Não foi possível construir o objeto 'Book'", "Erro - Object", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        /// <summary>
        ///         <br><b>** Método de controle interno **</b></br>
        ///         <br>Este método tenta preencher o objeto do tipo <b>Book</b> que terá as informações do livro</br>
        /// </summary>
        /// <param name="Tags">Recebe uma coleção com as <b><i>Tags</i></b> que serão verificadas para preencher o objeto <b>Book</b></param>
        /// <param name="JSON">Recebe a string com o conteúdo completo do <c><b><i>*.json</i></b></c> que contém as informações do ISBN-XX</param>
        /// <returns>Retorna um objeto do tipo <b>Book</b></returns>

        private static Book TryCreateObject(List<string> Tags, string JSON)
        {
            Book book = new Book();
            string[] Argumentos = JSON.Split('|');
            foreach (string Tag in Tags)
            {
                foreach (string Argument in Argumentos)
                {
                    if (Argument.Contains($"'{Tag}':"))
                    {
                        string Formated = Argument.Trim();
                        if (Tag == "title")
                            book.Title = Validacoes.FullFormat(@GetAttribute(Formated));
                        else if (Tag == "isbn_10")
                            book.ISBN10 = GetAttribute(Formated);
                        else if (Tag == "isbn_13")
                            book.ISBN13 = GetAttribute(Formated);
                        else if (Tag == "publish_date")
                            book.Publish_Date = GetAttribute(Formated);
                        else if (Tag == "source_records")
                            book.Source_Records = GetAttribute(Formated);
                        else if (Tag == "publishers")
                            book.Publishers = Validacoes.FullFormat(GetAttribute(Formated));
                        else if (Tag == "physical_format")
                            book.Physical_Format = GetAttribute(Formated);
                        else if (Tag == "authors")
                            GetAuthor(ref book, Argument.Substring(Argument.IndexOf("/")).Replace("]", "").Replace("}", "").Replace("'", ""));
                    }
                }
            }

            return book;
        }

        /// <summary>
        ///         <br><b>** Método de controle interno **</b></br>
        ///         <br>Este método tenta recuperar o <b>Autor</b> do livro no servidor online</br>
        /// </summary>
        /// <param name="book">Recebe a referência da variável em memória com o objeto criado do tipo <b>Book</b></param>
        /// <param name="reference">Recebe o <b>src</b> com o caminho para acessar os arquivos do Autor no servidor</param>

        private static void GetAuthor(ref Book book, string reference)
        {
            try
            {
                using (WebClient Client = new WebClient())
                {
                    string source = $"https://openlibrary.org{reference}.json";
                    string Content = Client.DownloadString(source).Replace("\"", "'").Replace("',", "'|").Replace("],", "]|").Replace("},", "}|");
                    string[] Argumentos = Content.Split('|');
                    foreach (string Argument in Argumentos)
                        if (Argument.Contains($"'name':"))
                            book.Author = Validacoes.FullFormat(GetAttribute(Argument));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro no processo de criação do objeto 'Book'\nERRO:{ex.Message}", "Error - Deserialização json", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Format an isbn code (10 to 13 or 13 to 10)
        /// </summary>
        /// <param name="ISBN">ISBN10 or ISBN13</param>
        /// <returns>Converted ISBN</returns>

        public static string FormatISBN(string ISBN)
        {
            string Formated = ISBN.Replace(".", "").Replace("-", "").Replace(" ", "");
            if (Formated.Length == 10)
            {
                int Soma = 0, fator = 1, digito = 0;
                int[] Valores = new int[12];

                Valores[0] = 9;
                Valores[1] = 7;
                Valores[2] = 8;

                for (int i = 3; i < 12; i++)
                {
                    Valores[i] = int.Parse(Convert.ToString(Formated[i - 3]));
                }

                for (int i = 0; i < 12; i++)
                {
                    Soma += Valores[i] * fator;
                    if (fator == 1)
                        fator = 3;
                    else
                        fator = 1;
                }

                int mod = Soma % 10;
                if (mod != 0) { digito = 10 - mod; }

                return String.Format("978{0}{1}", Formated.Substring(0, 9), digito);
            }
            else if (Formated.Length == 13)
            {
                int[] Valores = new int[9];
                for (int i = 3; i < 12; i++)
                {
                    Valores[i - 3] = int.Parse(Formated[i].ToString());
                }

                int Soma = 0, fator = 10;
                for (int i = 0; i < 9; i++)
                {
                    Soma += Valores[i] * (fator - i);
                }

                int mod = Soma % 11;
                int digito = 0;
                if (mod != 0) { digito = 11 - mod; }

                string part = "";
                foreach (int i in Valores)
                    part += i.ToString();

                return String.Format("{0}{1}", part, digito != 10 ? digito.ToString() : "X");
            }
            else
            {
                MessageBox.Show("ISBN code wrong", "ISBN Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Obtém a capa do livro solicitado
        /// </summary>
        /// <param name="Size">Tamanho da imagem disponível pela API</param>
        /// <param name="KEY">Chave de busca $ISBN</param>
        /// <returns>Retorna a imagem no formato ByteArray[]</returns>

        public static byte[] GetImage(ImageSize Size, string KEY)
        {
            string MASK = $"https://covers.openlibrary.org/b/isbn/{KEY}-{(char)Size}.jpg";
            byte[] IMAGE = null;

            try
            {
                using (WebClient Cliente = new WebClient())
                {
                    IMAGE = Cliente.DownloadData(MASK);
                }

                //API RETURN: Image Not Found (1 Pixel image returned)
                if (IMAGE.Length == 807)
                    return new byte[] { };
                else
                    return IMAGE; //Image found
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Converte um array de bytes na sua imagem correspondente
        /// </summary>
        /// <param name="bytes">Bytes da imagem</param>
        /// <returns>Imagem convertida</returns>

        public static Image GetImageFromByteArray(byte[] bytes)
        {
            if (bytes.Length == 0)
                return null;

            try
            {
                using (System.IO.MemoryStream Stream = new System.IO.MemoryStream(bytes))
                {
                    var IMG = Image.FromStream(Stream);
                    return IMG;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtém diretamente a imagem, executando as rotidas já existentes individuais
        /// </summary>
        /// <param name="Size">Tamanho da imagem disponibilizada pela API</param>
        /// <param name="KEY">Chave de consulta $ISBN</param>
        /// <returns>Imagem do $KEY passado no tamanho $SIZE</returns>

        public static Image GetCompostImage(ImageSize Size, string KEY)
        {
            var bytes = GetImage(Size, KEY);
            if(bytes.Length == 0)         
                return null;          

            return GetImageFromByteArray(bytes);
        }

        /// <summary>
        ///         <br><b>** Método de controle interno **</b></br>
        ///         <br>Este método recupera a informação associada a uma <b>Tag</b> no arquivo JSON</br>
        /// </summary>
        /// <param name="Sequo">Recebe a string com a tag e seu argumento</param>
        /// <returns>Devolve uma string com o argumento associado à tag</returns>

        private static string GetAttribute(string Sequo)
        {
            string Formated = Sequo.Trim().Replace("[", "").Replace("]", "");
            Formated = Formated.Substring(Formated.IndexOf(':') + 0x2).Replace("'", "").Trim();
            return Formated;
        }

        /// <summary>
        /// Tamanhos disponíveis da imagem
        /// </summary>

        [Flags]

        public enum ImageSize : int
        {
            /// <summary>
            /// Small size
            /// </summary>
            S = 83,
            /// <summary>
            /// Medium size
            /// </summary>
            M = 77,
            /// <summary>
            /// Large size
            /// </summary>
            L = 76
        }
    }
}
