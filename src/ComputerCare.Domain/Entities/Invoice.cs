namespace ComputerCare.Domain.Entities;

public class Invoice : BaseEntity
{
    public Guid OrderId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public string? PdfUrl { get; set; }

    // Navigation properties
    public Order Order { get; set; } = null!;
}
