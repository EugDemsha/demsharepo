using OOP_ICT.Second.Enums;

namespace OOP_ICT.Second.ResultBuilder;

public interface IResultBuilder
{
    public void SetResultName(ResultOptions status);
    public void SetMoneyWin(double money);
    public Result ReturnResult();
}