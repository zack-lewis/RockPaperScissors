using System;
using System.Collections.Generic;

namespace RockPaperScissors
{
    static class RPS_Lib
    {
        public static string playerLogFile = "player_log.csv";
        public static string appLog = "RockPaperScissors.log";
        public static string currentPlayerName = "";
        public static List<Player> players = new List<Player>();
        public static Player currentPlayer;
        public static Game game; 
        public static string welcomeMsg;
        public static string statMsg;

        public static void consoleSendLine(string message, bool newLine = true, bool lineHeading = true) {
            if(lineHeading) {
                Console.Write("<<< ");
            }
            
            Console.Write(message);

            if(newLine) {
                Console.Write("\n");
            }
        }
    
        public static void showMenu(List<string> stringList) {
            const string border = "-";
            int borderWidth = 40;
                        
            foreach(string s in stringList) {
                borderWidth = Math.Max(borderWidth, s.Length + 6);
            }

            for(int i = 0; i < borderWidth; i++) {
                RPS_Lib.consoleSendLine(border,false,false);
            }

            RPS_Lib.consoleSendLine("",true,false);

            foreach(string s in stringList) {
                int sLen = s.Length + 4;
                
                RPS_Lib.consoleSendLine($"| { s }",false,false);
                for(int i = 0; i < borderWidth - sLen; i++) {
                    RPS_Lib.consoleSendLine(" ",false,false);
                }
                RPS_Lib.consoleSendLine(" |",true,false);
            }
            
            for(int i = 0; i < borderWidth; i++) {
                RPS_Lib.consoleSendLine(border,false,false);
            }
            RPS_Lib.consoleSendLine("\n",false,false);

        }
    
        public static string sendPrompt(string message)
        {
            RPS_Lib.consoleSendLine($"<<< {message} \n>>> ",false,false);
            return Console.ReadLine();
        }

        public static decimal overallWLRatio()
        {
            int totalWins = 0;
            int totalLosses = 0;
            foreach(var p in RPS_Lib.players) {
                totalWins += p.Wins;
                totalLosses += p.Losses;
            }
            return totalWins/totalLosses;
        }

        public static int overallTotalGames() {
            int total = 0;
            foreach(var p in RPS_Lib.players) {
                total += p.TotalGames;
            }
            return total;
        }
    }

}