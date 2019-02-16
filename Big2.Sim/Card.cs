namespace Big2.Sim
{
    public class Card
    {
        public Suit Suit { get; set; }

        public int Value { get; set; }

        public string Face { get; set; }

        public override string ToString()
        {
            return $"{this.Face}-{this.Suit.ToString()[0]}";
        }
    }
}
