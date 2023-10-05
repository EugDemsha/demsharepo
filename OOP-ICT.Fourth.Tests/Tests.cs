using Microsoft.VisualBasic;
using OOP_ICT.Models;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Third;
using Xunit;

namespace OOP_ICT.Fourth.Tests;

public class Tests
{
    [Fact]
    public void CheckTestIsWorking_CorrectBuild()
    {
        Assert.True(true);
    }

    [Fact]
    public void CheckCombinations()
    {
        List<Card> cards = new List<Card>();
        cards.Add(new Card() { Suit = "Diamonds", Rank = "King" });
        cards.Add( new Card() { Suit = "Diamonds", Rank = "Queen" });
        cards.Add(new Card() { Suit = "Diamonds", Rank = "Ace" });
        cards.Add(new Card() { Suit = "Diamonds", Rank = "Jack" });
        cards.Add( new Card() { Suit = "Diamonds", Rank = "Ten" });
        cards.Add(new Card() { Suit = "Spades", Rank = "Ten" });
        cards.Add(new Card() { Suit = "Hearts", Rank = "Two" });
        
        Combinations.Cards = cards;
        Combinations.RankCounter = Combinations.CountRanks();
        Assert.True(Combinations.OnePair());
        Assert.False(Combinations.TwoPairs());
        Assert.False(Combinations.ThreeOfAKind());
        Assert.True(Combinations.Straight());
        Assert.False(Combinations.FullHouse());
        Assert.False(Combinations.FourOfAKind());
        Assert.True(Combinations.StraightFlush());
        Assert.True(Combinations.RoyalFlush());
    }

    [Fact]
    public void WinnerGetsMoney()
    {
        Poker poker = new Poker();
        User masha = new User("Masha");
        double mashaOriginalBalance = 100;
        Bank.UserAddMoney(masha, mashaOriginalBalance);
        OOP_ICT.Third.FromSecond.Player masha1 = new OOP_ICT.Third.FromSecond.Player(masha, 10);
        poker.AddPlayer(masha1);
        User tom = new User("Tom");
        Bank.UserAddMoney(tom, 10);
        OOP_ICT.Third.FromSecond.Player tom1 = new OOP_ICT.Third.FromSecond.Player(tom, 1);
        poker.AddPlayer(tom1);
        User zayka = new User("Zayka");
        Bank.UserAddMoney(zayka, 10);
        OOP_ICT.Third.FromSecond.Player zayka1 = new OOP_ICT.Third.FromSecond.Player(zayka, 1);
        poker.AddPlayer(zayka1);
        poker.StartCardGame();
        poker.Check(masha1);
        poker.Rise(tom1,2);
        poker.Call(zayka1);
        poker.NewCardOnTable();
        poker.Rise(masha1,12);;
        poker.Fold(tom1);
        poker.Fold(zayka1);
        poker.NewCardOnTable();
        OOP_ICT.Third.FromSecond.Player winner = poker.FindWinner();
        Assert.True(mashaOriginalBalance < masha.Balance);
    }

    [Fact]
    public void EnoughMoneyForBet()
    {
        Poker poker = new Poker();
        User masha = new User("Masha");
        double mashaOriginalBalance = 100;
        Bank.UserAddMoney(masha, mashaOriginalBalance);
        OOP_ICT.Third.FromSecond.Player masha1 = new OOP_ICT.Third.FromSecond.Player(masha, 10);
        poker.AddPlayer(masha1);
        User tom = new User("Tom");
        Bank.UserAddMoney(tom, 10);
        OOP_ICT.Third.FromSecond.Player tom1 = new OOP_ICT.Third.FromSecond.Player(tom, 1);
        poker.AddPlayer(tom1);
        poker.StartCardGame();
        poker.Check(masha1);
        Assert.Throws<PlayerBetException>(() => poker.Rise(tom1,12));
    }
}