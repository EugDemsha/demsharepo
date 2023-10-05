

using OOP_ICT.Second.PlayerEssentials;
using OOP_ICT.Second.ResultBuilder;

public class Player
{ 
     public string Name { set; get; }
     public double Bet { set; get; }
     public Hand Hand { set; get; }
     public Result PlayerResult { set; get; }
     
     public Player(string name, int bet)
     {
          Name = name;
          Bet = bet;
          Hand = new Hand();
          PlayerResult = new Result();
     } 
     
}