namespace OOP_ICT.Models;

public class UserDeck
{
    public readonly List<Card> _userCards;

    public UserDeck(List<Card> listOfShuffledCards)
    {
        _userCards = listOfShuffledCards;
    }
    
}