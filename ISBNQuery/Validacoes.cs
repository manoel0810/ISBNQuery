using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace ISBNQuery
{
    /// <summary>
    ///     Esta classe fornece métodos para verificar a validade dos códigos <b>ISBN-10</b> e <b>ISBN-13</b> e formatações <c>UTF-8</c>
    /// </summary>

    internal class Validacoes
    {
        private static readonly Dictionary<string, string> Acentos = new Dictionary<string, string>
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
        /// Verifica a conexão com a internet
        /// </summary>
        /// <param name="UserDefine">Recebe um host para ping definido pelo usuário. O padrão é: google.com</param>
        /// <returns><b>true</b> se a conexão ocorrer com sucesso</returns>

        [Obsolete]
        public static bool Internet(Uri UserDefine = null)
        {
            Uri url = UserDefine ?? new Uri("https://www.google.com.br");
            System.Net.WebRequest WebR;
            System.Net.WebResponse WebRs;
            WebR = System.Net.WebRequest.Create(url);
            try
            {
                WebRs = WebR.GetResponse();
                WebRs.Close();
                WebR = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica a conexão com a internet
        /// </summary>
        /// <param name="URL">Url para ping-test</param>
        /// <param name="Timeout">Tempo de aguado para resposta</param>
        /// <returns><i>true</i> se a conexão ocorrer bem</returns>

        public static bool CheckInternet(string URL = "www.google.com", int Timeout = 3000)
        {
            Ping ping = new Ping();
            try
            {
                PingReply reply = ping.Send(URL, Timeout);
                return (reply.Status == IPStatus.Success);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Este método retorna uma flag que indica se o código <b>ISBN-13</b> é válido
        /// </summary>
        /// <param name="ISBN13">Recebe uma string de treze (13) posições com o <b>ISBN-13</b></param>
        /// <returns>Retorna uma flag do tipo <b><i>ReturnType</i></b> com a responsta do método sobre a entrada <b>[ISBN-13]</b></returns>

        public static ReturnType CheckISBN13(string ISBN13)
        {
            if (!Numeric(ISBN13))
                return ReturnType.InvalidInputFormat;

            if (string.IsNullOrEmpty(ISBN13))
            {
                MessageBox.Show("O argumento [ISBN13] não pode ser nulo", "Argumento inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ReturnType.NullArgumentException;
            }
            else if (ISBN13.Length != 0xd /*13DEC*/)
            {
                MessageBox.Show("O argumento [ISBN13] deve ter tamanho fixo igual a 13 (treze)", "Argumento inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ReturnType.ISBN13LenghtError;
            }

            int[] Produtos = new int[0xc], ISBN = new int[0xd];
            for (int Position = 0x0; Position < ISBN.Length; Position++)
                ISBN[Position] = int.Parse(ISBN13.Substring(Position, 0x1));

            for (int Elemento = 0x0; Elemento < 0xc; Elemento++)
                Produtos[Elemento] = ISBN[Elemento] * (Elemento % 0x2 == 0x0 ? 0x1 : 0x3);

            Soma(Produtos, out int Resultado);
            int Test = (Resultado + ISBN[0xc]) % 0xa;
            if (Test == 0x0)
                return ReturnType.ValidISBN13;
            else
                return ReturnType.InvalidISBN13;
        }

        /// <summary>
        ///     Este método retorna uma flag que indica se o código <b>ISBN-10</b> é válido
        /// </summary>
        /// <param name="ISBN10">Recebe uma string de dez (10) posições com o <b>ISBN-10</b></param>
        /// <returns>Retorna uma flag do tipo <b><i>ReturnType</i></b> com a responsta do método sobre a entrada <b>[ISBN-10]</b></returns>

        public static ReturnType CheckISBN10(string ISBN10)
        {
            if (!Numeric(ISBN10, true))
                return ReturnType.InvalidInputFormat;

            if (string.IsNullOrEmpty(ISBN10))
            {
                MessageBox.Show("O argumento [ISBN10] não pode ser nulo", "Argumento inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ReturnType.NullArgumentException;
            }
            else if (ISBN10.Length != 0xa /*10DEC*/)
            {
                MessageBox.Show("O argumento [ISBN10] deve ter tamanho fixo igual a 10 (dez)", "Argumento inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ReturnType.ISBN10LenghtError;
            }

            int[] Produtos = new int[0x9], ISBN = new int[0xa];
            for (int Position = 0x0; Position < ISBN.Length; Position++)
                ISBN[Position] = ISBN10.Substring(Position, 0x1).ToUpper() == "X" ? 0xa : int.Parse(ISBN10.Substring(Position, 0x1));

            for (int Elemento = 0x0; Elemento < 0x9; Elemento++)
                Produtos[Elemento] = ISBN[Elemento] * (Elemento + 0x1);

            Soma(Produtos, out int Resultado);
            int Test = Resultado % 0xb;
            if (Test == ISBN[0x9])
                return ReturnType.ValidISBN10;
            else
                return ReturnType.InvalidISBN10;
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

                if (s[i + 1] == 92 && s[i + 1] == 92)
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

        /// <summary>
        ///     Efetua uma soma finita de n termos do array [Nuns] e devolve sua soma em <c><b>out</b> int Resultado</c>
        /// </summary>
        /// <param name="Nuns">Recebe um array do tipo int com os números que serão somados</param>
        /// <param name="Resultado">Variável de saída por construtor</param>

        private static void Soma(int[] Nuns, out int Resultado)
        {
            int Soma = 0x0;
            foreach (int Value in Nuns)
                Soma += Value;

            Resultado = Soma;
        }

        /// <summary>
        ///     <br><b>** Método de controle interno **</b></br>
        ///     <br>Verifica se todos os caracteres de uma string são números</br>
        /// </summary>
        /// <param name="ISBN">Recebe a string que será verificada</param>
        /// <param name="IsISBN10">Expecifica se o tipo do ISBN é ISBN-10, onde a ocorrência de 'X' é natural</param>
        /// <returns>Retorna um valor booleano <i><b>true</b></i> caso não haja ocorrências não numéricas, caso contrário, <i><b>false</b></i></returns>

        private static bool Numeric(string ISBN, bool IsISBN10 = false)
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

        /// <summary>
        ///     Este método obtém a versão do OS (Sistema Operacional) onde o software está sendo executado
        /// </summary>
        /// <returns>string: Nome e versão do Windows</returns>

        public static string WindowsVersion()
        {
            string r = "";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection information = searcher.Get();
                if (information != null)
                {
                    foreach (ManagementObject obj in information.Cast<ManagementObject>())
                    {
                        r = obj["Caption"].ToString() + " - " + obj["OSArchitecture"].ToString();
                    }
                }

                r = r.Replace("NT 5.1.2600", "XP");
                r = r.Replace("NT 5.2.3790", "Server 2003");
            }
            return r;
        }



    }

    /// <summary>
    /// Retornos possíveis da validação
    /// </summary>

    [Flags]
    public enum ReturnType : int
    {
        /// <summary>
        /// Indica que o ISBN-13 passado é válido
        /// </summary>
        ValidISBN13 = 0x0,
        /// <summary>
        /// Indica que o ISBN-13 passado é inválido
        /// </summary>
        InvalidISBN13 = 0x1,
        /// <summary>
        /// Indica que o tamanho do fluxo de caracteres do código ISBN-13 difere de 13 dígitos
        /// </summary>
        ISBN13LenghtError = 0x2,
        /// <summary>
        /// Indica que o ISBN-10 é válido
        /// </summary>
        ValidISBN10 = 0x3,
        /// <summary>
        /// Indica que o ISBN-10 é inválido
        /// </summary>
        InvalidISBN10 = 0x4,
        /// <summary>
        /// Indica que o tamanho do fluxo de caracteres do código ISBN-10 difere de 10 dígitos
        /// </summary>
        ISBN10LenghtError = 0x5,
        /// <summary>
        /// Indica que ocorreu um erro na operação de validação
        /// </summary>
        InternalError = 0x6,
        /// <summary>
        /// Indica que a entrada do método de verificação foi <c>null</c>
        /// </summary>
        NullArgumentException = 0x7,
        /// <summary>
        /// Indica que o formato de entrada do fluxo do código ISBN era inválido
        /// </summary>
        InvalidInputFormat = 0x8
    }
}
