using System;
using System.Collections.Generic;

namespace ExpenseNavigatorAPI.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        public ICollection<Place> Places { get; set; } = new List<Place>();

    }
}
