# Hangman
A command line game similar to Hangman, where the user tries to guess a hidden word that has been pulled at random from the associated database.

This version of the game Hangman was initially created as part of a C# course, however, the the HangmanMain.cs file has been expanded upon as my familiarity and comfort with programming increased, particularly logic and nested loops. Eventually, the game came to include a SQLite database (mydatabase.db) which is created and updated using the Database.cs file. The database contains a list of character names from the show Lost. When the HangmanMain.cs file runs, a pseudo-random row is chosen and the name in that row is used at the hidden word, which a player must then guess. 

I prefer to keep games lighthearted, so the version of this game that is uploaded in HangmanMain.cs has unlimited guesses. However, there is a comment, containing a for loop, below the while loop on line 55 that may easily be swapped in place of the while loop to implement a 5 guess maximum. Of course, this number of guesses may also be updated to alter the difficulty level. 
