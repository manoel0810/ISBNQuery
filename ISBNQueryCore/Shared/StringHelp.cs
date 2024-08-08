namespace ISBNQuery.Shared
{
    /// <summary>
    ///     Esta classe fornece métodos para verificar a validade dos códigos <b>ISBN-10</b> e <b>ISBN-13</b> e formatações <c>UTF-8</c>
    /// </summary>

    public class StringHelp
    {
        private static readonly Dictionary<string, string> Acentos = new()
        {
            {@"\u00e1", "á" },  {@"\u00e9", "é" },    {@"\u00ef", "ï"},    {@"\u00d3", "Ó"},    {@"\u00d9", "Ù"},
            {@"\u00e0", "à" },  {@"\u00e8", "è" },    {@"\u00cd", "Í"},    {@"\u00d2", "Ò"},    {@"\u00db", "Û"},
            {@"\u00e2", "â" },  {@"\u00ea", "ê" },    {@"\u00cc", "Ì"},    {@"\u00d4", "Ô"},    {@"\u00e7", "ç"},
            {@"\u00e3", "ã" },  {@"\u00c9", "É" },    {@"\u00ce", "Î"},    {@"\u00d5", "Õ"},    {@"\u00c7", "Ç"},
            {@"\u00e4", "ä" },  {@"\u00c8", "È" },    {@"\u00cf", "Ï"},    {@"\u00d6", "Ö"},    {@"\u00f1", "ñ"},
            {@"\u00c1", "Á" },  {@"\u00ca", "Ê" },    {@"\u00f3", "ó"},    {@"\u00fa", "ú"},    {@"\u00d1", "Ñ"},
            {@"\u00c0", "À" },  {@"\u00cb", "Ë" },    {@"\u00f2", "ò"},    {@"\u00f9", "ù"},    {@"\u0026", "&"},
            {@"\u00c2", "Â" },  {@"\u00ed", "í" },    {@"\u00f4", "ô"},    {@"\u00fb", "û"},    {@"\u0027", "'"},
            {@"\u00c3", "Ã" },  {@"\u00ec", "ì" },    {@"\u00f5", "õ"},    {@"\u00fc", "ü"},
            {@"\u00c4", "Ä" },  {@"\u00ee", "î" },    {@"\u00f6", "ö"},    {@"\u00da", "Ú"},
        };

        /// <summary>
        /// Obtém o unicode de união de um caractere
        /// </summary>
        /// <param name="u">Identidade Unicode HEX</param>
        /// <returns>Char unicode</returns>

        private static char GetUnicode(string u)
        {
            Dictionary<string, char> Unicodes = new Dictionary<string, char>(){

                {"0300", '\u0300'},
                {"0301", '\u0301'},
                {"0302", '\u0302'},
                {"0303", '\u0303'},
                {"0304", '\u0304'},
                {"0308", '\u0308'},
                {"0327", '\u0327'},
            };

            foreach (KeyValuePair<string, char> KVP in Unicodes)
            {
                if (KVP.Key == u)
                    return KVP.Value;
            }

            return Char.MinValue;
        }

        /// <summary>
        ///     Retorna para o usuário uma string com acentuação foramatada no <b>UTF-8</b>
        /// </summary>
        /// <param name="NonUTF8">Recebe a string que deve ser formatada</param>
        /// <returns>Uma string formatada em <b>UTF-8</b></returns>

        public static string FormatUTF8(string NonUTF8)
        {

            string UpdatingSTG = NonUTF8.Replace("\\", @"\");
            foreach (KeyValuePair<string, string> KVP in Acentos)
                UpdatingSTG = UpdatingSTG.Replace(KVP.Key, KVP.Value);

            return UpdatingSTG;
        }

        /// <summary>
        /// Retorna uma cadeia de caracteres formatada com acentos especiais e sinais, com base em cobinação de caracteres
        /// </summary>
        /// <param name="s">Cadeia de caracteres Unicode</param>
        /// <returns>Uma string formatada em UTF-8</returns>

        public static string FormatUnicodeCaracters(string s)
        {
            string NS = string.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                if (i + 2 >= s.Length)
                {
                    NS += s[i];
                    goto END;
                }

                if (s[i + 1] == 92)
                {
                    string u = s[i].ToString();
                    u += GetUnicode(s.Substring(i + 3, 4));
                    NS += u.Normalize();
                    i += 6;
                }
                else
                    NS += s[i];

                END:;
            }

            return NS;
        }

        /// <summary>
        /// Força a formatação da cadeia de caracteres, usando dois modelos base
        /// </summary>
        /// <param name="s">String com caracteres Unicode</param>
        /// <returns>Texto formatado</returns>
        /// 

        public static string FullFormat(string s)
        {
            return FormatUnicodeCaracters(FormatUTF8(s));
        }
    }
}
