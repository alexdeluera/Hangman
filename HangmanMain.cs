using System.Text.RegularExpressions;
using System.Data.SQLite;

namespace Hangman
{
    class Example3
    {
        public static void Main(string[] args)
        {
            /* Create a game similar to Hangman in which a player guesses
             * letters to try to replicate a hidden word. Store at least eight
             * words. Initially, display the hidden word using asterisks to
             * represent each letter. Allow the user to guess letters to replace
             * the asterisks in the hidden word until the user completes the 
             * entire word. If the user guess a letter that appears multiple
             * times in the hidden word, make sure that each correct letter is
             * placed. */

             string name="";
             string connectionString = "Data Source=mydatabase.db;Version=3;";

             
             using (SQLiteConnection connection = new SQLiteConnection(connectionString))
             {  connection.Open();
            
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {  command.CommandText = "SELECT * FROM NameTable ORDER BY RANDOM() LIMIT 1;";

                 using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                            name = reader.GetString(reader.GetOrdinal("Name"));
                            }
                        }
                        else
                        {
                            Console.WriteLine("No data found in the table.");
                        }
                    }
                 Console.WriteLine("Name gathered successfully.");
                 connection.Close();
                }
             }

             string hidden = "";

              for (int i=0; i<name.Length; i++)
                {
                    hidden +="*";
                }

             while(hidden.Contains("*"))
            {
                Console.WriteLine("Word so far:{0}", hidden);
                Console.WriteLine("Guess a letter: ");
                var input = Console.ReadLine();
                char guess='*';
                bool boolean=false;

                if(input==name)
                {
                    Console.WriteLine("Congrats, the name was {0}!", name);
                    break;
                }
                
                while(!boolean)
                {
                    boolean = char.TryParse(input, out guess); 
                    var validChar = new Regex("^[A-Za-z]$");
                    if(!validChar.IsMatch(guess.ToString()) || !boolean)
                    {
                        Console.WriteLine("Input is invalid, please try again!");
                        input = Console.ReadLine();
                    }
                }

                for (int i=0; i<name.Length; i++)
                {
                    if (string.Equals(name[0].ToString(),guess.ToString(),StringComparison.OrdinalIgnoreCase))
                    {
                        hidden=hidden.Remove(0,1);
                        hidden=hidden.Insert(0,guess.ToString().ToUpper());
                    }
                    else if (string.Equals(name[i].ToString(),guess.ToString(),StringComparison.OrdinalIgnoreCase))
                    {
                        hidden=hidden.Remove(i,1);
                        hidden=hidden.Insert(i,guess.ToString().ToLower());
                    }
                }
            }
         Console.WriteLine("Congrats, the name was {0}!", name);
        }
    }
}