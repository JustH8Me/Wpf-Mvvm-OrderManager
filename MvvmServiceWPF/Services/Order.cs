namespace MvvmServiceWPF.Services;

public enum OrderStatus
{
    New,
    Processing,
    Completed,
    Cancelled
}

public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string ClientName { get; set; }
    public string Phone { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public OrderStatus Status { get; set; }
}