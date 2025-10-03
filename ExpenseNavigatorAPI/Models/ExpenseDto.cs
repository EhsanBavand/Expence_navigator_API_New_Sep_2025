public class ExpenseDto
{
    public string UserId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid? SubCategoryId { get; set; }
    public Guid? PlaceId { get; set; }
    public decimal Amount { get; set; }
    public string? PaidFor { get; set; }
    public string? Note { get; set; }
    public bool IsFixed { get; set; } = false;
}
