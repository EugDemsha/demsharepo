using OOP_ICT.Third;
using OOP_ICT.Third.FromSecond;
namespace OOP_ICT.Fourth;

public class PokerBank: Bank
{
    public static double CurrentBet { set; get; }
    public static double AllBets { set; get; }
    public PokerBank()
    {
        CurrentBet = 1;
    }

    public void PayLoseWin(List<OOP_ICT.Third.FromSecond.Player> originalPlayersList, OOP_ICT.Third.FromSecond.Player winner)
    {
        foreach (var p in originalPlayersList)
        {
            if (p != winner)
            {
                CasinoWins(p.User,p.Bet);
            }
            else
            {
                PlayerWins(p.User,AllBets);
            }
        }
    }
}
