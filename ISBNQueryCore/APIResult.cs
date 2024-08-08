using System.Text.Json.Serialization;

namespace ISBNQuery
{
    [Serializable]
    internal class Rootobject
    {
        public Generic Generic { get; set; } = default!;
    }

    [Serializable]
    internal class Generic
    {
        //TODO: manipulate arrays so that json returns with more than one list component can be accessed by indexing

        private readonly Dictionary<string, Func<Generic, string?>> Access = new()
        {
            { "bib_key", (obj) => obj.BibKey },
            { "info_url", (obj) => obj.InfoUrl },
            { "preview", (obj) => obj.Preview },
            { "preview_url", (obj) => obj.PreviewUrl },
            { "thumbnail_url", (obj) => obj.ThumbnailUrl }
        };

        private string? Query(string key)
        {
            if (Access.TryGetValue(key, out var f))
            {
                return f(this);
            }

            return null;
        }

        public string? this[string key] => Query(key);

        [JsonPropertyName("bib_key")]
        public string? BibKey { get; set; }

        [JsonPropertyName("info_url")]
        public string? InfoUrl { get; set; }

        [JsonPropertyName("preview")]
        public string? Preview { get; set; }

        [JsonPropertyName("preview_url")]
        public string? PreviewUrl { get; set; }

        [JsonPropertyName("thumbnail_url")]
        public string? ThumbnailUrl { get; set; }

        [JsonPropertyName("details")]
        public Details? Details { get; set; }
    }

    [Serializable]
    internal class Details
    {
        private readonly Dictionary<string, Func<Details, string?>> Access = new()
        {
            { "title", (obj) => obj.Title },
            { "subtitle", (obj) => obj.Subtitle },
            { "physical_format", (obj) => obj.PhysicalFormat },
            { "key", (obj) => obj.Key },
            { "publish_date", (obj) => obj.PublishDate },
            { "physical_dimensions", (obj) => obj.PhysicalDimensions },
            { "number_of_pages", (obj) => obj.NumbeOfPages.ToString() },
            { "description", (obj) => obj.Description },
            { "translation_of", (obj) => obj.TranslationOf },
            { "latest_revision", (obj) => obj.LatestRevision.ToString() },
            { "revision", (obj) => obj.Revision.ToString() },
        };

        private string? Query(string key)
        {
            if (Access.TryGetValue(key, out var f))
            {
                return f(this);
            }

            return null;
        }

        public string? this[string key] => Query(key);

        [JsonPropertyName("type")]
        public Type? BookType { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("subtitle")]
        public string? Subtitle { get; set; }

        [JsonPropertyName("authors")]
        public Author[]? Authors { get; set; }

        [JsonPropertyName("source_records")]
        public string[]? SourceRecords { get; set; }

        [JsonPropertyName("publishers")]
        public string[]? Publishers { get; set; }

        [JsonPropertyName("physical_format")]
        public string? PhysicalFormat { get; set; }

        [JsonPropertyName("covers")]
        public int[]? Covers { get; set; }

        [JsonPropertyName("works")]
        public Work[]? Works { get; set; }

        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("identifiers")]
        public Identifiers? Identifiers { get; set; }

        [JsonPropertyName("classifications")]
        public Classifications? Classifications { get; set; }

        [JsonPropertyName("contributors")]
        public Contributor[]? Contributors { get; set; }

        [JsonPropertyName("publish_date")]
        public string? PublishDate { get; set; }

        [JsonPropertyName("languages")]
        public Language[]? Languages { get; set; }

        [JsonPropertyName("physical_dimensions")]
        public string? PhysicalDimensions { get; set; }

        [JsonPropertyName("number_of_pages")]
        public int NumbeOfPages { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("translation_of")]
        public string? TranslationOf { get; set; }

        [JsonPropertyName("publish_places")]
        public string[]? PublishPlaces { get; set; }

        [JsonPropertyName("translated_from")]
        public Translated_From[]? TranslatedFrom { get; set; }

        [JsonPropertyName("isbn_10")]
        public string[]? ISBN10 { get; set; }

        [JsonPropertyName("isbn_13")]
        public string[]? ISBN13 { get; set; }

        [JsonPropertyName("latest_revision")]
        public int LatestRevision { get; set; }

        [JsonPropertyName("revision")]
        public int Revision { get; set; }

        [JsonPropertyName("created")]
        public Created? Created { get; set; }

        [JsonPropertyName("last_modified")]
        public Last_Modified? LastModified { get; set; }
    }

    [Serializable]
    internal class Type
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }
    }

    [Serializable]
    internal class Identifiers
    {

    }

    [Serializable]
    internal class Classifications
    {

    }

    [Serializable]
    internal class Created
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("value")]
        public DateTime Value { get; set; }
    }

    [Serializable]
    internal class Last_Modified
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("value")]
        public DateTime Value { get; set; }
    }

    [Serializable]
    internal class Author
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    [Serializable]
    internal class Work
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }
    }

    [Serializable]
    internal class Contributor
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }
    }

    [Serializable]
    internal class Language
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }
    }

    [Serializable]
    internal class Translated_From
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }
    }
}
