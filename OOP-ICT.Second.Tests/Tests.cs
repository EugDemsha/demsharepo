using System.Text.RegularExpressions;
using OOP_ICT.Models;
using OOP_ICT.Second.Enums;
using OOP_ICT.Second.Game;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.ResultBuilder;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class Tests
{
    [Fact]
    public void CheckTestIsWorking_CorrectBuild()
    {
        Assert.True(true);
    }

    [Fact]
    public void NotEnoughPlayers()
    {
        BlackJack BJ = new BlackJack();
        Player Masha = new Player("Masha", 10);
        BJ.AddPlayer(Masha);
        
        Assert.Throws<PlayerCountException>(() => BJ.StartCardGame());
    }
    
    [Fact]
    public void TooManyPlayers()
    {
        BlackJack BJ = new BlackJack();
        Player Masha = new Player("Masha", 10);

        BJ.AddPlayer(Masha);
        BJ.AddPlayer(Masha);
        BJ.AddPlayer(Masha);
        BJ.AddPlayer(Masha);
        BJ.AddPlayer(Masha);
        BJ.AddPlayer(Masha);
        BJ.AddPlayer(Masha);

        Assert.Throws<PlayerCountException>(() => BJ.AddPlayer(Masha));
    }
    
    [Fact]
    public void DifferentCardLists()
    {
        string StringJoin(List<Card> clction)
        {
            return clction.Aggregate("", (current, card) => current + card.Rank + card.Suit + ',');
        }
        
        BlackJack BJ = new BlackJack();
        Player Masha = new Player("Masha", 10);
        BJ.AddPlayer(Masha);
        Player Tom = new Player("Tom", 1);
        BJ.AddPlayer(Tom);
        BJ.StartCardGame();
        
        var mashaCards = StringJoin(Masha.Hand.CardsInHand);
        var tomCards = StringJoin(Tom.Hand.CardsInHand);
        
        Assert.NotSame(mashaCards, tomCards);
    }

    [Fact]
    public void CorrectBuilder()
    {
        Player Masha = new Player("Masha", 10);
        
        var resultBuilder = new ResultBuilder.ResultBuilder();
        resultBuilder.SetResultName(ResultOptions.Lose);
        Masha.PlayerResult = resultBuilder.ReturnResult();
        string mashaResult = "It's a Lose! Your winning is 0$";
        
        Player Tom = new Player("Masha", 100);
        
        var resultBuilder1 = new ResultBuilder.ResultBuilder();
        resultBuilder1.SetResultName(ResultOptions.Win);
        resultBuilder1.SetMoneyWin(Tom.Bet * 2);
        Tom.PlayerResult = resultBuilder1.ReturnResult();
        string tomResult = "It's a Win! Your winning is 200$";
        
        Player Zayka = new Player("Masha", 1);
        
        var resultBuilder2 = new ResultBuilder.ResultBuilder();
        resultBuilder2.SetResultName(ResultOptions.BlackJack);
        resultBuilder2.SetMoneyWin(Zayka.Bet * 2.5);
        Zayka.PlayerResult = resultBuilder2.ReturnResult();
        string zaykaResult = "It's a BlackJack! Your winning is 2,5$";

        Assert.Equal(mashaResult, Masha.PlayerResult.ViewRes());
        Assert.Equal(tomResult, Tom.PlayerResult.ViewRes());
        Assert.Equal(zaykaResult, Zayka.PlayerResult.ViewRes());
    }
}