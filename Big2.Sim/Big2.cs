using System;
using System.Collections.Generic;

namespace Big2.Sim
{
    public class Big2
    {
        private readonly List<Play> _plays = new List<Play>
        {
            Play.Pair, Play.TwoPair, Play.Flush, Play.Straight, Play.Triple, Play.FullHouse, Play.Quadruple, Play.StraightFlush
        };
        private readonly Dictionary<Play, int> _results = new Dictionary<Play, int>();

        public Big2()
        {
            Init();
        }

        public void Run()
        {
            Run(101);
        }

        public void Run(int n)
        {
            if (n < 1)
            {
                throw new ArgumentException("n gotta be more than one FFS");
            }

            var deck = new Deck();
            
            Console.WriteLine("-- inizio --");

            for (int i = 0; i < n; i++)
            {
                var hand = deck.Deal(13);

                //PrintDebug(hand, i);

                foreach (var play in _plays)
                {
                    if (hand.HasA(play))
                    {
                        _results[play] += 1;
                    }
                }

                deck.Shuffle();
            }

            PrintResults(n);

            Console.WriteLine("-- fin --");
        }

        private void PrintResults(int n)
        {
            Console.WriteLine($">>> total hands = {n}");

            foreach (var play in _plays)
            {
                Console.WriteLine("   ----   ");
                Console.WriteLine($" no. with {play.ToString()} : {_results[play]}");
                var p = (double)_results[play] / n;
                Console.WriteLine($" => {Math.Round(p, 8)}");
            }
        }

        private void Init()
        {
            foreach (var play in _plays)
            {
                _results[play] = 0;
            }
        }

        private void PrintDebug(Hand hand, int i)
        {
            Console.WriteLine($">>> Hand # {i.ToString()}");
            Console.WriteLine("   ----   ");
            Console.WriteLine(hand.ToString());
            Console.WriteLine("   ----   ");
            Console.WriteLine($" has a pair? {(hand.HasA(Play.Pair) ? "Y" : "N")}");
            Console.WriteLine($" has two pair? {(hand.HasA(Play.TwoPair) ? "Y" : "N")}");
            Console.WriteLine($" has a flush? {(hand.HasA(Play.Flush) ? "Y" : "N")}");
            Console.WriteLine($" has a triple? {(hand.HasA(Play.Triple) ? "Y" : "N")}");
            Console.WriteLine($" has a quadruple? {(hand.HasA(Play.Quadruple) ? "Y" : "N")}");
            Console.WriteLine($" has a full house? {(hand.HasA(Play.FullHouse) ? "Y" : "N")}");
            Console.WriteLine($" has a straight? {(hand.HasA(Play.Straight) ? "Y" : "N")}");
            Console.WriteLine($" has a straight flush? {(hand.HasA(Play.StraightFlush) ? "Y" : "N")}");
            Console.WriteLine("   ====   ");
        }
    }
}
