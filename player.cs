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
 
        public decimal WinRatio { get => _wins / (TotalGames); }
        public decimal LossRatio { get => _losses / (TotalGames); }
        public int TotalGames { get => _wins + _losses + _draws; }

        public Player (string name, int wins, int losses, int draws) {
            this._name = name;
            this._draws = draws;
            this._losses = losses;
            this._wins = wins;
        }

        public Player(string name) : this(name,0,0,0) {}

        public Player() : this("anonymous") {}
    }
}