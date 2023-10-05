namespace OOP_ICT.Third;

public class User: IUser
{
    //public Bank UserBank { set; get; }
    public double Balance { set; get; }
    public string Name { set; get; }

    public User(string name)
    {
        Name = name;
        //UserBank = new Bank();
        Balance = 0;
    } 

}