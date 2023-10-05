using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Third;

public class Bank
{
    private static double CasinoBalance { get; set; } = 10000; 
    
    public static void UserAddMoney(User user,double money)
    {
        user.Balance += money;
    }
    
    public static void UserTakeMoney(User user,double money)
    {
        user.Balance -= money;
    }

    public static void PlayerWins(User user, double money)
    {
        UserAddMoney(user, money);
        CasinoBalance -= money;
    }
    
    public static void CasinoWins(User user, double money)
    {
        UserTakeMoney(user, money);
        CasinoBalance += money;
    }
    
    private static void ValidateBet(double balance, double bet)
    {
        if (bet > balance)
        {
            throw new PlayerBetException("На вашем балансе недостаточно средств для такой ставки");
        }
    }
    
    public static double CheckBet(User user, double bet)
    {
        try 
        {        
            ValidateBet(user.Balance, bet);
            return bet;
        }
        catch(PlayerBetException ex)
        {
            Console.WriteLine(ex.Message );
            throw;
        }
    }
}