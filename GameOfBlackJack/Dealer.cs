using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfBlackJack
{
    internal class Dealer :Players
    {
        private string _playerType = "Dealer";//to pass into the method
        private int _dealerScore;
        List<string> CardsHeld = new List<string>();//up to 6 per game (unlikely but has to happen)
        public int CardValue { get; set; }
        public string SuitName { get; set; }
        
        public Dealer() : base() { }
        public Dealer(int cardValue,string suitName,string playerType) : base(cardValue,suitName,playerType) 
        { 
            this.CardValue = cardValue;
            if(CardValue  == 11)
            {
              _dealerScore += PulledAnAce(playerType);
            }
            else
            {
                _dealerScore += cardValue;
            }
        }
        public override int PulledAnAce(string userType)//automatically chooses which card to pull
        {
            int aceValue = 11;
            if(userType == "Dealer")
            {
                if (_dealerScore <= 10)
                {
                    return aceValue;
                }
                else
                {
                    aceValue = 1;
                }
                return aceValue;
            }
            return aceValue;
        }
    }
}
