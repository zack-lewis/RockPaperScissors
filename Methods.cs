using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RockPaperScissors {
    partial class Program {

        private static int initialMenuPrompt() {
            // Returns: 
            // -1: Exit
            // 0: New player
            // 1: Existing Player
            while(true) {    
                Console.Clear();
                if(RPS_Lib.inputErrorMsg != null) {
                    RPS_Lib.consoleSendLine(RPS_Lib.inputErrorMsg);
                    RPS_Lib.inputErrorMsg = null;
                }

                List<string> lines = new List<string>();
                lines.Add("Welcome to Rock, Paper, Scissors!");
                lines.Add("1. New Player");
                lines.Add("2. Load Player");
                lines.Add("3. Quit");
                RPS_Lib.showMenu(lines);

                string initialMenuPrompt = RPS_Lib.sendPrompt("Enter Choice:");
                
                try {
                    int newOrLoad = Int32.Parse(initialMenuPrompt);
                    if(newOrLoad == 1 || newOrLoad == 2) {
                        RPS_Lib.currentPlayerName = promptPlayerName();
                        RPS_Lib.currentPlayer = (from p in RPS_Lib.players where p.Name == RPS_Lib.currentPlayerName select p).FirstOrDefault();
                    }
                    switch(newOrLoad) {
                        case 1:
                            if (RPS_Lib.currentPlayer == null) {
                                return 0;
                            }
                            else {
                                RPS_Lib.inputErrorMsg = ($"Sorry { RPS_Lib.currentPlayerName }, your game already exists.\n");
                                continue;
                            }
                        case 2:
                            // Get Name
                            if (RPS_Lib.currentPlayer != null) {
                                return 1;  
                            }
                            else {
                                RPS_Lib.inputErrorMsg = ($"Sorry { RPS_Lib.currentPlayerName }, your game could not be found.\n");
                                continue;
                            }
                        case 3:
                            return -1;
                        default:
                            continue;                            
                    }
                }
                catch (Exception ex) {
                    Log("Error", ex.Message);
                    RPS_Lib.inputErrorMsg = ("I don't think that's an option\n");
                    continue;
                }
            }
        }
        
        private static void updateList()
        {
            Player remove = (from p in RPS_Lib.players where p.Name == RPS_Lib.currentPlayer.Name select p).FirstOrDefault();
            if(remove != null) {
                RPS_Lib.players.Remove(remove);
            }
            RPS_Lib.players.Add(RPS_Lib.currentPlayer);

        }

        private static void saveAll()
        {
            try {
                using(StreamWriter playerDataSW = new StreamWriter(RPS_Lib.playerLogFile,false)) {
                    foreach(Player p in RPS_Lib.players) {
                        playerDataSW.WriteLine(p.ToString());
                    }
                }
            }
            catch(Exception ex) {
                Log("Error",$"Unable to save player data: {ex.Message}");
            }
        }

        private static List<Player> LoadPlayerData()
        {
            List<Player> loadList = new List<Player>();
            try {
                using(StreamReader sr = new StreamReader(RPS_Lib.playerLogFile)) {
                    string data = "";
                    int lineCount = 0;
                    while((data = sr.ReadLine()) != null){
                        string[] line = data.Split(",");
                        try {
                            Player inP = new Player(line[0].ToString(),Int32.Parse(line[1].ToString()),Int32.Parse(line[2].ToString()),Int32.Parse(line[3].ToString()));
                            Game.totalWins += inP.Wins;
                            Game.totalLosses += inP.Losses;
                            Game.totalDraws += inP.Draws;

                            loadList.Add(inP);
                        }
                        catch(Exception) {
                            Log("Warning", $"Exception importing player data on line { lineCount }. Skipping line: \n\t { data } ");
                            continue;
                        }
                        lineCount++;
                    }
                }
            }
            catch(Exception ex) {
                Log("Error",$"Exception reading player data file: { ex.Message }");
            }

            return loadList;
        }

        private static void Log(string loglevel, string message)
        {
            string timestamp = $"{ DateTime.Now.Month }/{ DateTime.Now.Day }/{ DateTime.Now.Year } { DateTime.Now.TimeOfDay.ToString() }";
            using(StreamWriter logSW = new StreamWriter(RPS_Lib.appLog,true)) {
                logSW.WriteLine($"{timestamp}\t{loglevel}\t\t{message}");
            }
        }

        private static string promptPlayerName()
        {
            string input = "";
            input = RPS_Lib.sendPrompt("What is your name?");
            return input;
        }

        private static int displayMainMenu(bool newGame = true)
        {
            RPS_Lib.statMsg = ($"Current Round: { RPS_Lib.currentPlayer.TotalGames+1 } (W:{ RPS_Lib.currentPlayer.Wins } / L:{ RPS_Lib.currentPlayer.Losses } / D:{ RPS_Lib.currentPlayer.Draws })");
            string choiceIn = "";
            Console.Clear();

            if (newGame)
            {
                RPS_Lib.consoleSendLine(RPS_Lib.welcomeMsg);
                RPS_Lib.consoleSendLine(RPS_Lib.statMsg);
            }
            else
            {
                // Display W/L/D to player
                RPS_Lib.consoleSendLine($"You chose {(RPS)RPS_Lib.game.PlayerInput - 1}. The Computer chose {(RPS)RPS_Lib.game.PCInput - 1}. {RPS_Lib.game.getResultString()}");
            }

            List<string> lines = new List<string>();
            lines.Add("Rock <-> Paper <-> Scissors");
            if(newGame) {
                lines.Add("1. Play!");
            }
            else {
                lines.Add("1. Play again!");
            }
            lines.Add("2. View Player Statistics");
            lines.Add("3. View Leaderboard");
            lines.Add("4. Exit");
            
            RPS_Lib.showMenu(lines);

            choiceIn = RPS_Lib.sendPrompt("What would you like to do?");

            int choice;
            try {
                choice = Int32.Parse(choiceIn);
            }
            catch(Exception) {
                return 0;
            }
            return choice;
        }
    
        public static void displayPlayerStats() {
            Console.Clear();
            List<string> stringList = new List<string>();
            stringList.Add($"{ RPS_Lib.currentPlayer.Name }, here are your game play statistics…");
            stringList.Add($"Wins: { RPS_Lib.currentPlayer.Wins } ({ RPS_Lib.currentPlayer.WinRatio.ToString("F2") }%)");
            stringList.Add($"Losses:  { RPS_Lib.currentPlayer.Losses } ({ RPS_Lib.currentPlayer.LossRatio.ToString("F2") }%)");
            stringList.Add($"Draws: { RPS_Lib.currentPlayer.Draws } ({ RPS_Lib.currentPlayer.DrawRatio.ToString("F2") }%)");
            stringList.Add($"");
            if(RPS_Lib.currentPlayer.Losses != 0) {
                stringList.Add($"Win/Loss Ratio: { (RPS_Lib.currentPlayer.Wins/RPS_Lib.currentPlayer.Losses).ToString("F2") }");
            }
            else {
                stringList.Add($"Win/Loss Ratio: { RPS_Lib.currentPlayer.Wins }");
            }
            RPS_Lib.showMenu(stringList);

            RPS_Lib.sendPrompt("Press <Enter> to return to menu....");
        }
    
        public static void showLeaderboard() {
            Console.Clear();
            string delimiter = "----------------------";
            List<string> stringList = new List<string>();
            var top10Wins = (from p in RPS_Lib.players orderby p.Wins descending select p).Take(10);
            var top5GamesPlayed = (from p in RPS_Lib.players orderby p.TotalGames descending select p).Take(5);

            stringList.Add("Global Game Statistics");
            stringList.Add(delimiter);
            stringList.Add(delimiter);
            stringList.Add("Top 10 Winning Players:");
            stringList.Add(delimiter);
            foreach(var p in top10Wins) {
                stringList.Add($"- { p.Name }: { p.Wins }");
            }
            stringList.Add(" ");

            stringList.Add(delimiter);
            stringList.Add("Most Games Played:");
            stringList.Add(delimiter);
            foreach(var p in top5GamesPlayed) {
                stringList.Add($"- { p.Name }: { p.TotalGames }");
            }
            stringList.Add(" ");

            stringList.Add(delimiter);
            stringList.Add($"Win/Loss Ratio: { RPS_Lib.overallWLRatio() }");
            stringList.Add(delimiter);
            stringList.Add(" ");

            stringList.Add(delimiter);
            stringList.Add($"Total Games Played: { Game.totalGames }");
            stringList.Add(delimiter);
            stringList.Add(" ");
            RPS_Lib.showMenu(stringList);

            RPS_Lib.sendPrompt("Press <Enter> to return to menu....");
        }
    }
}