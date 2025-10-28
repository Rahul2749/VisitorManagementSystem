using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models.EmployeesModels
{
    public class NGAC_AUTHLOG_Model
    {
        [Key]
        public long IndexKey { get; set; }
        public string UserID { get; set; }
        public int AuthResult { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}
