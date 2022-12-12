namespace TopLearn.Core.DTOs.UserPanel;

public class UserTransactionsViewModel
{
    public int Amount { get; set; }
    public int Type { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
}