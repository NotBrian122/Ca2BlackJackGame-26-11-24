using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfBlackJack
{
    internal class LocalPlayer :Players
    {
        private string _playerType = "Player";
        private int _playerScore;
        public int CardValue { get; set; }
        public string SuitName { get; set; }
        public LocalPlayer() : base() { }
        public LocalPlayer(int cardValue,string suitName,string playerType)
        {
            this.CardValue = cardValue;
            if(CardValue == 11)
            {
                _playerScore += PulledAnAce(_playerType);
            }
            else
            {
                _playerScore += cardValue;
            }
        }
    }
}
