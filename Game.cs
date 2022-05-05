using System;
using System.Text;

namespace RockPaperScissors
{
    internal class Game
    {
        private int gameID = 0;
        private int _playerInput;
        public int PlayerInput
        {
            get { return _playerInput; }
            set { _playerInput = value; }
        }

        private int _pcInput;
        public int PCInput
        {
            get { return _pcInput; }
        }
        
        public int Result {
            get { 
                if(PlayerInput == PCInput) return 0;
                if(PlayerInput == 1 && PCInput == 3) return 1; // Rock -> Scissors
                if(PlayerInput == 3 && PCInput == 2) return 1; // Scissors -> Paper
                if(PlayerInput == 2 && PCInput == 1) return 1; // Paper -> Rock
                if(PlayerInput == 3 && PCInput == 1) return -1; // Rock -> Scissors
                if(PlayerInput == 2 && PCInput == 3) return -1; // Scissors -> Paper
                if(PlayerInput == 1 && PCInput == 2) return -1; // Paper -> Rock
                return -2;
            }
        }
        
        public Game(int id) {
            this.gameID = id;
            Random rdm = new Random();
            _pcInput = (rdm.Next(3)+1);
            Console.WriteLine("Welcome to Rock <> Paper <> Scissors!");
        }

        public void getUserPlay()
        {
            while(true) {
                Console.WriteLine("What is your move? ");
                Console.WriteLine("1. ROCK. cuz rock crush metal");
                Console.WriteLine("2. PAPER. money is paper and money is power.");
                Console.WriteLine("3. SCISSORS. to cut the crap out of paper or any attackers");
                Console.Write("\n--> ");
                string input = Console.ReadLine();
                switch(input.ToUpper()) {
                    case "1":
                    case "ROCK":
                    case "R":
                        _playerInput = 1;
                        break;
                    case "2":
                    case "PAPER":
                    case "P":
                        _playerInput = 2;
                        break;
                    case "3":
                    case "SCISSORS":
                    case "S":
                        _playerInput = 3;
                        break;
                    default:
                        continue;
                }
                break; // break out of loop, only if not continued in default
            }
        }

        public string getResultString() {
            switch(Result) {
                case -1:
                    return ("You lost!");
                case 0:
                    return ("Draw!");
                case 1:
                    return ("You Won!");
                default:
                    return ("Error in result");
            }  
        }
    
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{gameID},");
            sb.Append($"{this.PlayerInput},");
            sb.Append($"{this.PCInput},");
            sb.Append($"{this.Result}");
            return sb.ToString();
        }
    }

    enum RPS {
        Rock, Paper, Scissors
    }
}