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
                RPS_Const.consoleSendLine("Welcome to Rock, Paper, Scissors!");
                RPS_Const.consoleSendLine("1. New Player");
                RPS_Const.consoleSendLine("2. Load Player");
                RPS_Const.consoleSendLine("3. Quit");
                string initialMenuPrompt = sendPrompt("Enter Choice:");
                
                try {
                    int newOrLoad = Int32.Parse(initialMenuPrompt);
                    Player existingPlayer = null;
                    if(newOrLoad == 1 || newOrLoad == 2) {
                        RPS_Const.currentPlayerName = promptPlayerName();
                        existingPlayer = (from p in RPS_Const.players where p.Name == RPS_Const.currentPlayerName select p).FirstOrDefault();
                    }
                    switch(newOrLoad) {
                        case 1:
                            if (existingPlayer == null) {
                                RPS_Const.consoleSendLine($"Hello { RPS_Const.currentPlayerName }. Let's play!\n");
                                return 0;
                            }
                            else {
                                RPS_Const.consoleSendLine($"Sorry { RPS_Const.currentPlayerName }, your game already exists.\n");
                                Console.Beep();
                                continue;
                            }
                        case 2:
                            // Get Name
                            if (existingPlayer != null) {
                                RPS_Const.consoleSendLine($"Current Round: { existingPlayer.TotalGames+1 } W:{ existingPlayer.Wins } / L:{ existingPlayer.Losses } / D:{ existingPlayer.Draws }");
                                RPS_Const.consoleSendLine($"Welcome back { RPS_Const.currentPlayerName }. Let's play!\n");
                                return 1;  
                            }
                            else {
                                RPS_Const.consoleSendLine($"Sorry { RPS_Const.currentPlayerName }, your game could not be found.\n");
                                Console.Beep();
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
                    RPS_Const.consoleSendLine("I don't think that's an option\n");
                    continue;
                }
            }
        }
        
        private static void updateList()
        {
            Player remove = (from p in RPS_Const.players where p.Name == RPS_Const.currentPlayer.Name select p).FirstOrDefault();
            if(remove != null) {
                RPS_Const.players.Remove(remove);
            }
            RPS_Const.players.Add(RPS_Const.currentPlayer);

        }

        private static void saveAll()
        {
            try {
                using(StreamWriter playerDataSW = new StreamWriter(RPS_Const.playerLogFile,false)) {
                    foreach(Player p in RPS_Const.players) {
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
                using(StreamReader sr = new StreamReader(RPS_Const.playerLogFile)) {
                    string data = "";
                    int lineCount = 0;
                    while((data = sr.ReadLine()) != null){
                        string[] line = data.Split(",");
                        try {
                            Player inP = new Player(line[0].ToString(),Int32.Parse(line[1].ToString()),Int32.Parse(line[2].ToString()),Int32.Parse(line[3].ToString()));
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
            using(StreamWriter logSW = new StreamWriter(RPS_Const.appLog,true)) {
                logSW.WriteLine($"{timestamp}\t{loglevel}\t\t{message}");
            }
        }

        private static string promptPlayerName()
        {
            string input = "";
            input = sendPrompt("What is your name?");
            return input;
        }

        private static string sendPrompt(string message)
        {
            RPS_Const.consoleSendLine($"<<< {message} \n>>> ",false,false);
            return Console.ReadLine();
        }

        private static int displayMainMenu(bool newGame = true)
        {
            string choiceIn = "";

            List<string> lines = new List<string>();
            lines.Add("Rock <-> Paper <-> Scissors");
            lines.Add($"Current Round: { RPS_Const.currentPlayer.TotalGames + 1 }");
            if(newGame) {
                lines.Add("1. Play!");
            }
            else {
                lines.Add("1. Play again!");
            }
            lines.Add("2. View Player Statistics");
            lines.Add("3. View Leaderboard");
            lines.Add("4. Exit");
            
            RPS_Const.showMenu(lines);
            // foreach(string s in lines) {
            //     borderWidth = Math.Max(borderWidth, s.Length + 6);
            // }

            // for(int i = 0; i < borderWidth; i++) {
            //     RPS_Const.consoleSendLine(border,false,false);
            // }

            // RPS_Const.consoleSendLine("",true,false);

            // foreach(string s in lines) {
            //     int sLen = s.Length + 4;
            //     RPS_Const.consoleSendLine($"| { s }",false,false);
            //     for(int i = 0; i < borderWidth - sLen; i++) {
            //         RPS_Const.consoleSendLine(" ",false,false);
            //     }
            //     RPS_Const.consoleSendLine(" |",true,false);
            // }
            
            // for(int i = 0; i < borderWidth; i++) {
            //     RPS_Const.consoleSendLine(border,false,false);
            // }
            // RPS_Const.consoleSendLine("\n",false,false);

            choiceIn = sendPrompt("What would you like to do?");

            int choice;
            try {
                choice = Int32.Parse(choiceIn);
            }
            catch(Exception) {
                return 0;
            }
            return choice;
        }

        // public static void consoleSendLine(string message, bool newLine = true, bool lineHeading = true) {
        //     if(lineHeading) {
        //         Console.Write("<<< ");
        //     }
            
        //     Console.Write(message);

        //     if(newLine) {
        //         Console.Write("\n");
        //     }
        // }
    
        public static void displayPlayerStats() {

        }
    }
}