using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Hand
    {
        public Card[] Cards { get; private set; }
        public bool[] Keep { get; set; }
        public Hand(Card a, Card b, Card c, Card d, Card e)
        {
            Cards = new Card[] { a, b, c, d, e };
            Keep = new bool[5];

            for (int i = 0; i < Keep.Length; i++)
            {
                Keep[i] = false;
            }
        }

        // the place to begin is by sorting the cards in the hand
        public void sortHand()
        {
            for (int outerLoop = Cards.Length; outerLoop > 0; outerLoop--)
            {
                for (int innerLoop = 0; innerLoop < outerLoop - 1; innerLoop++)
                {
                    if(Cards[innerLoop].FaceValue > Cards[innerLoop + 1].FaceValue)
                    {
                        // swap the cards
                        Card temp = Cards[innerLoop];
                        Cards[innerLoop] = Cards[innerLoop + 1];
                        Cards[innerLoop + 1] = temp;
                    }
                }
            }
        }

        // we need logic to score the value of a poker hand
        public HandValue ScoreHand()
        {
            HandValue result = HandValue.Nothing;
            bool isFlush = false;
            bool isStraight = false;
            bool isAceHighStraight = false;
            sortHand();

            // is it a flush?
            if (Cards[0].Suit == Cards[1].Suit &&
                Cards[0].Suit == Cards[2].Suit &&
                Cards[0].Suit == Cards[3].Suit &&
                Cards[0].Suit == Cards[4].Suit)
            {
                isFlush = true;
            }
            // is it a straight?
            if(Cards[0].FaceValue == Cards[1].FaceValue - 1 &&
               Cards[1].FaceValue == Cards[2].FaceValue - 1 &&
               Cards[2].FaceValue == Cards[3].FaceValue - 1 &&
               Cards[3].FaceValue == Cards[4].FaceValue - 1)
            {
                isStraight = true;
            }
            // is it an Ace-High straight?
            if(Cards[0].FaceValue == FaceValue.Ace &&
               Cards[1].FaceValue == FaceValue.Ten &&
               Cards[2].FaceValue == FaceValue.Jack &&
               Cards[3].FaceValue == FaceValue.Queen &&
               Cards[4].FaceValue == FaceValue.King)
            {
                isAceHighStraight = true;
            }

            if(isFlush || isStraight || isAceHighStraight)
            {   // straights and flushes
                if(isFlush && isAceHighStraight)
                {
                    result = HandValue.Royal_Flush;
                }
                else if(isFlush && isStraight)
                {
                    result = HandValue.Straight_Flush;
                }
                else if(isFlush)
                {
                    result = HandValue.Flush;
                }
                else // isStraight
                {
                    result = HandValue.Straight;
                }
            }
            else
            {   // pair-based hands
                
                // four of a kind
                if (Cards[0].FaceValue == Cards[3].FaceValue ||
                   Cards[1].FaceValue == Cards[4].FaceValue)
                {
                    result = HandValue.Four_of_a_Kind;
                }
                // full house
                else if( (Cards[0].FaceValue == Cards[2].FaceValue &&
                          Cards[3].FaceValue == Cards[4].FaceValue) 
                          ||
                         (Cards[0].FaceValue == Cards[1].FaceValue &&
                          Cards[2].FaceValue == Cards[4].FaceValue))
                {
                    result = HandValue.Full_House;
                }

                // three of a kind
                  else if (Cards[0].FaceValue == Cards[2].FaceValue 
                           ||
                           Cards[1].FaceValue == Cards[3].FaceValue 
                           ||
                           Cards[2].FaceValue == Cards[4].FaceValue)
                {
                    result = HandValue.Three_of_a_Kind;
                }
                // two pair
                else if ((Cards[0].FaceValue == Cards[1].FaceValue &&
                          Cards[2].FaceValue == Cards[3].FaceValue) 
                          ||
                         (Cards[0].FaceValue == Cards[1].FaceValue &&
                          Cards[3].FaceValue == Cards[4].FaceValue) 
                          ||
                          (Cards[1].FaceValue == Cards[2].FaceValue &&
                          Cards[3].FaceValue == Cards[4].FaceValue))
                {
                    result = HandValue.Two_Pair;
                }
                else if (Cards[0].FaceValue == Cards[1].FaceValue 
                         ||
                         Cards[1].FaceValue == Cards[2].FaceValue 
                         ||
                         Cards[2].FaceValue == Cards[3].FaceValue 
                         ||
                         Cards[3].FaceValue == Cards[4].FaceValue)
                {
                    result = HandValue.Pair;
                }
            }
            return result;
        }

    }
}
