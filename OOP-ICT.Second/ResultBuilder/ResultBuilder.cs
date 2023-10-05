using OOP_ICT.Second.Enums;

namespace OOP_ICT.Second.ResultBuilder;

public class ResultBuilder : IResultBuilder
{
    private Result MyResult { set; get; }

    public ResultBuilder()
    {
        MyResult = new Result();
    }
    
    public void SetResultName(ResultOptions status)
    {
        MyResult.ResultName = status;
    }

    public void SetMoneyWin(double money)
    {
        MyResult.MoneyWin = money;
    }
    public Result ReturnResult()
    {
        return MyResult;
    }
}
