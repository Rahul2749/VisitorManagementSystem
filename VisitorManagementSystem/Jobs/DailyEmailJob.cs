using Quartz;
using VisitorManagementSystem.Services;

namespace VisitorManagementSystem.Jobs
{
    public class DailyEmailJob : IJob
    {
       // private readonly ExcelService _excelService;
        private readonly EmailService _emailService;

        public DailyEmailJob(EmailService emailService)
        {
           // _excelService = excelService;
            _emailService = emailService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            //var data = GetTableData();
            //var htmlTable = 

            await _emailService.SendEmailAsync();
        }

    }
}
