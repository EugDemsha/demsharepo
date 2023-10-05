using OOP_ICT.Enums;
using OOP_ICT.Models;
using OOP_ICT.Second.Enums;
using OOP_ICT.Third.FromSecond;

namespace OOP_ICT.Fourth;

public class Combinations
{
    public static List<Card> Cards { set; get; }
    public static IEnumerable<RankNameCount> RankCounter { set; get; }

    public static ResultOptions CheckCombo(List<Card> cards)
    {
        Cards = cards;
        RankCounter = CountRanks();
        if (RoyalFlush())
        {
            return ResultOptions.RoyalFlush;
        } 
        else if (StraightFlush()) 
        {
            return ResultOptions.StraightFlush;
        } 
        else if (FourOfAKind()) 
        {
            return ResultOptions.FourOfAKind;
        } 
        else if (FullHouse()) 
        {
            return ResultOptions.FullHouse;
        } 
        else if (Straight()) 
        {
            return ResultOptions.Straight;
        } 
        else if (ThreeOfAKind()) 
        {
            return ResultOptions.ThreeOfAKind;
        } 
        else if (TwoPairs()) 
        {
            return ResultOptions.TwoPairs;
        } 
        else if (OnePair()) 
        {
            return ResultOptions.OnePair;
        } 
        else
        {
            return ResultOptions.HighCard;
        }
    }

    public class RankNameCount
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
    static bool IsSequential(List<int> list)
    {
        bool result = false;
        list.Sort();
        for (int i = 0; i < 3; i++)
        {
            List<int> subList = list.GetRange(i, 5);
            bool value = subList.Zip(subList.Skip(1), (a, b) => (a + 1) == b).All(x => x);
            if (value)
            {
                result = value;
            }
        }
        return result;
    }

    public static IEnumerable<RankNameCount> CountRanks()
    {
        return Cards
            .GroupBy(x => x.Rank)
            .Select(g => new RankNameCount { Name = g.Key, Count = g.Count() });
    }

    public static bool FourOfAKind()

    {
        return RankCounter.Any(result => result.Count == 4);
    }

    public static bool ThreeOfAKind()

    {
        return RankCounter.Any(result => result.Count == 3);
    }

    public static bool Flush()

    {
        var query = Cards
            .GroupBy(x => x.Suit)
            .Select(g => new { Name = g.Key, Count = g.Count() });

        return query.Any(result => result.Count == 5);
    }

    public static bool RoyalFlush()

    {
        if (!Flush()) return false;
        List<string> ranks = new List<string>(Cards.Select(x => x.Rank));
        List<string> highRanks = new List<string>() { "Ace", "King", "Queen", "Jack", "Ten" };
        return highRanks.All(str => ranks.Any(word => word == str));
    }

    public static bool Straight()

    {
        List<int> order = new List<int>(Cards.Select(x => (int)Enum.Parse(typeof(Ranks), x.Rank)));
        return IsSequential(order);
    }

    public static bool StraightFlush()

    {
        return Flush() && Straight();
    }

    public static bool TwoPairs()

    {
        var count = RankCounter.Count(result => result.Count == 2);
        return count == 2;
    }

    public static bool OnePair()

    {
        return RankCounter.Any(result => result.Count == 2);
    }

    public static bool FullHouse()

    {
        return ThreeOfAKind() && OnePair();
    }
}