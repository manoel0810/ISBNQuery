using System.Runtime.InteropServices;

namespace ISBNQuery.Shared
{
    internal class WindowsHelp
    {
        public static string WindowsVersion()
        {
            return RuntimeInformation.OSDescription;
        }
    }
}
