using System;
using System.Collections.Generic;

namespace Big2.Sim
{
    /*
     * Spades, Hearts, Clubs, Diamonds
     */
    public class Deck
    {
        private List<Card> _cards = new List<Card>();
        private Random _rand;

        public static Dictionary<int, string> Values = new Dictionary<int, string>
        {
            { 1, "3" }, { 2, "4" }, { 3, "5" }, { 4, "6" }, { 5, "7" }, { 6, "8" }, { 7, "9" }, 
            { 8, "10" }, { 9, "J" }, { 10, "Q" }, { 11, "K" }, { 12, "A" }, { 13, "2" }
        };

        public Deck()
        {
            _rand = new Random(DateTime.Now.Millisecond);
            Init();
        }

        public Deck(int rngSeed)
        {
            _rand = new Random(rngSeed);
            Init();
        }

        public Hand Deal(int numberCOfCards)
        {
            var cards = new List<Card>();

            if (numberCOfCards > 0 && numberCOfCards < _cards.Count)
            {
                for (int i = 0; i < numberCOfCards; i++)
                {
                    cards.Add(DealCard());
                }
            }

            return new Hand(cards);
        }

        public Card DealCard()
        {
            Card card = null;
            var deckCount = _cards.Count;

            if (deckCount > 0)
            {
                int deal = _rand.Next(0, deckCount);

                card = _cards[deal];
                _cards.RemoveAt(deal);
            }

            return card;
        }

        public void ReturnCard(Card card)
        {
            _cards.Add(card);
        }

        public void Shuffle()
        {
            _cards.Clear();
            Init();
        }

        private void AddCards(Suit suit)
        {
            // , ..., King = 11, Ace = 12, big 2 = 13
            for (int i = 1; i <= 13; i++)
            {
                _cards.Add(new Card { Suit = suit, Value = i, Face = Deck.Values[i] });
            }
        }

        private void Init()
        {
            AddCards(Suit.Spades);
            AddCards(Suit.Hearts);
            AddCards(Suit.Clubs);
            AddCards(Suit.Diamonds);
        }
    }

    public enum Suit
    {
        Spades,
        Hearts,
        Clubs,
        Diamonds
    }
}
