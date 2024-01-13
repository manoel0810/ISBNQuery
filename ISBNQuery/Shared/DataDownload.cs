using ISBNQuery.Erros;
using System;
using System.Net;
using System.Threading.Tasks;
namespace ISBNQuery.Shared
{
    internal class DataDownload
    {
        private static readonly string _url = "https://openlibrary.org/api/books?bibkeys=ISBN:";

        public static async Task<byte[]> DownloadAsyncData(string endPoibt)
        {
            if (string.IsNullOrWhiteSpace(endPoibt))
                throw new ArgumentNullException(nameof(endPoibt));

            if (!Internet.CheckInternet())
                throw new InternetException("no internet avaible", null);

            byte[] data = new byte[1024];
            using (WebClient client = new WebClient())
            {
                try
                {
                    if (Uri.TryCreate(endPoibt, UriKind.Absolute, out Uri uri))
                    {
                        data = await client.DownloadDataTaskAsync(uri);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return data;
        }

        public static async Task<Book> DownloadBookDataAsync(string key)
        {
            key = StringValidate.RemoveUnwantedCases(key);

            try
            {
                if (!Internet.CheckInternet())
                    throw new InternetException("no internet avaible", null);

                using (WebClient Client = new WebClient())
                {
                    if (WindowsHelp.WindowsVersion().Contains("Windows 7"))
                    {
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    }

                    string searchKey = string.Format("{0}{1}&jscmd=details&format=json", _url, key);
                    string content = string.Empty;
                    if (Uri.TryCreate(searchKey, UriKind.Absolute, out Uri uri))
                    {
                        content = await Client.DownloadStringTaskAsync(uri);
                    }
                    else
                        throw new InternetException("there was a error while parsing Url to Uri object", null);

                    if (string.IsNullOrEmpty(content) || content.Length <= 4)
                        throw new ApiRequestJsonError($"API Request not found for: {searchKey}", new Exception());

                    Book book = Parser.TryCreateObject(content, key.Length == 10 ? 15 : 18);
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
    }
}
