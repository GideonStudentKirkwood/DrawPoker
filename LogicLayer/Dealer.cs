using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public class Dealer
    {
        private Deck _deck = new Deck();

        public Hand NewHand() // return a fresh hand of cards
        {
            _deck.Shuffle();

            Hand newHand = new Hand(
                                    _deck.NextCard(),
                                    _deck.NextCard(),
                                    _deck.NextCard(),
                                    _deck.NextCard(),
                                    _deck.NextCard()
                                    );
            return newHand;
        }

        public void DrawCards(Hand hand)
        {
            for (int i = 0; i < hand.Cards.Length; i++)
            {
                if(hand.Keep[i] == false)
                {
                    hand.Cards[i] = _deck.NextCard();
                }
            }
        }
    }
}
