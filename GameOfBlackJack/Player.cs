using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfBlackJack
{
    internal class Players : Card
    {
        private string _playerType = "";
        public int CardValue { get; set; }
        public string SuitName {  get; set; }

        
        public Players(int cardValue,string suitName,string playerType) : base(cardValue, suitName)
        {
            this.CardValue = cardValue;
            this.SuitName = suitName;
            this._playerType = playerType;

            if (cardValue == 11)
            {
                CardValue = PulledAnAce(playerType);
            }
        }
        public Players() : base(0, "")//null refrence
        { }
        public virtual int PulledAnAce(string userType)//reason for virtual is so that we can have scores added up and see whos got what
        {
                bool aceLoop = false;//main fault loop
                string number = "", inCaseOfLoop = "You pulled an Ace!!\nYou can either  c";
                string formattingNiceNess = "======================";
                int tempNumber, loopAgain = 0, aceValue = 11;//we presume its 11 unless its 1

                if (userType == "Player")
                {
                    do
                    {
                        if (loopAgain >= 1)
                            inCaseOfLoop = "\nC";

                        Console.WriteLine($"{formattingNiceNess}\n{inCaseOfLoop}hoose 11 OR 1\n{formattingNiceNess}");
                        number = Console.ReadLine();

                        if ((int.TryParse(number, out tempNumber) && tempNumber == 1) || (number.ToUpper() == "ONE"))
                        {
                            aceValue = 1;
                            aceLoop = true;
                        }
                        else if ((int.TryParse(number, out tempNumber) && tempNumber == 11) || (number.ToUpper() == "ELEVEN"))//a tryparse to check if it has passed and is 11
                        {
                            aceLoop = true;
                        }
                        else
                        {
                            Console.WriteLine("You didnt enter the right intager, try again");
                            loopAgain++;
                        }
                    } while (aceLoop == false);

                }//dealing with a dealer who will automatically make the better judgement calls on this, **sometimes.
                return aceValue;
        }
        public void PullingCards(int cardValue, string suitName, List<string> cardsHeld )
        {
            string cardNickName = "";
            if(cardValue > 10)
            {
                switch (cardValue)
                {
                    case 11:
                        cardNickName = "Ace";
                        break;
                    case 12:
                        cardNickName = "King";
                        break;
                    case 13:
                        cardNickName = "Queen";
                        break;
                    case 14:
                        cardNickName = "Jack";
                        break;
                }

                for (int i = 0; i < 6; i++)
                {
                    if (cardsHeld[i] == "")
                    {
                        cardsHeld[i] = $"{cardNickName} of {suitName}, value:{cardValue}";
                    }
                }
            }
            else
            {

                for (int i = 0; i < 6; i++)
                {
                    if (cardsHeld[i] == "")
                    {
                        cardsHeld[i] = $"{CardValue} of {suitName}, value:{cardValue}";
                    }
                }
            }

        }
    }
}
