using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace GameOfBlackJack
{
    internal class Program
    {
        public static int PlayerScore = 0;
        public static int DealerScore = 0;
        static void Main(string[] args)
        {
            bool mainLoop = true,stickOrTwistLoop = true;

            string stickOrTwist = "", playerCard ="" ;
            int i = 0, playerBets = 10; 
            do//mainLoop
            {

                while (stickOrTwistLoop)//stickOrTwist while loop (for some variety)
                {
                    for (;i < 2; i++)//the reason why its null- easier to change for a double output then a single if i set the variable below
                    {
                        Card pCard = new Card();
                        playerCard = pCard.ToString();
                        Console.WriteLine(ObtainingPlayerCardValues(playerCard, "Player"));
                        Console.WriteLine(DrawingCards(playerCard));

                        if (DealerScore <= 17)
                        {
                            Card dCard = new Card();
                            ObtainingPlayerCardValues(pCard.ToString(), "Dealer");
                        }
                    }

                    if(i == 2)
                    {
                        Console.WriteLine($"Your score is {PlayerScore}\n");
                        Console.WriteLine("Do you want to stick or twist - s/t?");
                        stickOrTwist = Console.ReadLine();
                    }
                    else if (i == 3)
                    {
                        Console.WriteLine("Do you want to stick or twist - s/t?");
                        stickOrTwist = Console.ReadLine();
                    }

                    if (stickOrTwist.ToLower() != "s" && stickOrTwist.ToLower() != "t")//|| (stickOrTwist.ToLower() != "stick" && stickOrTwist.ToLower() != "twist"))
                    {
                        Console.WriteLine("You didnt enter the right char/string");
                        i = 3; 

                    }else if (stickOrTwist.ToLower() == "s" || stickOrTwist.ToLower() == "stick")
                    {
                        Console.WriteLine("Dealer Plays \n");
                        i = 1;
                    }
                    else if (stickOrTwist.ToLower() == "t" || stickOrTwist.ToLower() == "twist")
                    {
                        stickOrTwistLoop = false ;
                        Console.WriteLine("Player Twists");
                    }
                }

                ReturnFinalPlay();

            } while (mainLoop == false);

        }
        public static string ObtainingPlayerCardValues(string cardString,string userType)
        {
            int cardValue = 0;
            string suitName = "",cardName;
            string[] valuesString = cardString.Split(',');
            string[] specialCards = { "King", "Queen", "Jack", "Ace" };

            cardName = valuesString[0];
            suitName = valuesString[1];

            if(userType == "Player")
            {
                if (!(int.TryParse(valuesString[0], out cardValue)))
                {
                    if (cardName == "Ace")
                    {
                        cardValue += PulledAnAce(11, userType);
                        PlayerScore += cardValue;
                    }
                    else
                    {
                        cardValue += 10;
                        PlayerScore += cardValue;
                    }
                }
                else if (int.TryParse(valuesString[0], out cardValue))
                {
                    PlayerScore += cardValue;
                }
                return $"You pulled a {cardName} of {suitName}, with a value of {cardValue}";
            }
            else
            {
                if (!(int.TryParse(valuesString[0], out cardValue)))
                {
                    if (cardName == "Ace")
                    {
                        cardValue += PulledAnAce(11,userType);
                        DealerScore += cardValue;
                    }
                    else
                    {
                        cardValue += 10;
                        DealerScore += cardValue;
                    }
                }
                else if (int.TryParse(valuesString[0], out cardValue))
                {
                    DealerScore += cardValue;
                }
                return $"The dealer pulled a {cardName} of {suitName}, with a value of {cardValue}";
            }
        }
        public static int PulledAnAce(int aceValue,string userType)
        {
            bool aceLoop = false;//main fault loop
            string number = "", inCaseOfLoop = "You pulled an Ace!!\nYou can either  c";
            string formattingNiceNess = "======================";
            int tempNumber, loopAgain = 0;

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

            }
            else//dealing with a dealer who will automatically make the better judgement calls on this, **sometimes.
            {
                if(DealerScore <= 10)
                {
                    return aceValue;
                }else
                {
                    return 1;
                }
            }

            return aceValue;
        }
        public static string DrawingCards(string playerCard)
        {
            string cardValue = "", suitName = ""
            , topAndBottomCard = "=================",
            edgeSection = "||";
            string[] valuesString = playerCard.Split(',');

            cardValue = valuesString[0];
            suitName = valuesString[1];
            for (int i = 0; i < 10; i++)
            {
                if(i == 0 || i == 10)
                {
                    Console.WriteLine(topAndBottomCard);
                }else if (i == 1 || i == 8)
                {
                    Console.WriteLine($"||{cardValue}            {cardValue}||");
                }else if(i == 2 || i == 9)
                {
                    Console.WriteLine($"||{suitName}{suitName}||");
                }
                else
                {
                    Console.WriteLine($"||            ||");
                }
            }

            return $"{playerCard}";
        }
        public static string ReturnFinalPlay()
        {
            if (DealerScore >= PlayerScore && DealerScore < 21)
            {
                Console.WriteLine($"Dealer Wins, with a value of {DealerScore}, compared to player score of {PlayerScore}");

            }
            else if (PlayerScore >= DealerScore && PlayerScore < 21)
            {
                Console.WriteLine($"Player Wins, with a value of {PlayerScore}, compared to dealer score of {DealerScore}");
            }
            else if (PlayerScore == DealerScore)
            {
                Console.WriteLine($"Its a tie with player scoring {PlayerScore} and Dealer scoring {DealerScore}");

            }
            else if (PlayerScore > 21)
            {
                Console.WriteLine($"Dealer Wins, as players score exceeded 21 ({PlayerScore})");
            }
            else if (DealerScore > 21)
            {
                Console.WriteLine($"Player Wins, as Dealer score exceeded 21 ({PlayerScore})");
            }
            return "shit aint working";
        }
    }
}
