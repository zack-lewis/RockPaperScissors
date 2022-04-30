using System;
using System.Text;

namespace RockPaperScissors
{
    class Player
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _wins;
        public int Wins
        {
            get { return _wins; }
            set { _wins = value; }
        }

        private int _losses;
        public int Losses
        {
            get { return _losses; }
            set { _losses = value; }
        }
                
        private int _draws;
        public int Draws
        {
            get { return _draws; }
            set { _draws = value; }
        }
 
        public decimal WinRatio { 
            get { 
                try {
                    decimal output = (((decimal)_wins / (decimal)TotalGames)*100);
                    return output;
                }
                catch(DivideByZeroException) {
                    return 0;
                }
            } 
        }

        public decimal LossRatio { 
            get {
                try {
                    decimal output = (((decimal)_losses / (decimal)TotalGames)*100); 
                    return output;
                }
                catch(DivideByZeroException) {
                    return 0;
                }
            } 
        }

        public decimal DrawRatio { 
            get {
                try {
                    decimal output = (((decimal)_draws / (decimal)TotalGames)*100); 
                    return output;
                }
                catch(DivideByZeroException) {
                    return 0;
                }
            } 
        }
        public int TotalGames { get => _wins + _losses + _draws; }

        public Player (string name, int wins, int losses, int draws) {
            this._name = name;
            this._draws = draws;
            this._losses = losses;
            this._wins = wins;
        }

        public Player(string name) : this(name,0,0,0) {}

        public Player() : this("anonymous") {}

        public void addGame(int gameResult) {
            if(gameResult > 0) {
                this._wins++;
            }
            else if (gameResult < 0) {
                this._losses++;
            }
            else {
                this._draws++;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{this.Name},");
            sb.Append($"{this.Wins},");
            sb.Append($"{this.Losses},");
            sb.Append($"{this.Draws}");
            return sb.ToString();
        }
    }
}