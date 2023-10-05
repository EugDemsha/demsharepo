using OOP_ICT.Second.Enums;

namespace OOP_ICT.Second.ResultBuilder;

public class Result
{
    public ResultOptions ResultName { set; get; }
    public double MoneyWin { set; get; }
    
    public Result()
    {
        ResultName = ResultOptions.None;
        MoneyWin = 0;
    }

    public string ViewRes()
    {
        return $"It's a {ResultName}! Your winning is {MoneyWin}$";
    }
}