using Microsoft.VisualBasic;
using OOP_ICT.Enums;
using OOP_ICT.Models;
using Xunit;

namespace OOP_ICT.First.Tests;

public class Tests
{
    // TODO: Обратите внимание, что для коллекций и проверок есть разные виды Assert
    [Fact]
    public void CheckTestIsWorking_CorrectBuild()
    {
        Assert.True(true);
    }

    [Fact]
    public void ReturnCardCheck()
    {
        
        var myDealer = new Dealer();
        var myDeck = myDealer.ReturnDeck();
        var lenDeck = myDeck._userCards.Count;
        var myCard = myDealer.ReturnCard();
        Assert.Equal(lenDeck-1, myDeck._userCards.Count);
    }

    [Fact]
    public void ValueContainsInCollection_True()
    {
        var myDealer = new Dealer();
        var myDeck1 = myDealer.ReturnDeck();
        var myCard1 = myDealer.ReturnCard();
        var myDeck2 = myDealer.ReturnDeck();
        var myCard2 = myDealer.ReturnCard();
        Assert.NotSame(myCard1, myCard2);
    }

    [Fact]
    public void CheckForNullException_AssertNullRef()
    {
        var myDealer = new Dealer();
        var collection = myDealer.ReturnDeck()._userCards;
        Assert.Throws<InvalidOperationException>(() => collection.First(x => x == null));
    }
}