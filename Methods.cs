using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RockPaperScissors {
    partial class Program {
        
        private static void updateList(List<Player> list, Player updatedPlayer)
        {
            Player remove = (from p in list where p.Name == updatedPlayer.Name select p).FirstOrDefault();
            if(remove != null) {
                list.Remove(remove);
            }
            list.Add(updatedPlayer);

        }

        private static void addTransation(string playerName, string gameString)
        {
            try {
                using(StreamWriter transactSW = new StreamWriter(RPS_Const.transactionLog,true)) {
                    transactSW.WriteLine($"{playerName},{gameString}");
                }
            }
            catch(Exception e) {
                Log("Error",$"Unable to write to transaction log: {e.Message}");
            }
        }

        private static void saveAll(List<Player> listIn)
        {
            try {
                using(StreamWriter playerDataSW = new StreamWriter(RPS_Const.playerLogFile,false)) {
                    foreach(Player p in listIn) {
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
            input = sendPrompt("Input player's name:");
            return input;
        }

        private static string sendPrompt(string message)
        {
            consoleSendLine($"<<< {message} \n>>> ",false,false);
            return Console.ReadLine();
        }

        private static int displayMainMenu()
        {
            string choiceIn = "";
            const string border = "-";
            int borderWidth = 0;

            List<string> lines = new List<string>();
            lines.Add("Rock <-> Paper <-> Scissors");
            lines.Add("1. Play!");
            lines.Add("2. View Global Statistics");
            lines.Add("3. Exit");
            
            foreach(string s in lines) {
                borderWidth = Math.Max(borderWidth, s.Length + 6);
            }

            for(int i = 0; i < borderWidth; i++) {
                consoleSendLine(border,false,false);
            }

            consoleSendLine("",true,false);

            foreach(string s in lines) {
                int sLen = s.Length + 4;
                consoleSendLine($"| { s }",false,false);
                for(int i = 0; i < borderWidth - sLen; i++) {
                    consoleSendLine(" ",false,false);
                }
                consoleSendLine(" |",true,false);
            }
            
            for(int i = 0; i < borderWidth; i++) {
                consoleSendLine(border,false,false);
            }
            consoleSendLine("\n",false,false);

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

        public static void consoleSendLine(string message, bool newLine = true, bool lineHeading = true) {
            if(lineHeading) {
                Console.Write("<<< ");
            }
            
            Console.Write(message);

            if(newLine) {
                Console.Write("\n");
            }
        }
    }
}