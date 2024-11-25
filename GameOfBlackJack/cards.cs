using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GameOfBlackJack
{


    internal class Card 
    {
        private string _cardName;
        private string _suitType;
        private static int _suitValue;

        public string CardName 
        {
            get 
            {  
                Random rnd = new Random();
                int cardValue = rnd.Next(1,15);

                if(cardValue >= 11)
                {
                    switch(cardValue)
                    {
                        case 11:
                            _cardName = "Ace";
                            break;
                        case 12:
                            _cardName = "king";
                        break;
                        case 13:
                            _cardName = "Jack";
                            break;
                        case 14:
                            _cardName = "Queen";
                            break;
                    }
                    return _cardName;
                }else
                {
                    return _cardName = cardValue.ToString();
                }
               
            }
            set {  _cardName = value; }
        }
        public string SuitType 
        {
            get 
            { 
                Random suit = new Random();
                int suitValue = suit.Next(1, 5);

                switch (suitValue)
                {
                    case 1:
                        _suitType = "Clubs";
                        break;
                    case 2:
                        _suitType = "Spades";
                        break;
                    case 3:
                        _suitType = "Hearts";
                        break;
                    case 4:
                        _suitType = "Diamonds";
                        break;
                }
                return _suitType;
            }
            set { _suitType = value; }
        }
        public Card()
        {
            //cards are auto generated, thought it would be easier to genereate them then to see if we can get something different 
            //nothing needs to be passed into this main method bar the outputs

        }
        public override string ToString()
        {
            return $"{CardName},{SuitType}";
        }
    } 
}
