using ISBNQuery;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ISBNQueryTeste
{
    [TestClass]
    public class ValidacoesTeste
    {
        [TestMethod]
        public void InternetTeste()
        {
            //Throws check
            Assert.ThrowsException<ArgumentNullException>(() => { Validacoes.CheckInternet(null); });
            Assert.ThrowsException<Exception>(() => { Validacoes.CheckInternet("www.google.com", 1000); });
            
            //Method results check
            Assert.IsFalse(Validacoes.CheckInternet("www.a.com"));
            Assert.IsTrue(Validacoes.CheckInternet());
        }

        [TestMethod]
        public void CheckISBN13Teste()
        {
            Assert.IsTrue(Validacoes.CheckISBN13(null) == ReturnType.NullArgumentException);
            Assert.IsTrue(Validacoes.CheckISBN13("123456789123x") == ReturnType.InvalidInputFormat);
            Assert.IsTrue(Validacoes.CheckISBN13("9788551005193") == ReturnType.InvalidISBN13);
            Assert.IsTrue(Validacoes.CheckISBN13("123456789") == ReturnType.ISBN13LenghtError);
            Assert.IsTrue(Validacoes.CheckISBN13("1111111111110") == ReturnType.InvalidISBN13);
            Assert.IsTrue(Validacoes.CheckISBN13("9788551005194") == ReturnType.ValidISBN13);
        }

        [TestMethod]
        public void CheckISBN10Teste()
        {
            Assert.IsTrue(Validacoes.CheckISBN10(null) == ReturnType.NullArgumentException);
            Assert.IsTrue(Validacoes.CheckISBN10("123456789f") == ReturnType.InvalidInputFormat);
            Assert.IsTrue(Validacoes.CheckISBN10("123456789X") != ReturnType.InvalidInputFormat);
            Assert.IsTrue(Validacoes.CheckISBN10("1234567899") == ReturnType.InvalidISBN10);
            Assert.IsTrue(Validacoes.CheckISBN10("12345678") == ReturnType.ISBN10LenghtError);
            Assert.IsTrue(Validacoes.CheckISBN10("1111111110") == ReturnType.InvalidISBN10);
            Assert.IsTrue(Validacoes.CheckISBN10("8551005197") == ReturnType.ValidISBN10);
        }

        [TestMethod]
        public void CheckUTFFormat()
        {
            var dictionary = Validacoes.GetInternalDictionary();

            string comparable = "";
            string toFormat = "";
            foreach(var key in dictionary)
            {
                comparable += $"x{key.Value}";
                toFormat += $"x{key.Key}";
            }

            Assert.AreEqual(comparable, Validacoes.FormatUTF8(toFormat));
        }
    }
}
