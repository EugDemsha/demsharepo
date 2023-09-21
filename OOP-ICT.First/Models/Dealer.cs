using OOP_ICT.Interfaces;

namespace OOP_ICT.Models;

public class Dealer : IDealer
{
    private CardDeck _notShuffledDeck;
    private UserDeck _shuffledDeck;
    
    private void InitializeCardDeck()
    {
        _notShuffledDeck = new CardDeck();
        _notShuffledDeck.BuildADeck();
    }

    private UserDeck CreateShuffledUserDeck(CardDeck deck)
    {
        var list = deck.Cards;
        var len = list.Count;
        if (len % 2 != 0)
        {
            throw new ArgumentException("Length must be even.");
        }
        var half = len/2;
        Random rand = new Random();
        var iters = rand.Next(1, len);
        for (int j = 0; j <= iters; j++)
        {
            var templist = new List<Card>(new Card[len]);

            for (var i = 0; i < half; i++)
            {
                templist[i * 2] = list[i + half];
                templist[i * 2 + 1] = list[i];
            }
            list = templist;
        }

        return new UserDeck(list);

    }

    public UserDeck ReturnDeck()
    {
        InitializeCardDeck();
        _shuffledDeck = CreateShuffledUserDeck(_notShuffledDeck);
        return _shuffledDeck;
    }

    public Card ReturnCard()
    {
        Card userCard = _shuffledDeck._userCards[0];
        _shuffledDeck._userCards.RemoveAt(0);
        return userCard;
    }
}