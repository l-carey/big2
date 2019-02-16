using System;
using System.Collections.Generic;
using System.Linq;

namespace Big2.Sim
{
    public class Hand
    {
        public Hand(IEnumerable<Card> cards)
        {
            Cards = cards;
        }

        public IEnumerable<Card> Cards { get; }
        
        public override string ToString()
        {
            return string.Join(",", Cards.OrderBy(x => x.Value));
        }

        public bool HasA(Play play)
        {
            switch (play)
            {
                case Play.Pair:
                    return HasPair();

                case Play.TwoPair:
                    return HasTwoPair();

                case Play.Flush:
                    return HasFlush();

                case Play.Straight:
                    return HasStraight();

                case Play.Triple:
                    return HasTriple();

                case Play.FullHouse:
                    return HasFullHouse();

                case Play.Quadruple:
                    return HasQuadruple();

                case Play.StraightFlush:
                    return HasStraightFlush();

                case Play.RoyalFlush:
                    return HasRoyalFlush();

                default:
                    return false;
            }
        }
        
        private bool HasNOfAKind(int n)
        {
            if (n < 1)
            {
                throw new ArgumentException("n gotta be more than one FFS");
            }

            if (n > 4)
            {
                throw new ArgumentException("oh FFS");
            }

            return Cards
                .GroupBy(x => x.Value)
                .Any(g => g.Count() >= n);
        }

        private bool HasPair()
        {
            return Cards
                .GroupBy(x => x.Value)
                .Any(g => g.Count() > 1);
        }

        private bool HasTwoPair()
        {
            return Cards
                .GroupBy(x => x.Value)
                .Where(g => g.Count() > 1)
                .Count() > 2;
        }

        private bool HasTriple()
        {
            return HasNOfAKind(3);
        }

        private bool HasQuadruple()
        {
            return HasNOfAKind(4);
        }

        private bool HasFlush()
        {
            return Cards
                .GroupBy(x => x.Suit)
                .Any(g => g.Count() >= 5);
        }

        private bool HasFullHouse()
        {
            if (!HasTriple())
            {
                return false;
            }

            var tripleValue = Cards
                .GroupBy(x => x.Value)
                .FirstOrDefault(g => g.Count() >= 3)
                .First()
                .Value;
            
            return (new Hand(Cards.Where(x => x.Value != tripleValue))).HasPair();
        }

        private bool HasStraight()
        {
            var values = Cards
                .Where(x => x.Value != 13)
                .Select(y => y.Value)
                .Distinct()
                .OrderByDescending(z => z)
                .ToList();

            if (values.Count < 5)
            {
                return false;
            }

            for (int i = 0; i <= values.Count - 5; i++)
            {
                if (values[i] - 4 == values[i + 4])
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasStraightFlush()
        {
            if (!(HasFlush() || HasStraight())) // no flush or straight so def no straight flush
            {
                return false;
            }

            var has = Cards
                .GroupBy(x => x.Suit)
                .Where(g => g.Count() >= 5)
                .Any(a => (new Hand(Cards.Where(c => c.Suit == a.Key))).HasStraight());

            return has;
        }

        private bool HasRoyalFlush()
        {
            return false; // todo
        }
    }
}
