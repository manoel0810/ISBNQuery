using System.Net.NetworkInformation;

namespace ISBNQuery.Shared
{
    internal class Internet
    {
        /// <summary>
        /// Verifica a conexão com a internet
        /// </summary>
        /// <param name="URL">Url para ping-test</param>
        /// <param name="Timeout">Tempo de aguado para resposta, compreendendo um intervalo de [1500 ~ 10000ms]</param>
        /// <returns><i>true</i> se a conexão ocorrer bem</returns>

        public static bool CheckInternet(string URL = "www.google.com", int Timeout = 3000)
        {
            if (string.IsNullOrEmpty(URL))
                throw new ArgumentNullException(nameof(URL), "The 'Url' cannot be null or empty");

            if (Timeout < 1500 || Timeout > 10000)
                throw new Exception("The timeout value must be between 1500 ~ 10000ms");

            Ping ping = new();
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
    }
}
