using ISBNQuery.Erros;
using ISBNQuery.Models;
using System.Net;
using System.Text.Json;

namespace ISBNQuery.Shared
{
    internal class DataDownload
    {
        private static readonly HttpClient _httpClient = CreateHttpClient();

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("ISBNQuery/2.0 (+https://github.com/manoel0810/ISBNQuery)");
            return client;
        }

        public static async Task<byte[]> DownloadAsyncData(string url, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (!Internet.CheckInternet())
                throw new InternetException("no internet available", null);

            try
            {
                if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
                {
                    byte[] data = await _httpClient.GetByteArrayAsync(uri, cancellationToken);
                    return data;
                }

                throw new ArgumentException("Invalid URI", nameof(url));
            }
            catch (HttpRequestException httpEx) when (httpEx.StatusCode == HttpStatusCode.NotFound)
            {
                throw new InternetException404($"The resource at {url} was not found.", httpEx);
            }
            catch (Exception ex)
            {
                throw new InternetException($"Error fetching data from {url}", ex);
            }
        }

        public static async Task<string> DownloadStringAsync(string url, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (!Internet.CheckInternet())
                throw new InternetException("no internet available", null);

            try
            {
                if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
                {
                    return await _httpClient.GetStringAsync(uri, cancellationToken);
                }

                throw new ArgumentException("Invalid URI", nameof(url));
            }
            catch (HttpRequestException httpEx) when (httpEx.StatusCode == HttpStatusCode.NotFound)
            {
                throw new InternetException404($"Resource not found at {url}", httpEx);
            }
            catch (Exception ex)
            {
                throw new InternetException($"Error fetching string from {url}", ex);
            }
        }

        public static async Task<Book> DownloadBookDataAsync(string isbnKey, CancellationToken cancellationToken)
        {
            string cleanIsbn = StringValidate.RemoveUnwantedCases(isbnKey);

            if (!Internet.CheckInternet())
                throw new InternetException("no internet available", null);

            // 1. Tentar primeiro o endpoint oficial moderno /isbn/{isbn}.json
            string isbnEndpoint = $"https://openlibrary.org/isbn/{cleanIsbn}.json";
            try
            {
                string json = await DownloadStringAsync(isbnEndpoint, cancellationToken);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    var edition = JsonSerializer.Deserialize<EditionJsonResponse>(json);
                    if (edition != null && !string.IsNullOrWhiteSpace(edition.Title))
                    {
                        Book book = Parser.ParseFromEdition(edition, cleanIsbn);

                        // Tentar buscar autor se tiver chave
                        if (edition.Authors != null && edition.Authors.Length > 0 && !string.IsNullOrWhiteSpace(edition.Authors[0].Key))
                        {
                            try
                            {
                                var authorInfo = await DownloadAuthorDataAsync(edition.Authors[0].Key!, cancellationToken);
                                if (authorInfo != null)
                                {
                                    book.Author = authorInfo.Name;
                                    book.AuthorsList = new List<AuthorInfo> { authorInfo };
                                }
                            }
                            catch { /* ignorar falha ao carregar o autor detalhado */ }
                        }

                        return book;
                    }
                }
            }
            catch (InternetException404)
            {
                // Se não encontrar via /isbn/{isbn}.json, tentar fallback via API /api/books ou /search.json
            }

            // 2. Fallback via API /api/books?bibkeys=ISBN:...&jscmd=details&format=json
            string fallbackUrl = $"https://openlibrary.org/api/books?bibkeys=ISBN:{cleanIsbn}&jscmd=details&format=json";
            try
            {
                string json = await DownloadStringAsync(fallbackUrl, cancellationToken);
                if (string.IsNullOrEmpty(json) || json.Length <= 4)
                    throw new ApiRequestJsonError($"API Request not found for ISBN: {cleanIsbn}", new Exception());

                Book book = Parser.TryCreateObject(json, cleanIsbn.Length == 10 ? 15 : 18);
                return book;
            }
            catch (HttpRequestException httpEx) when (httpEx.StatusCode == HttpStatusCode.NotFound)
            {
                throw new InternetException404("The remote server did not find the passed ISBN or is offline", httpEx);
            }
            catch (Exception ex)
            {
                throw new InternetException("An error occurred in the process of creating the 'Book' object", ex);
            }
        }

        public static async Task<AuthorInfo> DownloadAuthorDataAsync(string authorKeyOrId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(authorKeyOrId))
                throw new ArgumentNullException(nameof(authorKeyOrId));

            string cleanKey = authorKeyOrId.Trim();
            if (!cleanKey.StartsWith("/authors/"))
            {
                cleanKey = "/authors/" + cleanKey;
            }

            string url = $"https://openlibrary.org{cleanKey}.json";
            string json = await DownloadStringAsync(url, cancellationToken);

            var authorDto = JsonSerializer.Deserialize<AuthorJsonResponse>(json);
            if (authorDto == null)
                throw new BookException($"Author data not found for {authorKeyOrId}");

            return Parser.ParseFromAuthor(authorDto);
        }

        public static async Task<IReadOnlyList<AuthorInfo>> SearchAuthorsAsync(string query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query));

            string url = $"https://openlibrary.org/search/authors.json?q={Uri.EscapeDataString(query)}";
            string json = await DownloadStringAsync(url, cancellationToken);

            var searchResult = JsonSerializer.Deserialize<AuthorSearchJsonResponse>(json);
            var list = new List<AuthorInfo>();

            if (searchResult?.Docs != null)
            {
                foreach (var doc in searchResult.Docs)
                {
                    list.Add(new AuthorInfo
                    {
                        Key = doc.Key,
                        Name = doc.Name,
                        BirthDate = doc.BirthDate,
                        TopWork = doc.TopWork,
                        WorkCount = doc.WorkCount
                    });
                }
            }

            return list;
        }
    }
}
