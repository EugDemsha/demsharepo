namespace OOP_ICT.Third.FromSecond;
using OOP_ICT.Second.PlayerEssentials;
using OOP_ICT.Second.ResultBuilder;
using OOP_ICT.Third;

public class Player
{ 
     public User User { set; get; }
     public double Bet { set; get; }
     public Hand Hand { set; get; }
     public Result PlayerResult { set; get; }
     
     public Player(User user, int bet)
     {
          User = user;
          Hand = new Hand();
          Bet = Bank.CheckBet(User, bet);
          PlayerResult = new Result();
     }
}