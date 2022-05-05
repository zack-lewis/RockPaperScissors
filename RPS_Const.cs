using System;
using System.Collections.Generic;

namespace RockPaperScissors
{
    static class RPS_Const
    {
        public static string playerLogFile = "player_log.csv";
        public static string appLog = "RockPaperScissors.log";
        public static string currentPlayerName = "";
        public static List<Player> players = new List<Player>();
        public static Player currentPlayer;

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
            int borderWidth = 0;
                        
            foreach(string s in stringList) {
                borderWidth = Math.Max(borderWidth, s.Length + 6);
            }

            for(int i = 0; i < borderWidth; i++) {
                RPS_Const.consoleSendLine(border,false,false);
            }

            RPS_Const.consoleSendLine("",true,false);

            foreach(string s in stringList) {
                int sLen = s.Length + 4;
                RPS_Const.consoleSendLine($"| { s }",false,false);
                for(int i = 0; i < borderWidth - sLen; i++) {
                    RPS_Const.consoleSendLine(" ",false,false);
                }
                RPS_Const.consoleSendLine(" |",true,false);
            }
            
            for(int i = 0; i < borderWidth; i++) {
                RPS_Const.consoleSendLine(border,false,false);
            }
            RPS_Const.consoleSendLine("\n",false,false);

        }
    }

}