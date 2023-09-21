using OOP_ICT.Enums;


namespace OOP_ICT.Models;

public class CardDeck
{
    public List<Card> Cards { get; set; }

    public void BuildADeck()
    {
        Cards = (from suit in Enum.GetNames(typeof(Suits)) from rank in Enum.GetNames(typeof(Ranks)) select new Card() { Suit = suit, Rank = rank }).ToList();
    }

}