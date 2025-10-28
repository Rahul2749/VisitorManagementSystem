using VisitorManagementSystem.Data;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.Models.CarriedMtrlModel;

namespace VisitorManagementSystem.Services
{
    public interface IVDataService
    {
        Task<List<VisitorMasterModel>> GetAllVisitorData();
        Task<VisitorMasterModel> GetVisitorDataById(string vid);
        Task<VisitorMasterModel> SearchVisitorByMob(string mob);
        Task<List<FavVisitorsModel>> GetFavVisitors();
        Task<List<VisitingDetailsModel>> GetAllVisitingDetails();
        Task<VisitingDetailsModel> GetVDetailsByID(string vid);
        Task<VisitingDetailsModel> GetFavVisitorDetailsByVIdEmp(string vid, string emp);
        Task<List<VisitingDetailsModel>> GetDateWiseVisitingDetails(DateOnly date);
        Task<List<VisitingDetailsModel>> GetDateWiseVisitingDetailsReport(DateOnly date);
        Task<List<VisitingDetailsModel>> GetDateWiseVisitingDetailsReportNoTransactions(DateOnly date);
        Task<List<VisitingDetailsModel>> GetAllVisitingDetailsMonthlyReport();
        Task<List<VisitingDetailsModel>> GetAllVisitingDetailsYearlyReport();
        Task<List<VisitingDetailsModel>> GetVisitingDetailsOnlyToday();

        Task<List<VisitingDetailsModel>> GetDateWiseVisitingDetailsWeekly();
        Task<List<VisitingDetailsModel>> GetDateWiseVisitingDetailsWeekly7DaysAhead();
        Task<List<VisitorTransactionModel>> GetAllVisitorTransactions();
        Task<List<VisitorTransactionModel>> GetVisitorTransactionsMonthWise();
        Task<List<VisitorTransactionModel>> GetVisitorTransactionsYearWise();
        Task<List<VisitorTransactionModel>> GetVisitorTransactionsOnlyToday();
        Task<List<VisitorTransactionModel>> GetVisitorTransactionsDateWise(DateOnly date);
        Task UpdateVisitingDetailsDataByID(int id);
        Task CancellationApproval(int id);
        Task UpdateVisitorCheckin(int id);
        Task UpdateSafetStatus(string vid, DateOnly vDate, DateOnly tDate, int id);
        Task UpdateSecurityApproval(string vid, DateOnly vDate, DateOnly tDate, int id);
        

        Task<VisitorMasterModel> GetExistingDataAsync(string fname, string lname, string mobNo);
        Task<VisitorMasterModel> GetExistingDataAsyncMob(string mobNo);
        Task<VisitingDetailsModel> GetExistingScheduleToday(string vId, DateOnly todayDate);
        Task<VisitingDetailsModel> GetExistingSchedule(string vId, DateOnly visitDate, DateOnly toDate);
        Task<VisitingDetailsModel> GetExistingScheduleWithTime(string vId, DateOnly visitDate, DateOnly toDate);
        Task<VisitingDetailsModel> GetExistingScheduleWithTimeCondition(
        string vId,
        DateOnly visitDate,
        DateOnly toDate,
        string time);
        Task<VisitorMasterModel> GetVisitorInApprove(string vid);
        Task<VisitorMasterModel> RegisterVisitorData(VisitorMasterModel vData);
        Task<VisitingDetailsModel> RegisterVisitingDetails(VisitingDetailsModel vDetails);
        Task<VisitingDetailsModel> UpdateVisitingDetails(VisitingDetailsModel vDetails);
        Task<VisitorTransactionModel> InsertVisitorTransactions(string vId, DateOnly todayDate, int vdIdfk);
        Task<FavVisitorsModel> AddNewFavourite(string vId, int empId, bool f);
        Task UpdateVisitorTransactionCheckout(int id);


        Task AddRangeAsync(List<CarriedMaterialModel> carriedMaterials, string visitorId);
        Task<List<CarriedMaterialModel>> GetCarriedMtrlData();
        Task<List<EmpData>> GetEmpData();
        Task<List<DeptData>> GetDeptData();
        Task<List<CountryModel>> GetCountryList();
        Task<List<CompanySiteModel>> GetCompanySite();
        Task<List<VTypeModel>> GetVisitorType();


        Task<List<VisitorMasterModel>> SearchVisitors(string searchText);
        Task<VisitorMasterModel> SearchVisitorById(string visitorId);

        Task UpdateVisitorPhoto(int id, byte[]? ImageData);
        Task UpdatePassword(string email, string pass);

        Task<EmpData> AddNewEmpData(EmpData eData);
        Task<EmpData> GetExistingEmp(string eMail);
        Task<EmpData> UpdateEmpData(EmpData empData, string email);

        Task<List<VisitorTransactionModel>> GetVisitorTransactionsLog(string vId, DateOnly todayDate);
        Task<List<VisitorTransactionModel>> GetVisitorTransactionsLogID(int id, string vId, DateOnly todayDate);

        Task<VisitorTransactionModel> GetExistingScheduleTransaction(int IdV);

        Task<VisitorTransactionModel> GetExistingScheduleWithCheckout(string vId, DateOnly visitDate, DateOnly toDate);

        Task<List<VisitingDetailsModel>> GetDateRangeWiseVisitingDetailsReport(DateOnly FromDate, DateOnly ToDate);
        Task<List<VisitorTransactionModel>> GetVisitorTransactionsDateRangeWise(DateOnly FromDate, DateOnly ToDate);

        Task<bool> DeleteVisitorData(int id);
        Task<VisitorMasterModel> UpdateVisitorDetails(VisitorMasterModel vDetail);

        Task<EmpData> GetEmpDataById(int id);

    }
}
