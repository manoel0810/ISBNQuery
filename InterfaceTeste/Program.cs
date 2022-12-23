using ISBNQuery;
using System;
using System.Text.RegularExpressions;

namespace InterfaceTeste
{
    internal class Program
    {
        private static bool Exit = false;

        static void Main(string[] args)
        {
            if (args.Length != 0)
                ReadArgs(args);

            while (!Exit)
            {
                Ponteiro();
                ReadArgs();
            }
        }

        private static void ShowBook(Book book)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("---------------------------------------- DADOS ----------------------------------------\n");
            Console.WriteLine($"Titulo......: {book.Title}");
            Console.WriteLine($"Autor.......: {book.Author}");
            Console.WriteLine($"ISBN10......: {book.ISBN10}");
            Console.WriteLine($"ISBN13......: {book.ISBN13}");
            Console.WriteLine($"Publish Date: {book.Publish_Date}");
            Console.WriteLine("\n---------------------------------------- ***** ----------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        private static void Ponteiro(string MSG = "")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            if (MSG.Equals(""))
                Console.Write("\nISBNQuery -> ");
            else
                Console.WriteLine($"\nISBNQuery [{MSG}] -> ");

            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ReadArgs(string[] _args = null)
        {
            string[] args;

            if (_args != null)
                args = _args;
            else
            {
                Regex regex = new Regex(@"\s{2,}");
                string Argumento = regex.Replace(Console.ReadLine(), " ");
                args = Argumento.Split(' ');
            }

            if (args.Length == 0) { return; }

            if (args[0].Equals("/help"))
            {
                Console.WriteLine("\n---------------------------------------- AJUDA ----------------------------------------\n");
                Console.WriteLine(" /ib10: Fornece suporte à dados isbn10, retornando um objeto Book");
                Console.WriteLine(" /ib13: Fornece suporte à dados isbn13, retornando um objeto Book");
                Console.WriteLine(" /conv: Converte um ISBN10 em ISBN13 e vice-versa");
                Console.WriteLine(" /help: Exibe esta ajuda de opções");
                Console.WriteLine("\n---------------------------------------- ***** ----------------------------------------");
                return;
            }
            else if (args[0].Equals("cls")) { Console.Clear(); }
            else if (args[0].Equals("exit")) { Exit = true; }
            else if (args.Length < 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Número inválido de argumetos");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            else if (args[0].Equals("/ib10")) { ISBN10Utility(args[1]); }
            else if (args[0].Equals("/ib13")) { ISBN13Utility(args[1]); }
            else if (args[0].Equals("/conv")) { ConverteISBN(args[1]); }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Comando inválido");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
        }

        private static void ISBN10Utility(string Args)
        {
            if (Args.Replace(".", "").Replace("-", "").Replace(" ", "").Length != 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tamanho inválido para o ISBN10");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Book book = Consultas.ConsultarISBN10(Args, true);
            if (book != null)
                ShowBook(book);
            else
                Console.WriteLine($"Não foi possível recuparar os dados para o seguinte ISBN: {Args}");

            return;
        }

        private static void ISBN13Utility(string Args)
        {
            if (Args.Replace(".", "").Replace("-", "").Replace(" ", "").Length != 13)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tamanho inválido para o ISBN13");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Book book = Consultas.ConsultarISBN13(Args, true);
            if (book != null)
                ShowBook(book);
            else
                Console.WriteLine($"Não foi possível recuparar os dados para o seguinte ISBN: {Args}");

            return;
        }

        private static void ConverteISBN(string Args)
        {
            string Convertido = Consultas.FormatISBN(Args);
            string Tipo = Convertido.Length.Equals(10) ? "ISBN13" : "ISBN10";
            Signal("return");

            Console.Write($"{Tipo} convertido para: {Convertido}\n");
            return;
        }

        private static void Signal(string MSG)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"[{MSG}] -> ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
