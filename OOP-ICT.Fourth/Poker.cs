using OOP_ICT.Models;
using OOP_ICT.Second.Enums;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Third;

namespace OOP_ICT.Fourth;
using OOP_ICT.Third.FromSecond;

public class Poker: ICardGame
{
    public PokerBank PokerBank { set; get; }
    private Players Players { set; get; }
    private List<Player> OriginalPlayersList { set; get; }
    private Dealer MyDealer { set; get; }
    public List<Card> CardsOnTable { set; get; }
    
    public Poker()
    {
        Players = new Players();
        MyDealer = new Dealer();
        MyDealer.ReturnDeck();
        CardsOnTable = new List<Card>();
        PokerBank = new PokerBank();
    }

    public void AddPlayer(Player player)
    {
        try
        {
            ValidateAddCount(Players.PlayersList.Count);
            Players.PlayersList.Add(player);
        }
        catch (PlayerCountException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    private static void ValidateAddCount(int count)
    {
        if (count >= 10)
        {
            throw new PlayerCountException("Вы не можете добавлять больше игроков");
        }
    }
    
    public void StartCardGame()
    {
        try
        {
            ValidatePlayCount(Players.PlayersList.Count);
            PlayCardGame();
        }
        catch (PlayerCountException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public void PlayCardGame()
    {
        OriginalPlayersList = new List<Player>(Players.PlayersList);
        Flop();
        foreach (var p in Players.PlayersList)
        {
            RecieveCards(p);
        }
        // Начало игры: на стол выкладыватся 3 карты, каждому игроку выдается по 2 карты
    }

    private static void ValidatePlayCount(int count)
    {
        if (count < 2)
        {
            throw new PlayerCountException("Слишком мало игроков для начала игры");
        }
    }
    
    public void Flop()
    {
        var i = 0;
        while (i < 3) 
        {
            CardsOnTable.Add(MyDealer.ReturnCard());
            i++;
        }
    }

    public void NewCardOnTable()
    {
        CardsOnTable.Add(MyDealer.ReturnCard());
    }

    public bool CheckPlayerInGame(Player player)
    {
        return Players.PlayersList.Contains(player);
    }

    public void RecieveCards(Player player)
    {
        player.Hand.AddCard(MyDealer.ReturnCard());
        player.Hand.AddCard(MyDealer.ReturnCard());
    }

    public void Call(Player player)
    {
        User user = player.User;
        var sameBet = Bank.CheckBet(user, PokerBank.CurrentBet);
        player.Bet += sameBet;
        PokerBank.AllBets += sameBet;
    }
    
    public void Rise(Player player, double bet)
    {
        User user = player.User;
        bet = Bank.CheckBet(user, bet);
        if (player.Bet > PokerBank.CurrentBet)
        {
            player.Bet += bet;
            PokerBank.CurrentBet = bet;
            PokerBank.AllBets += bet;
        }
    }
    
    public void Fold(Player player)
    {
        Players.PlayersList.Remove(player);
        player.PlayerResult.ResultName = ResultOptions.Lose;
    }
    
    public void Check(Player player)
    {
        //не добавлять ничего в банк, оставить «как есть»
    }
    
    public Player FindWinner()
    {
        var winCount = 0;
        List<Player> winners = new List<Player>();
        foreach (var p in Players.PlayersList)
        {
            List<Card> allCards = CardsOnTable.Concat(p.Hand.CardsInHand).ToList();
            ResultOptions result = Combinations.CheckCombo(allCards);
            p.PlayerResult.ResultName = result;
            var count = (int)result;
            if (count > winCount)
            {
                winCount = count;
                winners.Clear();
                winners.Add(p);
            }
            else if (count == winCount)
            {
                winners.Add(p);
            }
        }
        Player winner = winners.MaxBy(t => t.Hand.Score);
        PokerBank.PayLoseWin(OriginalPlayersList,winner);
        return winner;
    }
    

}