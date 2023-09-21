using OOP_ICT.Models;

namespace OOP_ICT.Interfaces;

public interface IDealer
{
    UserDeck ReturnDeck();
    Card ReturnCard();

}