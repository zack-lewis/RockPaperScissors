using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RockPaperScissors
{
    class Program
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
                Log("Info",$"Player { name } was not found and has been created as a new player");
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
                        // Get User Option
                        // Get Random option for PC
                        // Compare to see who won
                        // Add W/L/D to playerStat
                        // Write playerStat to file
                        // Display W/L/D to player
                        break;
                    // View Stats
                    case 2: 
                        // Run static queries
                        // Display output
                        break;
                    // Exit
                    case 3:
                        return;
                    default: 
                        break;
                }
                // Loop
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
                        catch(Exception ex) {
                            Log("Warning", $"Exception importing player data on line { lineCount }. Skipping line: \n\t { data } ");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace);
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
            string timestamp = $"{ DateTime.Now.Date.ToString() } { DateTime.Now.TimeOfDay.ToString() }";
            Console.WriteLine(message);
            //throw new NotImplementedException();
        }

        private static string promptPlayerName()
        {
            string input = "";
            Console.WriteLine("Input player's name:");
            input = Console.ReadLine();
            return input;
        }

        private static int displayMainMenu()
        {
            string choiceIn = "";
            const string border = "-";
            int borderWidth = 0;

            List<string> lines = new List<string>();
            lines.Add("Rock <-> Paper <-> Scissors");
            lines.Add("What would you like to do?");
            lines.Add("1. Play!");
            lines.Add("2. View Global Statistics");
            lines.Add("3. Exit");
            
            foreach(string s in lines) {
                borderWidth = Math.Max(borderWidth, s.Length + 6);
            }

            for(int i = 0; i < borderWidth; i++) {
                Console.Write(border);
            }

            Console.WriteLine();

            foreach(string s in lines) {
                int sLen = s.Length + 4;
                Console.Write($"| { s }");
                for(int i = 0; i < borderWidth - sLen; i++) {
                    Console.Write(" ");
                }
                Console.WriteLine(" |");
            }
            
            for(int i = 0; i < borderWidth; i++) {
                Console.Write(border);
            }
            Console.WriteLine("\n");

            Console.Write("--> ");
            choiceIn = Console.ReadLine();

            int choice;
            try {
                choice = Int32.Parse(choiceIn);
            }
            catch(Exception) {
                return 0;
            }
            return choice;
            //throw new NotImplementedException();
        }
    }
}
