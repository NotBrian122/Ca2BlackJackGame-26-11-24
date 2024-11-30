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
        private int _cardName;
        private string _suitType;
        public string Playertype { get; set; }
        public int CardName //genereates value and name, we do post processing to assign values
        {
            get { return _cardName;}
            set { _cardName = value; }
        }
        public string SuitType //genereates suit type 
        {
            get{ return _suitType;}
            set { _suitType = value; }
        }
        public Card(int CardName, string suitName)
        {
            this.CardName = CardName;
            this.SuitType = suitName;
            //cards are auto generated, thought it would be easier to genereate them then to see if we can get something different 
            //nothing needs to be passed into this main method bar the outputs
        }
        public override string ToString()//return method
        {
            return $"{CardName}{SuitType}";
        }
    }
}

