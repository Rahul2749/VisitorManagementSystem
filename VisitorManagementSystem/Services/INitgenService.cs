using VisitorManagementSystem.Models;
using VisitorManagementSystem.Models.EmployeesModels;

namespace VisitorManagementSystem.Services
{
    public interface INitgenService
    {
        Task<List<NGAC_USERINFO_Model>> GetEmployeeDataNitgen();
        Task<List<NGAC_AUTHLOG_Model>> GetEmployeeLogsNitgen();
    }
}
