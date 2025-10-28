using Microsoft.EntityFrameworkCore;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.Models.CarriedMtrlModel;

namespace VisitorManagementSystem.Data
{
    public class VMSDbContext : DbContext
    {
        public VMSDbContext()
        {
        }

        public VMSDbContext(DbContextOptions<VMSDbContext> options) : base(options) { }

        public DbSet<VisitorMasterModel> VisitorMaster { get; set; }
        public DbSet<VisitingDetailsModel> VisitDetails { get; set; }
        public DbSet<VisitorTransactionModel> VisitorTransactions { get; set; }
        public DbSet<EmpData>? Employee { get; set; }
        public DbSet<DeptData>? Department { get; set; }
        public DbSet<CompanySiteModel>? SiteMaster { get; set; }
        public DbSet<VTypeModel>? VisitorType { get; set; }
        public DbSet<CarriedMaterialModel>? CarriedMaterial_ByVisitor { get; set; }
        public DbSet<FavVisitorsModel>? FavVisitors { get; set; }
        public DbSet<CountryModel>? Country { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Country Data
            modelBuilder.Entity<CountryModel>().HasData(
                new CountryModel { CountryId = 1, CountryName = "India" },
                new CountryModel { CountryId = 2, CountryName = "United States" },
                new CountryModel { CountryId = 3, CountryName = "United Kingdom" },
                new CountryModel { CountryId = 4, CountryName = "Germany" },
                new CountryModel { CountryId = 5, CountryName = "Japan" }
            );

            // Seed Site Master Data
            modelBuilder.Entity<CompanySiteModel>().HasData(
                new CompanySiteModel { SiteId = 1, SiteName = "Nagpur HQ", CountryId = 1 },
                new CompanySiteModel { SiteId = 2, SiteName = "Mumbai Office", CountryId = 1 },
                new CompanySiteModel { SiteId = 3, SiteName = "Delhi Branch", CountryId = 1 },
                new CompanySiteModel { SiteId = 4, SiteName = "New York Office", CountryId = 2 },
                new CompanySiteModel { SiteId = 5, SiteName = "London Office", CountryId = 3 }
            );

            // Seed Department Data
            modelBuilder.Entity<DeptData>().HasData(
                new DeptData { DeptId = 1, DeptName = "IT", DeptHOD = "John Smith" },
                new DeptData { DeptId = 2, DeptName = "Human Resources", DeptHOD = "Sarah Johnson" },
                new DeptData { DeptId = 3, DeptName = "Finance", DeptHOD = "Michael Brown" },
                new DeptData { DeptId = 4, DeptName = "Operations", DeptHOD = "Emily Davis" },
                new DeptData { DeptId = 5, DeptName = "Marketing", DeptHOD = "David Wilson" },
                new DeptData { DeptId = 6, DeptName = "Sales", DeptHOD = "Jessica Martinez" }
            );

            // Seed Employee Data
            modelBuilder.Entity<EmpData>().HasData(
                new EmpData
                {
                    EmpId = 1,
                    EmpName = "Admin User",
                    KO = "ADM001",
                    EmpEmail = "admin@company.com",
                    EmpContactNo = "+91-9876543210",
                    DeptId = 1,
                    SiteId = 1,
                    Password = "Admin@123", // Consider hashing passwords in production
                    Role = "Admin"
                },
                new EmpData
                {
                    EmpId = 2,
                    EmpName = "Rajesh Kumar",
                    KO = "IT001",
                    EmpEmail = "rajesh.kumar@company.com",
                    EmpContactNo = "+91-9876543211",
                    DeptId = 1,
                    SiteId = 1,
                    Password = "User@123",
                    Role = "Security"
                },
                new EmpData
                {
                    EmpId = 3,
                    EmpName = "Priya Sharma",
                    KO = "HR001",
                    EmpEmail = "priya.sharma@company.com",
                    EmpContactNo = "+91-9876543212",
                    DeptId = 2,
                    SiteId = 1,
                    Password = "User@123",
                    Role = "Employee"
                },
                new EmpData
                {
                    EmpId = 4,
                    EmpName = "Amit Patel",
                    KO = "FIN001",
                    EmpEmail = "amit.patel@company.com",
                    EmpContactNo = "+91-9876543213",
                    DeptId = 3,
                    SiteId = 2,
                    Password = "User@123",
                    Role = "Employee"
                },
                new EmpData
                {
                    EmpId = 5,
                    EmpName = "Sneha Reddy",
                    KO = "OPS001",
                    EmpEmail = "sneha.reddy@company.com",
                    EmpContactNo = "+91-9876543214",
                    DeptId = 4,
                    SiteId = 1,
                    Password = "User@123",
                    Role = "Security"
                }
            );
        }
    }
}