using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel;
using VisitorManagementSystem.Data;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.Models.CarriedMtrlModel;
using static QRCoder.PayloadGenerator;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VisitorManagementSystem.Services
{
    public class VDataService : IVDataService
    {
        private readonly VMSDbContext _context;
        private readonly IServiceScopeFactory _scopeFactory;

        public VDataService(VMSDbContext context, IServiceScopeFactory scopeFactory)
        {
            _context = context;
            _scopeFactory = scopeFactory;
        }
        /*
        public VDataService(VMSDbContext context)
        {
            _context = context;
        }*/
        /*
        public async Task<List<VisitorMasterModel>> GetAllVisitorData()
        {

            var formdata = await _context.VisitorMaster.ToListAsync();
            return formdata;
            
        }*/

        public async Task<List<VisitorMasterModel>> GetAllVisitorData()
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var formdata = await context.VisitorMaster.ToListAsync();
                //formdata = formdata.Where(x => x.Gender == "Male").ToList();
                return formdata;
            }
        }
        public async Task<VisitorMasterModel> GetVisitorDataById(string vid)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitorData = await context.VisitorMaster
                    .Where(vm => vm.VisitorId == vid)
                    .FirstOrDefaultAsync();
                return visitorData;
            }
        }

        public async Task<VisitingDetailsModel> GetVDetailsByID(string vid)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var lastVisitorData = await context.VisitDetails.Where(x => x.VisitorId == vid)
                    .OrderByDescending(vm => vm.RegistrationDate)
                    .OrderByDescending(t => t.VisitTime)// Assuming CreatedDate is a DateTime property
                    .FirstOrDefaultAsync();
                return lastVisitorData;
            }
        }

        public async Task<List<FavVisitorsModel>> GetFavVisitors()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var favdata = await context.FavVisitors.Where(x => x.Favorite != false).ToListAsync();
                //formdata = formdata.Where(x => x.Gender == "Male").ToList();
                return favdata;
            }
        }

        /*
        public async Task<List<VisitingDetailsModel>> GetAllVisitingDetails()
        {

            var visitingdata = await _context.VisitDetails.ToListAsync();
            return visitingdata;

        }*/
        public async Task<List<VisitingDetailsModel>> GetAllVisitingDetails()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitingdata = await context.VisitDetails.ToListAsync();
                return visitingdata;
            }
        }
        public async Task<VisitingDetailsModel> GetFavVisitorDetailsByVIdEmp(string vid, string emp)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitorDetails = await context.VisitDetails
                    .Where(vm => vm.VisitorId == vid && vm.ApprovedBy == emp)
                    .FirstOrDefaultAsync();
                return visitorDetails;
            }
        }





        public async Task<List<VisitingDetailsModel>> GetDateWiseVisitingDetails(DateOnly date)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
               
                var visitingDetailsData = context.VisitDetails
                    .AsEnumerable().Where(v =>
                               v.VisitDate <= date &&
                               v.ToDate >= date)
                    .ToList();
                return visitingDetailsData;
            }
        }


        public async Task<List<VisitingDetailsModel>> GetDateWiseVisitingDetailsReport(DateOnly date)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var transactionData = await context.VisitorTransactions.Where(vt => vt.Date == date).ToListAsync();
                var visitingDetailsData = context.VisitDetails
                    .AsEnumerable().Where(v => transactionData.Select(td => td.VisitorId).Contains(v.VisitorId) &&
                               v.VisitDate <= date &&
                               v.ToDate >= date)
                    .ToList();
                return visitingDetailsData;
            }
        }

        public async Task<List<VisitingDetailsModel>> GetDateRangeWiseVisitingDetailsReport(DateOnly FromDate, DateOnly ToDate)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var transactionData = await context.VisitorTransactions.Where(vt => vt.Date >= FromDate && vt.Date <= ToDate).ToListAsync();
                var visitingDetailsData = context.VisitDetails
                    .AsEnumerable().Where(v => transactionData.Select(td => td.VisitorId).Contains(v.VisitorId) &&
                               v.VisitDate >= FromDate &&
                               v.ToDate <= ToDate)
                    .ToList();
                return visitingDetailsData;
            }
        }

        public async Task<List<VisitingDetailsModel>> GetDateWiseVisitingDetailsReportNoTransactions(DateOnly date)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitingDetailsData = context.VisitDetails
                    .AsEnumerable().Where(v =>
                               v.VisitDate <= date &&
                               v.ToDate >= date)
                    .ToList();
                return visitingDetailsData;
            }
        }

        public async Task<List<VisitingDetailsModel>> GetAllVisitingDetailsMonthlyReport()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                //var date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                DateOnly date = DateOnly.FromDateTime(DateTime.Now.Date);
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var transactionData = context.VisitorTransactions.AsEnumerable().Where(vt => vt.Date.Month == date.Month && vt.Date.Year == date.Year).ToList();
                var visitingDetailsData = context.VisitDetails
                   .AsEnumerable().Where(v => transactionData.Select(td => td.VisitorId).Contains(v.VisitorId) &&
                                v.VisitDate.Year == date.Year &&
                                v.VisitDate.Month == date.Month &&
                                v.ToDate.Year == date.Year &&
                                v.ToDate.Month == date.Month &&
                                transactionData.Any(td => td.Date >= v.VisitDate && td.Date <= v.ToDate))
                   .ToList();
                return visitingDetailsData;
            }
        }

        public async Task<List<VisitingDetailsModel>> GetAllVisitingDetailsYearlyReport()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                DateOnly date = DateOnly.FromDateTime(DateTime.Now.Date);
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var transactionData = context.VisitorTransactions.AsEnumerable().Where(vt => vt.Date.Year == date.Year).ToList();
                var visitingDetailsData = context.VisitDetails
                   .AsEnumerable().Where(v => transactionData.Select(td => td.VisitorId).Contains(v.VisitorId) &&
                                v.VisitDate.Year == date.Year &&                                
                                v.ToDate.Year == date.Year &&                                
                                transactionData.Any(td => td.Date >= v.VisitDate && td.Date <= v.ToDate))
                   .ToList();
                return visitingDetailsData;
            }
        }


        public async Task<List<VisitingDetailsModel>> GetVisitingDetailsOnlyToday()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now.Date);
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
               
                var visitingDetailsData = context.VisitDetails
                    .AsEnumerable().Where(v => v.VisitDate <= todayDate &&
                               v.ToDate >= todayDate)
                    .ToList();
                return visitingDetailsData;
            }
        }

        public async Task<List<VisitingDetailsModel>> GetDateWiseVisitingDetailsWeekly()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
               // DateOnly date = DateOnly.FromDateTime(DateTime.Now.Date);
               // DateOnly edate = DateOnly.FromDateTime(DateTime.Now.Date).AddDays(7);
               // DateOnly weekStart = DateOnly.FromDateTime(DateTime.Now.Date).AddDays(-7);
               // var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();

               // var visitingDetailsData = context.VisitDetails
               //     .AsEnumerable().Where(v =>
               //                v.VisitDate >= weekStart &&
               //                v.ToDate <= date)
               //     .ToList();
               // return visitingDetailsData;


                DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now.Date);
                DateOnly endDate = DateOnly.FromDateTime(DateTime.Now.Date).AddDays(7);
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var filteredVisits = context.VisitDetails
                    .AsEnumerable().Where(v =>
                    (v.VisitDate >= currentDate && v.VisitDate <= endDate) ||
                    (v.ToDate >= currentDate && v.ToDate <= endDate) ||
                    (v.VisitDate < currentDate && v.ToDate > endDate)
                ).ToList();

                return filteredVisits;
            }
        }

        public async Task<List<VisitingDetailsModel>> GetDateWiseVisitingDetailsWeekly7DaysAhead()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                DateOnly date = DateOnly.FromDateTime(DateTime.Now.Date);
                DateOnly edate = DateOnly.FromDateTime(DateTime.Now.Date).AddDays(7);
                
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();

                //var visitingDetailsData = context.VisitDetails
                //    .AsEnumerable().Where(v =>
                //               v.VisitDate <= date &&
                //               v.ToDate >= edate)
                //    .ToList();

                var visitingDetailsData = context.VisitDetails
                    .AsEnumerable()
                    .Where(v =>
                        v.VisitDate <= edate && // VisitDate is before or on the end of the range
                        v.ToDate >= date // ToDate is after or on the start of the range
                    ).ToList();
                return visitingDetailsData;
            }
        }


        public async Task<List<VisitorTransactionModel>> GetAllVisitorTransactions()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                string todayDate = DateTime.Now.ToString("yyyy-MM-dd");
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitorTransactiondata = await context.VisitorTransactions.ToListAsync();
                return visitorTransactiondata;
            }
        }
        public async Task<List<VisitorTransactionModel>> GetVisitorTransactionsOnlyToday()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var todayDate = DateOnly.FromDateTime(DateTime.Now.Date);
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitorTransactiondata = await context.VisitorTransactions.Where(v => v.Date == todayDate).ToListAsync();
                return visitorTransactiondata;
            }
        }
        public async Task<List<VisitorTransactionModel>> GetVisitorTransactionsMonthWise()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitorTransactiondata = await context.VisitorTransactions
                    .Where(v => v.Date.Year == todayDate.Year && v.Date.Month == todayDate.Month)
                    .ToListAsync();
                return visitorTransactiondata;
            }
        }
        public async Task<List<VisitorTransactionModel>> GetVisitorTransactionsYearWise()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitorTransactiondata = await context.VisitorTransactions.Where(v => v.Date.Year == todayDate.Year).ToListAsync();
                return visitorTransactiondata;
            }
        }
        public async Task<List<VisitorTransactionModel>> GetVisitorTransactionsDateWise(DateOnly date)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
               
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitorTransactiondata = await context.VisitorTransactions.Where(v => v.Date == date).ToListAsync();
                return visitorTransactiondata;
            }
        }

        public async Task<List<VisitorTransactionModel>> GetVisitorTransactionsDateRangeWise(DateOnly FromDate, DateOnly ToDate)
        {
            using (var scope = _scopeFactory.CreateScope())
            {

                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitorTransactiondata = await context.VisitorTransactions.Where(v => v.Date >= FromDate && v.Date <= ToDate).ToListAsync();
                return visitorTransactiondata;
            }
        }



        public async Task<VisitorMasterModel> GetExistingDataAsync(string fname, string lname, string mobNo)
        {
            return await _context.VisitorMaster.FirstOrDefaultAsync(x => x.VisitorId == (fname.ToUpper() + lname.ToUpper() + mobNo) || x.MobileNo == mobNo);
        }

        public async Task<VisitorMasterModel> GetExistingDataAsyncMob(string mobNo)
        {
            return await _context.VisitorMaster.FirstOrDefaultAsync(x => x.MobileNo == mobNo);
        }


        public async Task<VisitingDetailsModel> GetExistingScheduleToday(string vId, DateOnly todayDate)
        {
            return await _context.VisitDetails.FirstOrDefaultAsync(x => x.VisitorId == vId && x.VisitDate == todayDate);
        }
        public async Task<VisitingDetailsModel> GetExistingSchedule(string vId, DateOnly visitDate, DateOnly toDate)
        {
            return await _context.VisitDetails
                .Where(x => x.VisitorId == vId && x.VisitDate >= visitDate && x.VisitDate <= toDate).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }
        public async Task<VisitingDetailsModel> GetExistingScheduleWithTime(string vId, DateOnly visitDate, DateOnly toDate)
        {
            //return await _context.VisitDetails
            //    .FirstOrDefaultAsync(x => x.VisitorId == vId && x.VisitDate >= visitDate && x.VisitDate <= toDate);

            return await _context.VisitDetails
            .Where(x => x.VisitorId == vId && x.VisitDate >= visitDate && x.VisitDate <= toDate)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();
        }

       
        public async Task<VisitingDetailsModel> GetExistingScheduleWithTimeCondition(
        string vId,
        DateOnly visitDate,
        DateOnly toDate,
        string time)
        {
         
            TimeSpan visitTime = TimeSpan.Parse(time);

           
            //TimeSpan currentTime = DateTime.Now.TimeOfDay;

            //// If today is between visitDate and toDate, check if the time is also greater than or equal to the current time
            //if (visitDate <= DateOnly.FromDateTime(DateTime.Now.Date) && toDate >= DateOnly.FromDateTime(DateTime.Now.Date))
            //{
            //    if (visitTime < currentTime)
            //    {
                   
            //        return null;
            //    }
            //}

            try
            {
                var result = await _context.VisitDetails
               .Where(x =>
                   x.VisitorId == vId &&
                   x.VisitDate >= visitDate &&
                   x.VisitDate <= toDate)
               .OrderByDescending(x => x.Id)
               .FirstOrDefaultAsync();

                if(result != null)
                {
                    TimeSpan visitTimeRes = TimeSpan.Parse(result.VisitTime);
                    TimeSpan visitTimeOnlyHoursAndMinutes = new TimeSpan(visitTimeRes.Hours, visitTimeRes.Minutes, 0);
                    TimeSpan visitTimeOnlyHoursAndMinutesToCompare = new TimeSpan(visitTime.Hours, visitTime.Minutes, 0);


                    if (visitTimeOnlyHoursAndMinutes < (visitTimeOnlyHoursAndMinutesToCompare + TimeSpan.FromMinutes(2)))
                    {
                        return null;
                    }
                    else {
                        return result;
                    }
                }
                else
                {
                    return result;
                }
               


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
           
        }



        public async Task<VisitorMasterModel> GetVisitorInApprove(string vid)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                return await _context.VisitorMaster.FirstOrDefaultAsync(x => x.VisitorId == vid);
            }   
           
        }


        //public async Task<VisitorMasterModel> RegisterVisitorData(VisitorMasterModel vData)
        //{

        //   _context.VisitorMaster.Add(vData);


        //    await _context.SaveChangesAsync();

        //    return vData;
        //}
        public async Task<VisitorMasterModel> RegisterVisitorData(VisitorMasterModel vData)
        {
            // Check if a visitor with the provided mobile number already exists
            var existingVisitor = await _context.VisitorMaster
                .FirstOrDefaultAsync(v => v.MobileNo == vData.MobileNo);

            if (existingVisitor != null)
            {
            }
            else
            {
                // If the visitor does not exist, add the new visitor
                _context.VisitorMaster.Add(vData);
            }

            await _context.SaveChangesAsync();

            return existingVisitor ?? vData; // Return the updated or newly added visitor
        }
        public async Task<VisitingDetailsModel> RegisterVisitingDetails(VisitingDetailsModel vDetails)
        {

            string visitorId = await _context.VisitorMaster
                .OrderByDescending(v => v.VisitorId)
                .Select(v => v.VisitorId)
                .FirstOrDefaultAsync();

            vDetails.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);
            //vDetails.VisitorId = visitorId;
            _context.VisitDetails.Add(vDetails);


            await _context.SaveChangesAsync();

            return vDetails;
        }
        public async Task<VisitingDetailsModel> UpdateVisitingDetails(VisitingDetailsModel vDetails)
        {
            // Find the existing record in the database using its ID
            var existingDetails = await _context.VisitDetails.FindAsync(vDetails.Id);

            if (existingDetails == null)
            {
                // Handle the case where the record does not exist
                throw new Exception("Visiting details not found.");
            }

            // Update the properties of the existing record
            existingDetails.TypeId = vDetails.TypeId; // Example property
            existingDetails.CountryId = vDetails.CountryId; 
            existingDetails.CompanySite = vDetails.CompanySite; 
            existingDetails.CompanyDepartment = vDetails.CompanyDepartment; 
            existingDetails.ApprovedBy = vDetails.ApprovedBy; 
            existingDetails.Duration = vDetails.Duration; 
            existingDetails.Purpose = vDetails.Purpose; 
            existingDetails.VisitDate = vDetails.VisitDate; 
            existingDetails.ToDate = vDetails.ToDate; 
            existingDetails.VisitArea = vDetails.VisitArea; 
            existingDetails.Approval = vDetails.Approval; 
            existingDetails.SecurityApproval = vDetails.SecurityApproval; 


            existingDetails.RegistrationDate = vDetails.RegistrationDate; // Example property
                                                                          // Update other properties as needed

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return existingDetails; // Return the updated entity
        }


        public async Task AddRangeAsync(List<CarriedMaterialModel> carriedMaterials, string visitorId)
        {
            /*int visitorId = await _context.VisitorMaster
                .OrderByDescending(v => v.Id)
                .Select(v => v.Id)
                .FirstOrDefaultAsync();*/
            
            //_context.CarriedMaterial_ByVisitor.AddRange(carriedMaterials);
            //await _context.SaveChangesAsync();


            foreach (var carriedMaterial in carriedMaterials)
            {
                // Set the Id property to null or 0 to let Entity Framework generate a new value
                //carriedMaterial.Id = null; // or 0 if it's an integer
                carriedMaterial.VisitorId = visitorId;
                carriedMaterial.VisitDate = DateOnly.FromDateTime(DateTime.Now);
                carriedMaterial.DateTime = DateTime.Now;
                _context.CarriedMaterial_ByVisitor.Add(carriedMaterial);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<List<CarriedMaterialModel>> GetCarriedMtrlData()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var mtrldata = await context.CarriedMaterial_ByVisitor.ToListAsync();
                return mtrldata;
            }
        }


        /*public async Task<List<EmpData>> GetEmpData()
        {
            var empdata = await _context.Employee.ToListAsync();
            return empdata;
        }*/
        public async Task<List<EmpData>> GetEmpData()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var empdata = await context.Employee.ToListAsync();
                return empdata;
            }
        }

        public async Task<EmpData> GetEmpDataById(int id)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var empdata = await context.Employee.FindAsync(id);
                return empdata;
            }
        }

        /*
        public async Task<List<DeptData>> GetDeptData()
        {
            var deptdata = await _context.Department.ToListAsync();
            return deptdata;
        }*/
        public async Task<List<DeptData>> GetDeptData()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var deptdata = await context.Department.ToListAsync();
                return deptdata;
            }
        }
        public async Task<List<CountryModel>> GetCountryList()
        {
            var countries = await _context.Country.ToListAsync();
            return countries;
        }
        public async Task<List<CompanySiteModel>> GetCompanySite()
        {
            var comSite = await _context.SiteMaster.ToListAsync();
            return comSite;
        }
        public async Task<List<VTypeModel>> GetVisitorType()
        {
            var vType = await _context.VisitorType.ToListAsync();
            return vType;
        }

        public async Task UpdateVisitingDetailsDataByID(int id)
        {
           var updateVisitor = await _context.VisitDetails.FindAsync(id);
            if (updateVisitor != null)
            {
                updateVisitor.Approval = "Approved";   ///update
                await _context.SaveChangesAsync();
                
            }
            
        }
        public async Task CancellationApproval(int id)
        {
            var updateVisitor = await _context.VisitDetails.FindAsync(id);
            if (updateVisitor != null)
            {
                updateVisitor.Approval = "Cancelled";   ///update
                await _context.SaveChangesAsync();

            }

        }

        public async Task UpdateVisitorCheckin(int id)
        {
            var updateVisitor = await _context.VisitorMaster.FindAsync(id);
            if (updateVisitor != null)
            {
                //updateVisitor.Checkin = DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss");    ///update
                //updateVisitor.Checkin = DateTime.Now.ToShortTimeString();
                await _context.SaveChangesAsync();

            }

        }

        public async Task<VisitorTransactionModel> InsertVisitorTransactions(string vId, DateOnly todayDate, int vdIdfk)
        {


            VisitorTransactionModel vTransaction = new VisitorTransactionModel
            {
                VisitorId = vId,
                Date = todayDate,
                CheckIn = DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss"),
                VDetails_Id = vdIdfk,

            };
            _context.VisitorTransactions.Add(vTransaction);


            await _context.SaveChangesAsync();

            return vTransaction;
        }
        //public async Task<FavVisitorsModel> AddNewFavourite(string vId, int empId, bool f)
        //{


        //    FavVisitorsModel vFav = new FavVisitorsModel
        //    {
        //        VisitorId = vId,
        //        EmpId = empId,
        //        Favorite = f

        //    };
        //    _context.FavVisitors.Add(vFav);


        //    await _context.SaveChangesAsync();

        //    return vFav;
        //}
        public async Task<FavVisitorsModel> AddNewFavourite(string vId, int empId, bool f)
        {
            // First, check if there's an existing record for the given vId and empId
            var existingRecord = _context.FavVisitors
                .Where(x => x.VisitorId == vId && x.EmpId == empId)
                .FirstOrDefault();

            FavVisitorsModel vFav; // Declare vFav here

            if (existingRecord != null)
            {
                // If a record exists, toggle the Favorite property
                existingRecord.Favorite = !existingRecord.Favorite;
                vFav = existingRecord; // Assign existingRecord to vFav
            }
            else
            {
                // If no record exists, create a new one
                vFav = new FavVisitorsModel
                {
                    VisitorId = vId,
                    EmpId = empId,
                    Favorite = f
                };
                _context.FavVisitors.Add(vFav);
            }

            await _context.SaveChangesAsync();

            // Return the updated or newly created record
            return vFav;
        }

        public async Task UpdateVisitorTransactionCheckout(int id)
        {
            
            var updateVisitor = await _context.VisitorTransactions.FindAsync(id);
            if (updateVisitor != null)
            {
                //updateVisitor.CheckOut = DateTime.Now.ToShortTimeString();
                updateVisitor.CheckOut = DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss");   ////update
                await _context.SaveChangesAsync();

            } 
        }
        
        public async Task UpdateSafetStatus(string vid, DateOnly vDate, DateOnly tDate, int id)
        {
            //var updateVisitor = await _context.VisitDetails.FindAsync(vid, vDate);

            var todayDate = DateOnly.FromDateTime(DateTime.Now);
            var updateVisitor = await _context.VisitDetails.FirstOrDefaultAsync(v => v.VisitorId == vid && v.VisitDate == vDate && v.ToDate == tDate && v.VisitDate <= todayDate && v.ToDate >= todayDate && v.Id == id); 

            if (updateVisitor != null)
            {
                updateVisitor.SafetyStatus = "Checked";
                await _context.SaveChangesAsync();
            }
        }


        public async Task UpdateSecurityApproval(string vid, DateOnly vDate, DateOnly tDate, int id)
        {
            //var updateVisitor = await _context.VisitDetails.FindAsync(vid, vDate);

            var todayDate = DateOnly.FromDateTime(DateTime.Now);
            var updateVisitor = await _context.VisitDetails.FirstOrDefaultAsync(v => v.VisitorId == vid && v.VisitDate == vDate && v.ToDate == tDate && v.VisitDate <= todayDate && v.ToDate >= todayDate && v.Id == id);

            if (updateVisitor != null)
            {
                updateVisitor.SecurityApproval = "Yes";
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<VisitorMasterModel>> SearchVisitors(string searchText)
        {

            return await _context.VisitorMaster
                .Where(v => v.FirstName.Contains(searchText) || v.LastName.Contains(searchText) || v.IdProofNo.Contains(searchText) || v.MobileNo.Contains(searchText) || v.Email.Contains(searchText))
                .ToListAsync();
        }

        public async Task<VisitorMasterModel> SearchVisitorById(string visitorId)
        {
            // return await _context.VisitorMaster.FirstOrDefaultAsync(v => v.VisitorId == visitorId || v.IdProofNo.Trim().ToLower() == visitorId.Trim().ToLower());
            return await _context.VisitorMaster
                .Where(v => v.VisitorId == visitorId || v.IdProofNo.Trim().ToLower() == visitorId.Trim().ToLower())
                .OrderByDescending(v => v.Id) 
                .FirstOrDefaultAsync();
        }
        public async Task<VisitorMasterModel> SearchVisitorByMob(string mob)
        {
           // return await _context.VisitorMaster.FirstOrDefaultAsync(v => v.MobileNo == mob);

            return await _context.VisitorMaster
            .Where(v => v.MobileNo == mob)
            .OrderByDescending(v => v.Id) 
            .FirstOrDefaultAsync();
        }



        public async Task UpdateVisitorPhoto(int id,  byte[]? ImageData)
        {
            var updateVisitor = await _context.VisitorMaster.FindAsync(id);
            if (updateVisitor != null)
            {
                updateVisitor.ImageData = ImageData;
                await _context.SaveChangesAsync();

            }

        }

        public async Task UpdatePassword(string email, string pass)
        {
            //var updateVisitor = await _context.VisitDetails.FindAsync(vid, vDate);


            var updatePass = await _context.Employee.FirstOrDefaultAsync(x => x.EmpEmail.ToLower() == email);

            if (updatePass != null)
            {
                updatePass.Password = pass;
                await _context.SaveChangesAsync();
            }
        }



        public async Task<EmpData> AddNewEmpData(EmpData empData)
        {

            _context.Employee.Add(empData);

            await _context.SaveChangesAsync();

            return empData;
        }

        public async Task<EmpData> UpdateEmpData(EmpData empData, string email)
        {
            var emp = await _context.Employee.FirstOrDefaultAsync(x => x.EmpEmail.ToLower() == email.ToLower());
            // Update the properties of the existing record
            if(emp != null)
            {
                emp.EmpName = empData.EmpName;
                emp.EmpEmail = empData.EmpEmail;
                emp.KO = empData.KO;

                emp.EmpContactNo = empData.EmpContactNo;
                emp.DeptId = empData.DeptId;
                emp.SiteId = empData.SiteId;
                empData.Password = empData.Password;
                empData.Role = empData.Role;
            }
           
                                                         

            // Save changes to the database
            await _context.SaveChangesAsync();

            return emp; // Return the updated record
        }


        public async Task<EmpData> GetExistingEmp(string eMail)
        {
            return await _context.Employee.FirstOrDefaultAsync(x => x.EmpEmail.ToLower() == eMail.ToLower());
        }


        public async Task<List<VisitorTransactionModel>> GetVisitorTransactionsLog(string vId, DateOnly todayDate)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
              
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitorTransactiondata = await context.VisitorTransactions.Where(x => x.VisitorId == vId && x.Date == todayDate).ToListAsync();
                return visitorTransactiondata;
            }
        }

        public async Task<VisitorTransactionModel> GetExistingScheduleWithCheckout(string vId, DateOnly visitDate, DateOnly toDate)
        {
            using (var scope = _scopeFactory.CreateScope())
            {

                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitorTransactiondata = await context.VisitorTransactions.Where(x => x.VisitorId == vId && x.Date >= visitDate && visitDate <= toDate).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                return visitorTransactiondata;
            }
        }

        public async Task<VisitorTransactionModel> GetExistingScheduleTransaction(int IdV)
        {
            using (var scope = _scopeFactory.CreateScope())
            {

                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitorTransactiondata = await context.VisitorTransactions.Where(x => x.VDetails_Id == IdV).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                return visitorTransactiondata;
            }
        }

        public async Task<List<VisitorTransactionModel>> GetVisitorTransactionsLogID(int id, string vId, DateOnly todayDate)
        {
            using (var scope = _scopeFactory.CreateScope())
            {

                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var visitorTransactiondata = await context.VisitorTransactions.Where(x => x.VDetails_Id == id && x.VisitorId == vId && x.Date == todayDate).ToListAsync();
                return visitorTransactiondata;
            }
        }


        public async Task<bool> DeleteVisitorData(int id)
        {
            // Find the visitor record by ID
            var visitorDet = await _context.VisitDetails.FindAsync(id);

            // Check if the visitor exists
            if (visitorDet == null)
            {
                return false; // Record not found
            }

            // Remove the visitor record from the context
            _context.VisitDetails.Remove(visitorDet);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return true; // Record deleted successfully
        }



        public async Task<VisitorMasterModel> UpdateVisitorDetails(VisitorMasterModel vDetail)
        {
            // Find the existing record in the database using its ID
            var existingDetails = await _context.VisitorMaster.FindAsync(vDetail.Id);

            if (existingDetails == null)
            {
                // Handle the case where the record does not exist
                throw new Exception("Visitor details not found.");
            }

          
            
            
            existingDetails.FirstName = vDetail.FirstName;
            existingDetails.LastName = vDetail.LastName;
            existingDetails.Company = vDetail.Company;
            existingDetails.Email = vDetail.Email;
            existingDetails.MobileNo = vDetail.MobileNo;
            existingDetails.IdProof = vDetail.IdProof;
            existingDetails.IdProofNo = vDetail.IdProofNo;
            existingDetails.Gender = vDetail.Gender;
            existingDetails.Address = vDetail.Address;

            existingDetails.VisitorId = vDetail.FirstName.ToUpper().Trim().Replace(" ", "") + vDetail.LastName.ToUpper().Trim().Replace(" ", "") + vDetail.MobileNo ?? "".ToString().Trim();




            // Save the changes to the database
            await _context.SaveChangesAsync();

            return existingDetails; // Return the updated entity
        }
    }
}
