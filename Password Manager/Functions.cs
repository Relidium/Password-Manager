using System.IO;

namespace Password_Manager
{
    internal class Functions
    {
        public static readonly string bytearray1 = "tqq6VsxI_d9iaUt7gDgGxnU84a"+"RPFFY5jkYXPz1_CKf3iKl"+Program.rrrr;
        public static readonly string bytearray2 = "764550993311957043";
        public static void GenPassword(string path, string title, string newpass)
        {
            File.WriteAllText(path, File.ReadAllText(path) + "Title: " + title + " | Password: " + newpass + "\n");
        }
    }
}