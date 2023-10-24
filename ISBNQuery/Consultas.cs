using ISBNQuery.Erros;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Net;
using System.Reflection;
using FormatException = ISBNQuery.Erros.FormatException;

namespace ISBNQuery
{
    /// <summary>
    ///     <br>pt-br: Esta classe possue os métodos necessários para se obter as informações dos ISBN-10 e ISBN-13</br>
    ///     <br>en-us: This class has the methods necessary to obtain information from ISBN-10 and ISBN-13</br>
    /// </summary>

    public class Consultas
    {
        /// <summary>
        /// Obtém a versão atual da dll
        /// </summary>
        public static string GetCallingAssemblyVersion()
        {
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            AssemblyFileVersionAttribute fileVersionAttr = callingAssembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
            if (fileVersionAttr != null)
            {
                return fileVersionAttr.Version;
            }

            // Se o atributo [AssemblyFileVersion] não estiver definido, retorne a versão do assembly.
            return callingAssembly.GetName().Version.ToString();
        }

        private static void FormatISBN(ref string isbn)
        {
            isbn = isbn.Replace("-", "").Replace(".", "").Replace(" ", "").Trim();
        }

        private static Book ExecuteProcedures(string Key)
        {
            var check = Key.Length == 10 ? ReturnType.ValidISBN10 : ReturnType.ValidISBN13;
            if ((Key.Length == 13 ? Validacoes.CheckISBN13(Key) : Validacoes.CheckISBN10(Key)) == check)
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

                        string searchKey = $"https://openlibrary.org/api/books?bibkeys=ISBN:{Key}&jscmd=details&format=json";
                        string Content = Client.DownloadString(searchKey);
                        if (string.IsNullOrEmpty(Content) || Content.Length <= 4)
                        {
                            throw new ApiRequestJsonError($"API Request not found for: {searchKey}", new Exception());
                        }

                        Book book = TryCreateObject(Content, Key.Length == 10 ? 15 : 18);
                        return book;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("404"))
                        throw new InternetException404("O servidor remoto não encontrou o ISBN passado ou está offline", ex);
                    else
                        throw new InternetException("Ocorreu um erro no processo de criação do objeto 'Book'", ex);
                }
            }
            else
            {
                throw new FormatException("ISBN format error", new Exception(), ReturnType.InvalidISBN13);
            }
        }

        /// <summary>
        /// <br>pt-br: Tenta recuperar de forma online as informações associadas a um <b>ISBN</b> e antes verifica a conexão com a internet no host <b>google.com</b></br>
        /// <br>en-us: Attempts to retrieve the information associated with an <b>ISBN-13</b> online and first check the internet connection at the host <b>google.com</b></br>
        /// </summary>
        /// <param name="ISBN"></param>
        /// <param name="InternetCheck"></param>
        /// <returns></returns>

        public static Book ConsultarISBN(string ISBN, bool InternetCheck)
        {
            string Code = ISBN;
            FormatISBN(ref Code);

            if (Code.Length.Equals(10))
                return ConsultarISBN10(ISBN, InternetCheck);
            else if (Code.Length.Equals(13))
                return ConsultarISBN13(ISBN, InternetCheck);
            else
                throw new FormatExceptionArgument("The ISBN code isn't ISBN10 or ISBN13", new Exception(), ReturnType.InvalidInputFormat);
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
            FormatISBN(ref ISBN13);
            if (InternetCheck)
                if (!Validacoes.CheckInternet())
                {
                    throw new InternetException("No internet access detected", new Exception());
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
            FormatISBN(ref ISBN13);
            if (!Validacoes.CheckInternet(Url, Timeout))
            {
                throw new InternetException("No internet access detected", new Exception());
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
            FormatISBN(ref ISBN13);
            return ExecuteProcedures(ISBN13);
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
            FormatISBN(ref ISBN10);
            if (InternetCheck)
                if (!Validacoes.CheckInternet())
                {
                    throw new InternetException("No internet access detected", new Exception());
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
            FormatISBN(ref ISBN10);
            if (!Validacoes.CheckInternet(Url, Timeout))
            {
                throw new InternetException("No internet access detected", new Exception());
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
            FormatISBN(ref ISBN10);
            return ExecuteProcedures(ISBN10);
        }

        static void ReplaceSubstring(ref string original, int startIndex, int length, string substitute)
        {
            if (startIndex < 0 || startIndex >= original.Length || length < 0 || startIndex + length > original.Length)
            {
                throw new ArgumentException("Índices inválidos");
            }

            original = original.Remove(startIndex, length).Insert(startIndex, substitute);
        }

        /// <summary>
        ///         <br><b>** Método de controle interno **</b></br>
        ///         <br>Este método tenta preencher o objeto do tipo <b>Book</b> que terá as informações do livro</br>
        /// </summary>
        /// <param name="JSON">Recebe a string com o conteúdo completo do <c><b><i>*.json</i></b></c> que contém as informações do ISBN-XX</param>
        /// <param name="Deslocation">Deslocamento do json para substituir raiz</param>
        /// <returns>Retorna um objeto do tipo <b>Book</b></returns>

        private static Book TryCreateObject(string JSON, int Deslocation)
        {
            ReplaceSubstring(ref JSON, 2, Deslocation, "Generic");
            var obj = JsonConvert.DeserializeObject<Rootobject>(JSON);

            Parser parser = Parser.Instance();
            parser.Parse(obj);
            return parser.Parse(obj);
        }

        /// <summary>
        /// Format an isbn code (ISBN10 to ISBN13 or ISBN13 to ISBN10)
        /// </summary>
        /// <param name="ISBN">ISBN10 or ISBN13</param>
        /// <returns>Converted ISBN</returns>

        public static string FormatISBN(string ISBN)
        {
            FormatISBN(ref ISBN);
            if (ISBN.Length == 10)
            {
                int Soma = 0, fator = 1, digito = 0;
                int[] Valores = new int[12];

                Valores[0] = 9;
                Valores[1] = 7;
                Valores[2] = 8;

                for (int i = 3; i < 12; i++)
                {
                    Valores[i] = int.Parse(Convert.ToString(ISBN[i - 3]));
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

                return String.Format("978{0}{1}", ISBN.Substring(0, 9), digito);
            }
            else if (ISBN.Length == 13)
            {
                int[] Valores = new int[9];
                for (int i = 3; i < 12; i++)
                {
                    Valores[i - 3] = int.Parse(ISBN[i].ToString());
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
                throw new FormatExceptionArgument("ISBN code wrong", new Exception(), ReturnType.InvalidInputFormat);
            }
        }

        /// <summary>
        /// Obtém a capa do livro solicitado
        /// </summary>
        /// <param name="Size">Tamanho da imagem disponível pela API</param>
        /// <param name="KEY">Chave de busca $ISBN</param>
        /// <returns>Retorna a imagem no formato ByteArray[]</returns>
        [Obsolete("Change to new method constructor using Book object")]
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
        /// Obtém a capa do livro solicitado
        /// </summary>
        /// <param name="Size">Tamanho da imagem disponível pela API</param>
        /// <param name="Book">Objeto book que será usado para validar a existência de uma capa disponível pela API</param>
        /// <returns></returns>
        /// <exception cref="Erros.BookException"></exception>

        public static byte[] GetImage(ImageSize Size, Book @Book)
        {
            string MASK = $"https://covers.openlibrary.org/b/isbn/{(string.IsNullOrWhiteSpace(Book.ISBN13) ? Book.ISBN10 : Book.ISBN13)}-{(char)Size}.jpg";
            byte[] IMAGE = null;

            if (Book.HasCover)
            {
                try
                {
                    using (WebClient Cliente = new WebClient())
                    {
                        IMAGE = Cliente.DownloadData(MASK);
                    }

                    return IMAGE; //Image found
                }
                catch (Exception e)
                {
                    throw new BookException("Generic error while getting cover", e);
                }
            }
            else
                throw new BookException("Cover unvaible");
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
        /// <param name="Key">chave de consulta $ISBN</param>
        /// <returns>Imagem do $KEY passado no tamanho $SIZE</returns>
        [Obsolete("Change to new method constructor using Book object")]
        public static Image GetCompostImage(ImageSize Size, string Key)
        {
            var bytes = GetImage(Size, Key);
            if (bytes.Length == 0)
                return null;

            return GetImageFromByteArray(bytes);
        }

        /// <summary>
        /// Obtém diretamente a imagem, executando as rotidas já existentes individuais
        /// </summary>
        /// <param name="Size">Tamanho da imagem disponibilizada pela API</param>
        /// <param name="book">Objeto book associado para a consulta</param>
        /// <returns>Imagem do $KEY passado no tamanho $SIZE</returns>

        public static Image GetCompostImage(ImageSize Size, Book book)
        {
            var bytes = GetImage(Size, book);
            if (bytes.Length == 0)
                return null;

            return GetImageFromByteArray(bytes);
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
