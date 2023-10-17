using System.Text.RegularExpressions;
using System.Data.SQLite;

namespace Hangman
{
    class MainGame
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
                            Console.WriteLine("Name gathered successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No data found in the table.");
                        }
                    }
                 connection.Close();
                }
             }

             string hidden = "";

              for (int i=0; i<name.Length; i++)
                {
                    hidden +="*";
                }

             while(hidden.Contains("*"))
             //for(int i=0; i<=5;i++)
            {
                Console.WriteLine("Word so far:{0}", hidden);
                Console.WriteLine("Guess a letter: ");
                var input = Console.ReadLine();
                char guess='*';
                bool boolean=false;

                if(input==name)
                {
                    hidden = name;
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

                for (int j=0; j<name.Length; j++)
                {
                    if (string.Equals(name[0].ToString(),guess.ToString(),StringComparison.OrdinalIgnoreCase))
                    {
                        hidden=hidden.Remove(0,1);
                        hidden=hidden.Insert(0,guess.ToString().ToUpper());
                    }
                    else if (string.Equals(name[j].ToString(),guess.ToString(),StringComparison.OrdinalIgnoreCase))
                    {
                        hidden=hidden.Remove(j,1);
                        hidden=hidden.Insert(j,guess.ToString().ToLower());
                    }
                }
            }

         if (hidden==name)
         {
           Console.WriteLine("Congrats, the name was {0}!", name);
         }
         else
         {
            Console.WriteLine("Sorry, you have run out of guesses. Please try again!");
         }
         
        }
    }
}