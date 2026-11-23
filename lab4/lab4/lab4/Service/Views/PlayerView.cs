namespace Lab4.Service.Views;

public class PlayerView
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string AccountType { get; set; }
    public int Rating { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, User Name: {UserName}, Account Type: {AccountType}, Rating: {Rating}";
    }
}