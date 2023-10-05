
namespace OOP_ICT.Third.FromSecond;

public class BlackJackCasino: ICasino
{
    
    public static List<User>? UsersList { get; set; }

    public BlackJackCasino()
    {
        UsersList = new List<User>();
    }

    public static void PayWin(Player player)
    {
        //player.User.UserBank.AddMoney(player.Bet * 2);
        Bank.PlayerWins(player.User, player.Bet * 2);
    }
    
    public static void TakeLose(Player player)
    {
        Bank.CasinoWins(player.User, player.Bet);
    }
    
    public static void PayBlackJack(Player player)
    {   
        Bank.PlayerWins(player.User, player.Bet * 2.5);
    }
}