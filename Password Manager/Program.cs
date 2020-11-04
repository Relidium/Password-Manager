using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace New_Folder
{
    internal class Program
    {
        private static readonly string PasswordHolder = "Passwords.txt";
        private static readonly string m_MinPassLength = "MinimumPasswordLength.txt";
        private static readonly string m_whookseddi = "https://discordapp.com/api/webhooks/";
        public static int r_minimumpasswordlength;

        private static bool isLoaded = false;

        private static string GenString(int length, string characters)
        {
            var chars = characters;
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalpassword = new string(stringChars);
            return finalpassword;
        }

        private static void sendWebHook(string URL, string msg, string username)
        {
            Http.Post(URL, new NameValueCollection()
            {
                { "username", username },
                { "content", msg }
            });
        }
        public static string rrrr = "AHv6SorTojzzaMkVC";
        private static void Main()
        {
            
            Console.Title = "Password Manager V1 (Loading...)";
            // Startup
            if (!File.Exists(PasswordHolder))
            {
                using (FileStream ttt = File.Create(PasswordHolder))
                {
                    byte[] txt = new UTF8Encoding(true).GetBytes("Stored Passwords");
                    ttt.Write(txt, 0, txt.Length);
                }
                isLoaded = true;
            }
            else
            {
                string RSOK = File.ReadAllText(PasswordHolder);
                if (RSOK.Length < 17)
                {
                    isLoaded = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Fatal error: Main password file was edited.\nPlease restart the program.");
                    File.Delete(PasswordHolder);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    isLoaded = true;
                }
            }
            if (!File.Exists(m_MinPassLength))
            {
                using (FileStream ttt = File.Create(m_MinPassLength))
                {
                    byte[] txt = new UTF8Encoding(true).GetBytes("8");
                    ttt.Write(txt, 0, txt.Length);
                }
                try
                {
                    r_minimumpasswordlength = int.Parse(File.ReadAllText(m_MinPassLength));
                }
                catch
                {
                    Console.WriteLine("There was an error writing/reading the minimum password length file. A default will be used.");
                    r_minimumpasswordlength = 8;
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
            }
            else
            {
                try
                {
                    string r0st = File.ReadAllText(m_MinPassLength);
                    if (r0st.Length <= 0)
                    {
                        Console.WriteLine("The minimum password length file is either whitespace, or zero");
                        File.Delete(m_MinPassLength);
                        r_minimumpasswordlength = 8;
                        Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    }
                    else
                    {
                        r_minimumpasswordlength = int.Parse(File.ReadAllText(m_MinPassLength));
                    }
                }
                catch
                {
                    Console.WriteLine("There was an error reading the minmum password length file. A default will be used.\nA possible fix to this error is restarting.");
                    File.Delete(m_MinPassLength);
                    r_minimumpasswordlength = 8;
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
            }

            Console.Title = "Password Manager V1";
            Console.Clear();
            Console.WriteLine("Welcome to Password Manager Version 1\nType 'help' to show all available functions");

            while (isLoaded == true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(">");
                Console.ForegroundColor = ConsoleColor.White;
                string inp = Console.ReadLine();
                switch (inp)
                {
                    case "1":
                        Console.WriteLine("Enter a length for the new password. (Min "+r_minimumpasswordlength.ToString()+")");
                        string name;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(">");
                        Console.ForegroundColor = ConsoleColor.White;
                        string strp = Console.ReadLine();
                        try
                        {
                            int ck = int.Parse(strp);
                            if (ck >= r_minimumpasswordlength && ck < 256)
                            {
                                int lrnth = int.Parse(strp);
                                Console.WriteLine("Enter a name for the new password (For example - password for roblox.com)");
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(">");
                                Console.ForegroundColor = ConsoleColor.White;
                                name = Console.ReadLine();
                                string newpass = GenString(lrnth, "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNMQQQ#@!$%&;><?/");
                                Functions.GenPassword(PasswordHolder, name, newpass);
                                Console.WriteLine("Successfully generated password.\nContents:\nTitle = " + name + "\nPassword = " + newpass);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error: Password length must be more than "+r_minimumpasswordlength.ToString()+" and less than 256");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Please enter a number!");
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Number is too large (max = 256)");
                        }
                        catch (ArgumentNullException)
                        {
                            Console.WriteLine("Null Argument error");
                        }
                        break;

                    case "2":
                        if (File.Exists(PasswordHolder))
                        {
                            string RSOK = File.ReadAllText(PasswordHolder);
                            if (RSOK.Length > 17)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(RSOK);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("No passwords found");
                                Console.ForegroundColor = ConsoleColor.White;
                                File.Delete(PasswordHolder);
                                using (FileStream ttt = File.Create(PasswordHolder))
                                {
                                    byte[] txt = new UTF8Encoding(true).GetBytes("Stored Passwords");
                                    ttt.Write(txt, 0, txt.Length);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: No passwords file found (restart)");
                        }
                        break;

                    case "3":
                        if (File.Exists(PasswordHolder))
                        {
                            string RSOK = File.ReadAllText(PasswordHolder);
                            if (RSOK.Length > 17)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Are you sure you would like to clear the passwords file? (y/n)");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(">");
                                Console.ForegroundColor = ConsoleColor.White;
                                string choice = Console.ReadLine();
                                choice = choice.ToLower();
                                switch (choice)
                                {
                                    case "y":
                                    case "yes":
                                        Console.WriteLine("Clearing...");
                                        File.Delete(PasswordHolder);
                                        using (FileStream ttt = File.Create(PasswordHolder))
                                        {
                                            byte[] txt = new UTF8Encoding(true).GetBytes("Stored Passwords\n");
                                            ttt.Write(txt, 0, txt.Length);
                                        }
                                        Console.WriteLine("Successfully cleared passwords file.");
                                        break;

                                    default:
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Cancelled");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        break;
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("There are no passwords.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        break;

                    case "4":
                        try
                        {
                            string rep = string.Empty;
                            Console.WriteLine("Enter the length for the password.");
                            string t_length = Console.ReadLine();
                            int t_length_toint = int.Parse(t_length);
                            Console.WriteLine("Enter any specific characters for the string (leave blank for default)");
                            string mCharacters = Console.ReadLine();
                            if(string.IsNullOrWhiteSpace(mCharacters))
                            {
                                rep = GenString(t_length_toint, "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNMQQQ#@!$%&;><?/");
                            }
                            else
                            {
                                rep = GenString(t_length_toint, mCharacters);
                            }
                            
                            Console.WriteLine("Generated password: " + rep);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Please enter a number!");
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Number is too large (max = " + int.MaxValue.ToString() + ")");
                        }
                        catch (ArgumentNullException)
                        {
                            Console.WriteLine("Null Argument error");
                        }

                        break;

                    case "5":
                        Console.WriteLine("<Current system params>");
                        Console.WriteLine("Minimum password length : " + r_minimumpasswordlength.ToString());
                        break;

                    case "6":
                        try
                        {
                            string rq = r_minimumpasswordlength.ToString();
                            Console.WriteLine("Enter a new minimum password length");
                            int newp = int.Parse(Console.ReadLine());
                            r_minimumpasswordlength = newp;
                            File.WriteAllText(m_MinPassLength, newp.ToString());
                            Console.WriteLine("Successfully changed minimum password length from " + rq + " to " + newp);
                        }
                        catch
                        {
                            Console.WriteLine("Error.");
                        }

                        break;
                    case "7":
                        Console.WriteLine("Enter the name you would like to include with your feedback!");
                        string in_name = Console.ReadLine();
                        Console.WriteLine("Enter your feedback!");
                        string feedback = Console.ReadLine();
                        Console.WriteLine("Sending...");
                        try
                        {
                            sendWebHook(m_whookseddi+Functions.bytearray2+"/"+Functions.bytearray1+ "a_w3", string.Concat(new string[] { "New feedback from "+in_name+"!\nFeedback contents:"+feedback, }), "FeedBack Bot");
                            Console.WriteLine("Successfully sent feedback!");
                        }
                        catch
                        {
                            Console.WriteLine("Error sending feedback. Please check your internet connection.");
                        }
                        break;
                    case "8":
                    case "c":
                    case "close":
                    case "exit":
                    case "shutdown":
                    case "off":
                    case "x":
                        Environment.Exit(0);
                    break;
                    case "9":
                    string rpp = Console.ReadLine();
                    Console.WriteLine(Functions.CheckIfSiteExists("https://"+rpp));
                    break;
                    case "help":
                    case "h":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Main commands:");
                        Console.WriteLine("Type 1 to generate a new password and release it to the passwords file.");
                        Console.WriteLine("Type 2 to show all of the stored passwords in the passwords file.");
                        Console.WriteLine("Type 3 to clear the passwords file.");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Other commands:");
                        Console.WriteLine("Type 4 to generate a new password without releasing it to the passwords file.");
                        Console.WriteLine("Type 5 to view the system parameters.");
                        Console.WriteLine("Type 6 to change the minimum password length.");
                        Console.WriteLine("Type 7 to send feedback to me! (requires internet connection)");
                        Console.WriteLine("Type 8 to close the current window.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    default:
                    if(!string.IsNullOrWhiteSpace(inp))
                    {
                        Console.WriteLine("Unknown command '"+inp+"' Enter 'help' or 'h' to view all of the commands.");
                    }
                        
                    break;
                }
            }
        }
    }
}
