namespace OOP_ICT.Second.Exceptions;

public class PlayerBetException : Exception
{
    public PlayerBetException() {  }

    public PlayerBetException(string mes)
        : base(mes) { }
}