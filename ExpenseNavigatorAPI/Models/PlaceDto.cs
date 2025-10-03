namespace ExpenseNavigatorAPI.Models
{
    public class PlaceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
    }

    public class PlaceDropdownDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
