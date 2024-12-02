using System.Collections.Concurrent;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

namespace GameOfBlackJack
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //this is to use aski and euro symbol

            //var region
            int playerMoney = 0;
            bool mainLoop = true;
            

            do//mainLoop
            {
                #region start of game (generating objects)
                //Beginnign of each game we generate a new deck of cards.
                int suitInt = 0, amount = 0;
                List<Card> Deck = new List<Card>();

                string[] playerCards = new string[6];//the hand the player deals with
                string[] dealerPlayingCards = new string[6];//the hand the dealer plays with
                string[] theDealerHand = new string[6];//the hand the dealer "shuffels"

                bool cridentialsLoop = true, canDoubleDown = true,innerGameLoop = true;
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
                #endregion
                //main menu
                Console.WriteLine("Welcome to BlackJack");
                string mainMenuChoice = MainMenu();

                //you get awarded €10 for your first play
                if(mainMenuChoice == "NG")
                {
                    Console.WriteLine($"\nFor your first game the house awards you {playerMoney += 10:c2}");
                    Console.WriteLine("\n");

                    while (cridentialsLoop)//Entering a loop to check how much the palyer wants to enter 
                    {
                        Console.WriteLine("How much would you like to bet? (The house will bet the same against you)");
                        string playerBetAmount = Console.ReadLine();

                        if (!(int.TryParse(playerBetAmount, out amount))|| amount > playerMoney)
                        {
                            Console.WriteLine("You didnt enter the right credintenals");
                        }
                        else
                        {
                            Console.WriteLine($"You bet {amount:c2}");
                            playerMoney -= amount;
                            cridentialsLoop = false;
                            if(amount > playerMoney / 2)
                            {
                                canDoubleDown = false;
                            }
                        }
                    }
                    //dealer takes 6 cards from the deck and deals 2 to the player face up and one of theirs face up. 
                    Random generateCardRandom = new Random();

                    for (int i = 0; i < 6; i++)
                    {
                      
                        int randomNumb = generateCardRandom.Next(1, 53);//generates a random number between 1 and 52
                        if (Deck[randomNumb] == null)
                        {
                            generateCardRandom.Next(1, 53);//if the card they by chance is null then pull again
                        }

                        if (i % 2 == 0)
                        {
                            gamePlayer.CardValue = Deck[randomNumb].CardName;
                            gamePlayer.SuitName = Deck[randomNumb].SuitType;
                            gamePlayer.PullingCards((gamePlayer.CardValue), (gamePlayer.SuitName), (playerCards));//this pulling card function adds card values to an array 
                        }
                        else
                        {
                            gameDealer.CardValue = Deck[randomNumb].CardName;//so the dealer gets 3 random cards from the deck and stores it to the "hand" using the score of the first 2 
                            gameDealer.SuitName = Deck[randomNumb].SuitType;
                            gameDealer.PullingCards((gameDealer.CardValue), (gameDealer.SuitName), (theDealerHand));//adding them to an array called dealer hand 
                        }
                        
                        Deck.RemoveAt(randomNumb);//removes the card used from the deck
                    }

                    //player takes 2 cards from deck, so does the dealer
                    Console.WriteLine("\nThe house deals 2 cards from the hand...");

                    Console.WriteLine(playerCards[0]);//displaying card 1
                    Console.WriteLine(playerCards[1]);//displaying card 2
                    gamePlayer.GettingValuesForCards(playerCards[0]);//secretely adding card 1 to the playerScore
                    Console.WriteLine($"Your score is {gamePlayer.GettingValuesForCards(playerCards[1])}");//displaying card 2 

                    gameDealer.GettingValuesForCards(theDealerHand[0]);
                    gameDealer.GettingValuesForCards(theDealerHand[1]);//dealer adding their values together

                    do
                    {


                        Console.WriteLine($"Would you like to:\n1-Stand (set your cards to play)\n2-Hit (take another card)\n3-Double Down (put down double your bet - win twice the amount)\n4-Surrender(take half your bet and fortit the game)");
                        string playerChoice = Console.ReadLine();

                        if (playerChoice == "Stand" || playerChoice == "1")
                        {
                            gameWinner(gamePlayer._playerScore, gameDealer._dealerScore, amount);
                            innerGameLoop = false;
                        }
                        else if (playerChoice == "Hit" || playerChoice == "2")
                        {
                            Console.WriteLine($"You just pulled another card {gamePlayer.GettingValuesForCards(playerCards[2])}");

                        }
                        else if ((playerChoice == "Double Down" || playerChoice == "3") && canDoubleDown == true)
                        {
                            amount *= 2;
                            Console.WriteLine($"Your doubling down and betting {amount}");
                            gameWinner(gamePlayer._playerScore, gameDealer._dealerScore, amount);
                        }
                        else if (playerChoice == "Surrender" || playerChoice == "4" || gamePlayer._playerScore >= 21)
                        {
                            playerMoney -= (playerMoney / 2);
                            Console.WriteLine($"\nDealer wins with a score of :{gameDealer._dealerScore}\nAgainst a players score of :{gamePlayer._playerScore} ");
                            innerGameLoop = false;
                        }

                    }while (innerGameLoop);  

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
        #region main menu selector 
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
        #endregion
        public static void gameWinner(int playerPoints, int dealerPoints, int playerBet)
        {
            if((playerPoints > dealerPoints) && playerPoints <= 21) 
            {
                Console.WriteLine($"The player wins with a score of {playerPoints} to {dealerPoints}\nYou win {playerBet *= 2:c2}");
                
            }else if((dealerPoints > playerPoints) && dealerPoints <= 21)
            {
                Console.WriteLine($"The Dealer wins with a score of {dealerPoints} to {playerPoints}\nThey win {playerBet:c2}");
                Console.WriteLine($"Your account is now {}");

            }
            else if (playerPoints > 21)
            {
                Console.WriteLine($"The dealer wins");

            }else if (dealerPoints > 21)
            {
                Console.WriteLine($"The player wins, you get {playerBet *=2}");
            }
        }
    }
}
