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
                   
            // Load all players into list
            RPS_Lib.players = LoadPlayerData();
            int menuReturn = initialMenuPrompt();
            if(menuReturn == -1) {
                return;
            }
            
            Console.Clear();


            // Query List for player
            // If player exists, load stats
            if(menuReturn == 1) {
                RPS_Lib.welcomeMsg = ($"Welcome back { RPS_Lib.currentPlayerName }. Let's play!");
            }
            else {
                RPS_Lib.welcomeMsg = ($"Hello { RPS_Lib.currentPlayerName }. Let's play!\n");
                RPS_Lib.currentPlayer = new Player(RPS_Lib.currentPlayerName);
            }

            bool newGame = true;

            while (true) {
                // Display menu
                // Get Input
                int menuSelect = displayMainMenu(newGame);
                
                // Run Subroutine from Input
                switch(menuSelect){
                    // Play Game
                    case 1: 
                        // Create new Game
                        // Get Random option for PC
                        RPS_Lib.game = new Game(RPS_Lib.currentPlayer.TotalGames+1);

                        // Get User Option
                        RPS_Lib.game.getUserPlay();

                        // Compare to see who won

                        // Add W/L/D to playerStat
                        RPS_Lib.currentPlayer.addGame(RPS_Lib.game.Result);

                        newGame = false;

                        break;
                    // View Player Stats
                    case 2: 
                        // Run static queries
                        // Display output
                        displayPlayerStats();
                        
                        break;
                    // Exit
                    case 3: 
                        // Run static queries
                        showLeaderboard();
                        // Display output
                        break;
                    // Exit
                    case 4:
                        updateList();
                        // Write playerStat to file
                        saveAll();
                        return;
                    default: 
                        RPS_Lib.consoleSendLine("I don't think that was a valid choice. Try again?");
                        break;
                }
                // Loop
            }
        }
    }
}
