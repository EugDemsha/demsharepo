using OOP_ICT.Models;
using OOP_ICT.Second.Enums;

namespace OOP_ICT.Second.PlayerEssentials;

//прототип
public class Hand 
{
    public List<Card> CardsInHand { get; set; }
    public int Score { get; set; }

    public Hand()
    {
        CardsInHand = new List<Card>();
        Score = 0;
    }

    public int AddCard(Card card)
    {
        CardsInHand.Add(card);
        Score += (int)Enum.Parse(typeof(RankCount), card.Rank);
        return Score;
    }

    public Hand DeepCopy()
    {
        Hand clone = (Hand)this.MemberwiseClone();
        clone.CardsInHand = CardsInHand.ConvertAll(card => new Card(){ Suit = card.Suit, Rank = card.Rank });
        return clone;
    }
}