using ISBNQuery.Erros;

#nullable disable

namespace ISBNQuery.Shared
{
    internal class DataDownload
    {
        private static readonly string _url = "https://openlibrary.org/api/books?bibkeys=ISBN:";

        public static async Task<byte[]> DownloadAsyncData(string endPoibt, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(endPoibt))
                throw new ArgumentNullException(nameof(endPoibt));

            if (!Internet.CheckInternet())
                throw new InternetException("no internet available", null);

            using HttpClient client = new();
            try
            {
                if (Uri.TryCreate(endPoibt, UriKind.Absolute, out Uri uri))
                {
                    byte[] data = await client.GetByteArrayAsync(uri, cancellationToken);
                    return data;
                }
                else
                {
                    throw new ArgumentException("Invalid URI", nameof(endPoibt));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Book> DownloadBookDataAsync(string key, CancellationToken cancellationToken)
        {
            key = StringValidate.RemoveUnwantedCases(key);

            try
            {
                if (!Internet.CheckInternet())
                    throw new InternetException("no internet available", null);

                using HttpClient client = new();
                string searchKey = string.Format("{0}{1}&jscmd=details&format=json", _url, key);

                if (Uri.TryCreate(searchKey, UriKind.Absolute, out Uri uri))
                {
                    string content = await client.GetStringAsync(uri, cancellationToken);

                    if (string.IsNullOrEmpty(content) || content.Length <= 4)
                        throw new ApiRequestJsonError($"API Request not found for: {searchKey}", new Exception());

                    Book book = Parser.TryCreateObject(content, key.Length == 10 ? 15 : 18);
                    return book;
                }
                else
                {
                    throw new InternetException("there was an error while parsing URL to URI object", null);
                }
            }
            catch (HttpRequestException httpEx) when (httpEx.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new InternetException404("The remote server did not find the passed ISBN or is offline", httpEx);
            }
            catch (Exception ex)
            {
                throw new InternetException("An error occurred in the process of creating the 'Book' object", ex);
            }
        }

    }
}
