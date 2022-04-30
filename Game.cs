using System;
using System.Text;

namespace RockPaperScissors
{
    internal class Game
    {
        private static int gameID = 0;
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
        
        public Game()
        {
            gameID++;
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

        public void displayResult()
        {
            switch(Result) {
                case -1:
                    Console.WriteLine("Oooh sorry, you lost!");
                    break;
                case 0:
                    Console.WriteLine("Well, it wasn't a loss, but it wasn't a win. Draw!");
                    break;  
                case 1:
                    Console.WriteLine("Congrats! You Won!");
                    break;
                default:
                    break;
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
}