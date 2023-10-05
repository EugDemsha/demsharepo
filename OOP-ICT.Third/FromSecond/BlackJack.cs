using OOP_ICT.Models;
using OOP_ICT.Second.Enums;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.PlayerEssentials;
using OOP_ICT.Second.ResultBuilder;
using OOP_ICT.Third.FromSecond;

namespace OOP_ICT.Third.FromSecond;

public class BlackJack : ICardGame
{
    // public BlackJackCasino Casino { set; get; }
    private Players MyPlayers { set; get; }
    private Dealer MyDealer { set; get; }
    public Hand MyDealerHand { set; get; }

    public BlackJack()
    {
        MyPlayers = new Players();
        MyDealer = new Dealer();
        MyDealer.ReturnDeck();
        MyDealerHand = new Hand();
        //Casino = new BlackJackCasino();
    }

    public void AddPlayer(Player player)
    {
        try
        {
            ValidateAddCount(Players.PlayersList.Count);
            Players.PlayersList.Add(player);
        }
        catch (PlayerBetException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    private static void ValidateAddCount(int count)
    {
        if (count >= 7)
        {
            throw new PlayerBetException("Вы не можете добавлять больше игроков");
        }
    }

    private static void ValidatePlayCount(int count)
    {
        if (count < 2)
        {
            throw new PlayerBetException("Слишком мало игроков для начала игры");
        }
    }

    private void DealCards()
    {
        foreach (var p in Players.PlayersList)
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
        else if (score > 21)
        {
            result = "lose";
        }

        return result;
    }

    public void StartCardGame()
    {
        try
        {
            ValidatePlayCount(Players.PlayersList.Count);
            PlayCardGame();
        }
        catch (PlayerBetException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public void PlayCardGame()
    {
        DealCards();
        var dealerScore = MyDealerHand.AddCard(MyDealer.ReturnCard());
        DealCards();
        var clonedPlayersList = new List<Player>(Players.PlayersList);
        foreach (var p in clonedPlayersList)
        {
            var res = CheckCards(p.Hand.Score);
            var resultBuilder = new ResultBuilder();
            if (res == "blackjack")
            {
                resultBuilder.SetResultName(ResultOptions.BlackJack);
                resultBuilder.SetMoneyWin(p.Bet * 2.5);
                BlackJackCasino.PayBlackJack(p);
                Players.PlayersList.Remove(p);
            }
            else if (res == "lose")
            {
                resultBuilder.SetResultName(ResultOptions.Lose);
                BlackJackCasino.TakeLose(p);
                Players.PlayersList.Remove(p);
            }

            p.PlayerResult = resultBuilder.ReturnResult();
        }

        DealCards();

        while (dealerScore < 17)
        {
            dealerScore = MyDealerHand.AddCard(MyDealer.ReturnCard());
        }

        var dealerRes = CheckCards(MyDealerHand.Score);

        foreach (var p in Players.PlayersList)
        {
            var resultBuilder = new ResultBuilder();

            if (dealerRes == "blackjack")
            {
                resultBuilder.SetResultName(ResultOptions.Lose);
                BlackJackCasino.TakeLose(p);
            }
            else if (p.Hand.Score > 21)
            {
                resultBuilder.SetResultName(ResultOptions.Lose);
                BlackJackCasino.TakeLose(p);
            }
            else if (dealerRes == "lose")
            {
                resultBuilder.SetResultName(ResultOptions.Win);
                resultBuilder.SetMoneyWin(p.Bet * 2);
                BlackJackCasino.PayWin(p);
            }
            else if (p.Hand.Score < dealerScore)
            {
                resultBuilder.SetResultName(ResultOptions.Lose);
                BlackJackCasino.TakeLose(p);
            }
            else if (p.Hand.Score > dealerScore)
            {
                resultBuilder.SetResultName(ResultOptions.Win);
                resultBuilder.SetMoneyWin(p.Bet * 2);
                BlackJackCasino.PayWin(p);
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