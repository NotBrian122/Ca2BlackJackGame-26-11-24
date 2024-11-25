using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfBlackJack
{
    internal class Player : Card
    {
        private static int _playerScore;

        public int PlayerScore 
        {
            get {return _playerScore; }
            set {_playerScore = value; } 
        }
        public Player()
        {
            
        }
       
    }
}
