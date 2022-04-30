using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RockPaperScissors
{
    partial class Program
    {
        static void Main(string[] args)
        {   
            List<Player> players = new List<Player>();       
            // Load all players into list
            players = LoadPlayerData();

            // Get Name
            string name = promptPlayerName();

            // Query List for player
            // If player exists, load stats
            Player currentPlayer;
            if((currentPlayer = (from p in players where p.Name == name select p).FirstOrDefault()) == null) {
                // if no player exists, create in list (all lowercase) with all 0's, write file
                currentPlayer = new Player(name);
                consoleSendLine($"Player { name } was not found and has been created as a new player");
                Log("Info",$"Player { name } was not found and has been created as a new player");
            }
            else {
                consoleSendLine($"Welcome Back {currentPlayer.Name}!");
                consoleSendLine($"Previous Stats:");
                consoleSendLine($"\tWins: {currentPlayer.Wins} ({currentPlayer.WinRatio.ToString("G2")}%)");
                consoleSendLine($"\tLosses: {currentPlayer.Losses} ({currentPlayer.LossRatio.ToString("G2")}%)");
                consoleSendLine($"\tDraws: {currentPlayer.Draws} ({currentPlayer.DrawRatio.ToString("G2")}%)");
            }

            while(true) {
                // Display menu
                // Get Input
                int menuSelect = displayMainMenu();
                
                // Run Subroutine from Input
                switch(menuSelect){
                    // Play Game
                    case 1: 
                        // Create new Game
                        // Get Random option for PC
                        Game game = new Game();

                        // Get User Option
                        game.getUserPlay();

                        // Compare to see who won
                        // Add W/L/D to playerStat
                        currentPlayer.addGame(game.Result);
                        addTransation(currentPlayer.Name,game.ToString());
                        
                        // Display W/L/D to player
                        game.displayResult();
                        break;
                    // View Stats
                    case 2: 
                        // Run static queries
                        // Display output
                        break;
                    // Exit
                    case 3:
                        updateList(players, currentPlayer);
                        // Write playerStat to file
                        saveAll(players);

                        return;
                    default: 
                        break;
                }
                // Loop
            }
        }
    }
}
