using Microsoft.EntityFrameworkCore;
using VisitorManagementSystem.Data;
using VisitorManagementSystem.Models.EmployeesModels;

namespace VisitorManagementSystem.Services
{
    public class NitgenService : INitgenService
    {
        private readonly NitgenDbContext _context;
        private readonly IServiceScopeFactory _scopeFactory;

        public NitgenService(NitgenDbContext context, IServiceScopeFactory scopeFactory)
        {
            _context = context;
            _scopeFactory = scopeFactory;
        }
        public async Task<List<NGAC_USERINFO_Model>> GetEmployeeDataNitgen()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<NitgenDbContext>();
                var empdata = await context.NGAC_USERINFO.ToListAsync();
               
                return empdata;
            }
        }

        public async Task<List<NGAC_AUTHLOG_Model>> GetEmployeeLogsNitgen()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var today = DateTime.Today;
                var context = scope.ServiceProvider.GetRequiredService<NitgenDbContext>();
                var emplogs = await context.NGAC_AUTHLOG
                    .Where(e => e.TransactionTime.Date == today && e.UserID != "")
                    .ToListAsync();

                return emplogs;


            }
        }
    }
}
