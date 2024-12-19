namespace Domain.Model;

public class Order
{
    private int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int TableId { get; set; }
    public string Status { get; set; }
}