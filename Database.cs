using System.Data.SQLite;

namespace Hangman
{
    class Database
    {
    static void Method()
    {  
        string[] words={"Sawyer","Ben","Jack","Kate","Locke","Sayid","Juliet","Charlie","Claire","Hurley"};

        // Define the connection string, including the database file name
        string connectionString = "Data Source=mydatabase.db;Version=3;";

        // Create a new SQLite database file if it doesn't exist
        // SQLiteConnection.CreateFile("mydatabase.db");

        // Open a connection to the database
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // SQL statement to create a table
                command.CommandText = "CREATE TABLE IF NOT EXISTS NameTable (Id INTEGER PRIMARY KEY, Name TEXT UNIQUE);";

                // Execute the SQL command to create the table
                command.ExecuteNonQuery();

                Console.WriteLine("Table created successfully.");
            }

            using (SQLiteCommand commandStr = new SQLiteCommand(connection))
            {   // Start building the SQL command
                commandStr.CommandText = "INSERT or IGNORE INTO NameTable (NAME) VALUES (";

                for (int i = 0; i < words.Length; i++)
                {
                    // Use proper parameter names
                    commandStr.Parameters.AddWithValue($"@name{i}", words[i]);

                    // Add a placeholder for each parameter
                    commandStr.CommandText += $"@name{i}";

                    // Add correct syntax between each entry for SQL Query to run
                    if (i < words.Length - 1)
                    {
                        commandStr.CommandText += "), (";
                    }
                }
             // Complete the SQL command
             commandStr.CommandText += ")";

             // Execute the SQL command
             commandStr.ExecuteNonQuery();
            }

            Console.WriteLine("Data inserted into the table.");

            // Close connection
            connection.Close();    
        }
    }
}
}
