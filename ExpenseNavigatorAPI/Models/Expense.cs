using System.Text.Json.Serialization;

namespace ExpenseNavigatorAPI.Models
{
    public class Expense
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } // Link expense to user

        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; } 
        public Guid? PlaceId { get; set; } // Optional if place is user-defined

        public decimal Amount { get; set; }
        public string? PaidFor { get; set; } // e.g., Ehsan, Minoo
        public string? Note { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool IsFixed { get; set; } = false;

        public string? ExtraData { get; set; } // JSON for category-specific fields

        // Navigation Properties
        [JsonIgnore] public Category Category { get; set; }
        [JsonIgnore] public SubCategory SubCategory { get; set; }
        [JsonIgnore] public Place? Place { get; set; }
    }
}
