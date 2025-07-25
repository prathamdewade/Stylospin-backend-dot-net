public class CustomerOrderDto
{
   
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string? ContactNumber { get; set; }
    public string? Address { get; set; }

  
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string? Message { get; set; }
    public string PaymentStatus { get; set; } = "Pending";
    
}
