namespace VisitorManagementSystem.Models
{
    public class VisitorTransactionModel
    {
        public int Id { get; set; }
        public required string VisitorId { get; set; }


        public DateOnly Date { get; set; }
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }

        public int VDetails_Id { get; set; }
    }
}
