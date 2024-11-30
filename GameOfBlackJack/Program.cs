using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace GameOfBlackJack
{
    internal class Program
    {
        //these are player and dealer game scores static to use across programmes
        public static int PlayerScore = 0;
        public static int DealerScore = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //this is to use aski and euro symbol

            //var region
            int playerMoney = 0;
            bool mainLoop = true;
            

            do//mainLoop
            {
                //Beginnign of each game we generate a new deck of cards.
                int suitInt = 0;
                List<Card> Deck = new List<Card>();
                // new Deck is generated in the form of a list (its easier to remove cards this way)

                //then we create a player and a dealer type inherinting player values
                LocalPlayer gamePlayer = new LocalPlayer();
                Dealer gameDealer = new Dealer();

                for (int i = 0; i < 4; i++)//every suit for every deck
                {
                    //counts each card number
                    for (int j = 1; j < 15; j++)
                    {
                        switch (suitInt)
                        {
                            case 0:
                                Card cardH = new Card(j, "Hearts");//so a suit then a value of 1 => 14
                                Deck.Add(cardH);
                                break;
                            case 1:
                                Card cardD = new Card(j, "Diamonds");
                                Deck.Add(cardD);
                                break;
                            case 2:
                                Card cardS = new Card(j, "Spades");
                                Deck.Add(cardS);
                                break;
                            case 3:
                                Card cardC = new Card(j, "Clubs");
                                Deck.Add(cardC);
                                break;
                        }
                    }
                    suitInt++;//just counts the deck 
                }

                //main menu
                Console.WriteLine("Welcome to BlackJack");
                string mainMenuChoice = MainMenu();

                //you get awarded €10 for your first play
                if(mainMenuChoice == "NG")
                {
                    Console.WriteLine($"\nFor your first game the house awards you {playerMoney += 10:c2}");


                    //dealer takes 4 cards from the deck and deals 2 to the player face up and one of theirs face up. 
                    Random generateCardRandom = new Random();
                    for (int i = 0; i < 4; i++)
                    {
                        int randomNumb = generateCardRandom.Next(1, 53);//generates a random number between 1 and 52

                        if (i % 2 == 0)//even is the house and odd is the player
                        {
                            gameDealer.CardValue = Deck[randomNumb].CardName;
                            gameDealer.SuitName = Deck[randomNumb].SuitType;
               

                        }else
                        {
                            gamePlayer.CardValue = Deck[randomNumb].CardName;
                            gamePlayer.SuitName = Deck[randomNumb].SuitType;
                            Console.WriteLine(gamePlayer.);
                        }    
                        Deck.RemoveAt(randomNumb);//removes the card used from the deck
                    }

                    //player takes 2 cards from deck, so does the dealer
                    Console.WriteLine("\nThe house deals 2 cards from the hand...");
                    Random dealingCards = new Random();
                    dealingCards.Next(0, 6);
                    
                    
                   
                 
                    //your presented with your total and asked to play again

                    //if you play again (stick) you take another card from the deck - so does the dealer. 

                    //compare scores or stick and twist again

                }
                else if (mainMenuChoice == "LOG")
                {

                }else if (mainMenuChoice == "EX")
                {
                    Console.WriteLine("\nGoodBye");
                    mainLoop = true; 
                }
            } while (mainLoop == false);
            //exit game
        }

        public static string MainMenu()
        {
            string menuChoiceString = "";
            bool mainMenuLoop = true;
            do
            {
                Console.Write("\n1.Play New Game\n2.Load old game\n3.Exit\n(Enter 1,2 or 3):");
                string mainMenuChoice = Console.ReadLine();

                switch (mainMenuChoice)//this is to "fool proof" this program
                {
                    case "1":
                        menuChoiceString = "NG";//new game
                        break;
                    case "2":
                        menuChoiceString = "LOG";//log old games
                        break;
                    case "3":
                        menuChoiceString = "EX";//exit 
                        break;
                    default://this defualt case is to check that if any of the other cases didnt work this one would repeat the loop and ask players again
                        mainMenuLoop = false;
                        if (!int.TryParse(menuChoiceString, out int tempScore))
                        {
                            Console.WriteLine("You didnt enter a intager");
                        }
                        else if (int.TryParse(menuChoiceString, out tempScore) && tempScore > 3)
                        {
                            Console.WriteLine("You enterd too large a number");
                        }
                        else if (int.TryParse(menuChoiceString, out tempScore) && tempScore < 1)

                            Console.WriteLine("The number you enterd was too small \nTry again:");
                        break;
                }
            } while (mainMenuLoop == false);

            return menuChoiceString;

        }
    }
}
