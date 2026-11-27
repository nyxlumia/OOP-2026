namespace lab5.Service.Views;

public class UserView
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public decimal AccountBalance { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, User Name: {UserName}, Account Balance: {AccountBalance}";
    }
}