using Microsoft.EntityFrameworkCore;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.Models.EmployeesModels;

namespace VisitorManagementSystem.Data
{
    public class NitgenDbContext : DbContext
    {

        public NitgenDbContext()
        {
        }

        public NitgenDbContext(DbContextOptions<NitgenDbContext> options) : base(options) { }

        public DbSet<NGAC_USERINFO_Model> NGAC_USERINFO { get; set; }
        public DbSet<NGAC_AUTHLOG_Model> NGAC_AUTHLOG { get; set; }
    }
}
