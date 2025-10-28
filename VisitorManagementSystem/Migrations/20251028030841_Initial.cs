using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VisitorManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarriedMaterial_ByVisitor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cMtrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cMtrlQuantity = table.Column<int>(type: "int", nullable: false),
                    VisitorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarriedMaterial_ByVisitor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DeptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptHOD = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DeptId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptId = table.Column<int>(type: "int", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmpId);
                });

            migrationBuilder.CreateTable(
                name: "FavVisitors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    Favorite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavVisitors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SiteMaster",
                columns: table => new
                {
                    SiteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteMaster", x => x.SiteId);
                });

            migrationBuilder.CreateTable(
                name: "VisitDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CompanySite = table.Column<int>(type: "int", nullable: false),
                    CompanyDepartment = table.Column<int>(type: "int", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitDate = table.Column<DateOnly>(type: "date", nullable: false),
                    VisitTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitArea = table.Column<bool>(type: "bit", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SafetyStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityApproval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitorId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitorMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProofNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitorTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckOut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VDetails_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitorType",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorType", x => x.TypeId);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[,]
                {
                    { 1, "India" },
                    { 2, "United States" },
                    { 3, "United Kingdom" },
                    { 4, "Germany" },
                    { 5, "Japan" }
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DeptId", "DeptHOD", "DeptName" },
                values: new object[,]
                {
                    { 1, "John Smith", "IT" },
                    { 2, "Sarah Johnson", "Human Resources" },
                    { 3, "Michael Brown", "Finance" },
                    { 4, "Emily Davis", "Operations" },
                    { 5, "David Wilson", "Marketing" },
                    { 6, "Jessica Martinez", "Sales" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmpId", "DeptId", "EmpContactNo", "EmpEmail", "EmpName", "KO", "Password", "Role", "SiteId" },
                values: new object[,]
                {
                    { 1, 1, "+91-9876543210", "admin@company.com", "Admin User", "ADM001", "Admin@123", "Admin", 1 },
                    { 2, 1, "+91-9876543211", "rajesh.kumar@company.com", "Rajesh Kumar", "IT001", "User@123", "Security", 1 },
                    { 3, 2, "+91-9876543212", "priya.sharma@company.com", "Priya Sharma", "HR001", "User@123", "Employee", 1 },
                    { 4, 3, "+91-9876543213", "amit.patel@company.com", "Amit Patel", "FIN001", "User@123", "Employee", 2 },
                    { 5, 4, "+91-9876543214", "sneha.reddy@company.com", "Sneha Reddy", "OPS001", "User@123", "Security", 1 }
                });

            migrationBuilder.InsertData(
                table: "SiteMaster",
                columns: new[] { "SiteId", "CountryId", "SiteName" },
                values: new object[,]
                {
                    { 1, 1, "Nagpur HQ" },
                    { 2, 1, "Mumbai Office" },
                    { 3, 1, "Delhi Branch" },
                    { 4, 2, "New York Office" },
                    { 5, 3, "London Office" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarriedMaterial_ByVisitor");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "FavVisitors");

            migrationBuilder.DropTable(
                name: "SiteMaster");

            migrationBuilder.DropTable(
                name: "VisitDetails");

            migrationBuilder.DropTable(
                name: "VisitorMaster");

            migrationBuilder.DropTable(
                name: "VisitorTransactions");

            migrationBuilder.DropTable(
                name: "VisitorType");
        }
    }
}
