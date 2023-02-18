using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Deck
    {
        private List<Card> _cards = new List<Card>();
        private int _topCard = -1;  // this variable holds the current position
        private Random _rand = new Random();

        public Deck()
        {
            // populate a deck of 52 cards
            for (int suit = 0; suit < 4; suit++) // loop through suits
            {
                for (int faceValue = 1; faceValue <= 13; faceValue++)
                {
                    _cards.Add(new Card((Suit)suit, (FaceValue)faceValue));
                }
            }
        }

        public Card NextCard()
        {
            _topCard++;

            if(_topCard >= _cards.Count) // to keep from exceeding array
            {
                _topCard = 0;
            }

            return _cards[_topCard];
        }

        public void Shuffle()
        {
            // run through the deck a number of times
            // swapping each card with a randomly chosen card

            for (int traversals = 0; traversals < 25; traversals++) // number of traversals
            {
                for (int currentCard = 0; currentCard < _cards.Count; currentCard++) // card
                {
                    // simple swap logic
                    int cardToSwapWith = _rand.Next(0, _cards.Count);
                    Card temp = _cards[currentCard]; // save the current card
                    _cards[currentCard] = _cards[cardToSwapWith];
                    _cards[cardToSwapWith] = temp;
                }
            }
            _topCard = -1;
        }
    }
}
