using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Card
    {
        // C#
        public Suit Suit { get; private set; }
        public FaceValue FaceValue { get; private set; }

        public Card(Suit suit, FaceValue faceValue)
        {
            this.Suit = suit;
            this.FaceValue = faceValue;
        }

        public override string ToString()
        {
            string cardName;
            cardName = Enum.GetName(typeof(FaceValue), this.FaceValue) +
                " of " +
                Enum.GetName(typeof(Suit), this.Suit);

            return cardName;
        }

        public string ImageName
        {
            get
            {
                string imageName = this.ToString().Replace(' ', '_').ToLower();
                return imageName;
            }
        }

        public int PointValue
        {
            get
            {
                int points;

                if((int)this.FaceValue < 10)
                {
                    points = (int)this.FaceValue;
                }
                else
                {
                    points = 10;
                }
                return points;
            }
        }
    }
}
