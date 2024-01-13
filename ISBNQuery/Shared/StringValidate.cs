namespace ISBNQuery.Shared
{
    internal class StringValidate
    {
        public static bool IsNumeric(string ISBN, bool IsISBN10 = false)
        {
            string Formated = ISBN;
            if (IsISBN10)
                Formated = ISBN.Replace("X", "");

            char[] chars = Formated.ToCharArray();
            foreach (char c in chars)
                if (c < 0x30 || c > 0x39)
                    return false;

            return true;
        }

        public static string RemoveUnwantedCases(string value)
        {
            value = value.ToUpper();
            string temp = string.Empty;

            foreach (char c in value)
                if ((c >= 48 && c <= 57) || c == 88) // (char)88 == 'X'
                    temp += c.ToString();

            return temp;
        }
    }
}
