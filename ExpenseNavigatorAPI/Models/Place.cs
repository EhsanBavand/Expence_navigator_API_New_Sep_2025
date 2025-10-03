using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace ExpenseNavigatorAPI.Models
{
    using Microsoft.AspNetCore.Identity;

    public class Place
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string UserId { get; set; } // link to AspNetUsers

        public Guid CategoryId { get; set; }        // always linked to category
        public Guid? SubCategoryId { get; set; }    // optional

        [JsonIgnore] public IdentityUser User { get; set; }
        [JsonIgnore] public Category Category { get; set; }
        [JsonIgnore] public SubCategory SubCategory { get; set; }
    }

}
