using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ISBNQuery
{
    [Serializable]
    internal class Rootobject
    {
        public Generic Generic { get; set; }
    }

    [Serializable]
    internal class Generic
    {
        private readonly Dictionary<string, Func<Generic, string>> Access = new Dictionary<string, Func<Generic, string>>()
        {
            { "bib_key", (obj) => obj.BibKey },
            { "info_url", (obj) => obj.InfoUrl },
            { "preview", (obj) => obj.Preview },
            { "preview_url", (obj) => obj.PreviewUrl },
            { "thumbnail_url", (obj) => obj.ThumbnailUrl }
        };

        private string Query(string key)
        {
            if (Access.TryGetValue(key, out var f))
            {
                return f(this);
            }

            return null;
        }

        public string this[string key] => Query(key);

        [JsonProperty("bib_key")]
        public string BibKey { get; set; }

        [JsonProperty("info_url")]
        public string InfoUrl { get; set; }

        [JsonProperty("preview")]
        public string Preview { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }

        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        [JsonProperty("details")]
        public Details Details { get; set; }
    }

    [Serializable]
    internal class Details
    {
        private readonly Dictionary<string, Func<Details, string>> Access = new Dictionary<string, Func<Details, string>>()
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

        private string Query(string key)
        {
            if (Access.TryGetValue(key, out var f))
            {
                return f(this);
            }

            return null;
        }

        public string this[string key] => Query(key);

        [JsonProperty("type")]
        public Type BookType { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        [JsonProperty("authors")]
        public Author[] Authors { get; set; }

        [JsonProperty("source_records")]
        public string[] SourceRecords { get; set; }

        [JsonProperty("publishers")]
        public string[] Publishers { get; set; }

        [JsonProperty("physical_format")]
        public string PhysicalFormat { get; set; }

        [JsonProperty("covers")]
        public int[] Covers { get; set; }

        [JsonProperty("works")]
        public Work[] Works { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("identifiers")]
        public Identifiers Identifiers { get; set; }

        [JsonProperty("classifications")]
        public Classifications Classifications { get; set; }

        [JsonProperty("contributors")]
        public Contributor[] Contributors { get; set; }

        [JsonProperty("publish_date")]
        public string PublishDate { get; set; }

        [JsonProperty("languages")]
        public Language[] Languages { get; set; }

        [JsonProperty("physical_dimensions")]
        public string PhysicalDimensions { get; set; }

        [JsonProperty("number_of_pages")]
        public int NumbeOfPages { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("translation_of")]
        public string TranslationOf { get; set; }

        [JsonProperty("publish_places")]
        public string[] PublishPlaces { get; set; }

        [JsonProperty("translated_from")]
        public Translated_From[] TranslatedFrom { get; set; }

        [JsonProperty("isbn_10")]
        public string[] ISBN10 { get; set; }

        [JsonProperty("isbn_13")]
        public string[] ISBN13 { get; set; }

        [JsonProperty("latest_revision")]
        public int LatestRevision { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("created")]
        public Created Created { get; set; }

        [JsonProperty("last_modified")]
        public Last_Modified LastModified { get; set; }
    }

    [Serializable]
    internal class Type
    {
        [JsonProperty("key")]
        public string Key { get; set; }
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
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public DateTime Value { get; set; }
    }

    [Serializable]
    internal class Last_Modified
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public DateTime Value { get; set; }
    }

    [Serializable]
    internal class Author
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    [Serializable]
    internal class Work
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }

    [Serializable]
    internal class Contributor
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }

    [Serializable]
    internal class Language
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }

    [Serializable]
    internal class Translated_From
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
