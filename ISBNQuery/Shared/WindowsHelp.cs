using System.Linq;
using System.Management;

namespace ISBNQuery.Shared
{
    internal class WindowsHelp
    {
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
}
