using OOP_ICT.Models;
using OOP_ICT.Second.Enums;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.PlayerEssentials;

namespace OOP_ICT.Second.Game;

public class BlackJack : ICardGame
{
    private Players MyPlayers { set; get; }
    private Dealer MyDealer { set; get; }
    public Hand MyDealerHand { set; get; }

    public BlackJack()
    {
        MyPlayers = new Players();
        MyDealer = new Dealer();
        MyDealer.ReturnDeck();
        MyDealerHand = new Hand();
    }
    
    public void AddPlayer(Player player)
    {
        
        try
        {        
            ValidateAddCount(MyPlayers.PlayersList.Count);
            MyPlayers.PlayersList.Add(player);
        }
        catch(PlayerCountException ex)
        {
            Console.WriteLine(ex.Message );
            throw;
        }
        
    }
    
    private static void ValidateAddCount(int count)
    {
        if (count >= 7)
        {
            throw new PlayerCountException("Вы не можете добавлять больше игроков");
        }
    }
    
    private static void ValidatePlayCount(int count)
    {
        if (count < 2)
        {
            throw new PlayerCountException("Слишком мало игроков для начала игры");
        }
    }

    private void DealCards()
    {
        foreach (var p in MyPlayers.PlayersList)
        {
            p.Hand.AddCard(MyDealer.ReturnCard());
        } 
    }
    
    public string CheckCards(int score)
    {
        string result = "none";
        if (score == 21)
        {
            result = "blackjack";
        }
        else if(score > 21)
        {
            result = "lose";
        }

        return result;
    }

    public void StartCardGame()
    {
        try 
        {        
            ValidatePlayCount(MyPlayers.PlayersList.Count);
            PlayCardGame();
        }
        catch(PlayerCountException ex)
        {
            Console.WriteLine(ex.Message );
            throw;
        }
    }

    public void PlayCardGame()
    {
        DealCards();
        var dealerScore = MyDealerHand.AddCard(MyDealer.ReturnCard());
        DealCards();
        var clonedPlayersList = new List<Player>(MyPlayers.PlayersList);
        foreach (var p in clonedPlayersList)
        {
            var res = CheckCards(p.Hand.Score);
            var resultBuilder = new ResultBuilder.ResultBuilder();
            if (res == "blackjack")
            {
                resultBuilder.SetResultName(ResultOptions.BlackJack);
                resultBuilder.SetMoneyWin(p.Bet * 2.5);
                MyPlayers.PlayersList.Remove(p);
            }
            else if (res == "lose")
            {
                resultBuilder.SetResultName(ResultOptions.Lose);
                MyPlayers.PlayersList.Remove(p);
            }
            p.PlayerResult = resultBuilder.ReturnResult();
        }
        DealCards();
        
        while (dealerScore < 17)
        {
            dealerScore = MyDealerHand.AddCard(MyDealer.ReturnCard());
        }
        
        var dealerRes = CheckCards(MyDealerHand.Score);
        
        foreach (var p in MyPlayers.PlayersList)
        {
            var resultBuilder = new ResultBuilder.ResultBuilder();
            
            if (dealerRes == "blackjack")
            {
                resultBuilder.SetResultName(ResultOptions.Lose);
            }
            else if (p.Hand.Score > 21)
            {
                resultBuilder.SetResultName(ResultOptions.Lose);
            }
            else if (dealerRes == "lose")
            {
                resultBuilder.SetResultName(ResultOptions.Win);
                resultBuilder.SetMoneyWin(p.Bet * 2);
            }
            else if (p.Hand.Score < dealerScore)
            {
                resultBuilder.SetResultName(ResultOptions.Lose);
            }
            else if (p.Hand.Score > dealerScore)
            {
                resultBuilder.SetResultName(ResultOptions.Win);
                resultBuilder.SetMoneyWin(p.Bet * 2);
            }
            else if (p.Hand.Score == dealerScore)
            {
                resultBuilder.SetResultName(ResultOptions.Tie);
                resultBuilder.SetMoneyWin(p.Bet);
            }
            p.PlayerResult = resultBuilder.ReturnResult();
                
        }
        
    }
}

