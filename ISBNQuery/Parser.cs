using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ISBNQuery
{
    internal class Parser
    {
        public static Book Parse(Rootobject @object)
        {
            Book book = new Book();
            string[] rootValues = { "bib_key", "info_url", "thumbnail_url" };
            foreach (string s in rootValues)
                book.SetPropertie(new KeyValuePair<string, string>(s, @object.Generic[s]));

            string[] derivedValues = { "title", "physical_format", "key", "publish_date", "physical_dimensions", "number_of_pages", "description", "translation_of", "latest_revision", "revision" };
            foreach (string s in derivedValues)
                book.SetPropertie(new KeyValuePair<string, string>(s, @object.Generic.Details[s]));

            //Especial fields
            book.SetPropertie(new KeyValuePair<string, string>("isbn_10", @object.Generic.Details.ISBN10 != null && @object.Generic.Details.ISBN10.Length > 0 ? @object.Generic.Details.ISBN10[0] : ""));
            book.SetPropertie(new KeyValuePair<string, string>("isbn_13", @object.Generic.Details.ISBN13 != null && @object.Generic.Details.ISBN13.Length > 0 ? @object.Generic.Details.ISBN13[0] : ""));
            book.SetPropertie(new KeyValuePair<string, string>("authors", @object.Generic.Details.Authors != null && @object.Generic.Details.Authors.Length > 0 ? @object.Generic.Details.Authors[0].Name : ""));
            book.SetPropertie(new KeyValuePair<string, string>("publishers", @object.Generic.Details.Publishers != null && @object.Generic.Details.Publishers.Length > 0 ? @object.Generic.Details.Publishers[0] : ""));

            return book;
        }

        static void ReplaceSubstring(ref string original, int startIndex, int length, string substitute)
        {
            if (startIndex < 0 || startIndex >= original.Length || length < 0 || startIndex + length > original.Length)
            {
                throw new ArgumentException("Índices inválidos");
            }

            original = original.Remove(startIndex, length).Insert(startIndex, substitute);
        }

        public static Book TryCreateObject(string JSON, int Deslocation)
        {
            ReplaceSubstring(ref JSON, 2, Deslocation, "Generic");
            var obj = JsonConvert.DeserializeObject<Rootobject>(JSON);
            return Parse(obj);
        }
    }

}
