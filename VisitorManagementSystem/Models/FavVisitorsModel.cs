namespace VisitorManagementSystem.Models
{
    public class FavVisitorsModel
    {
        public int ID { get; set; }
        public string VisitorId { get; set; }
        public int EmpId { get; set; }

        public bool Favorite { get; set; }
    }
}
