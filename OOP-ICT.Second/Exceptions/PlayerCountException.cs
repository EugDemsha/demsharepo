namespace OOP_ICT.Second.Exceptions;

public class PlayerCountException : Exception
{
    public PlayerCountException() {  }

    public PlayerCountException(string mes)
        : base(mes) { }
}