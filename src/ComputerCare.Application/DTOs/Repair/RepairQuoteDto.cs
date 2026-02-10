namespace ComputerCare.Application.DTOs.Repair;

public class RepairQuoteDto
{
    public Guid RepairRequestId { get; set; }
    public List<QuoteItemDto> Items { get; set; } = new List<QuoteItemDto>();
    public decimal TotalCost => Items.Sum(i => i.Cost);
}

public class QuoteItemDto
{
    public Guid ServiceId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Cost { get; set; }
}
