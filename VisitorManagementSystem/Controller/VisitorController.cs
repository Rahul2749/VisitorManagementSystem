using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.Services;

namespace VisitorManagementSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {

        private IVDataService _dataService;
        public VisitorController(IVDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("Search/{searchText}")]
        public async Task<ActionResult<List<VisitorMasterModel>>> SearchVisitors(string searchText)
        {
            return Ok(await _dataService.SearchVisitors(searchText));
        }
    }
}





