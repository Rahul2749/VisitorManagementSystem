using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorManagementSystem.Data;
using Microsoft.EntityFrameworkCore; // Add this line for ToListAsync
using DocumentFormat.OpenXml.InkML;

namespace VisitorManagementSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowDataController : ControllerBase
    {
        private readonly VMSDbContext _context;

        public ShowDataController(VMSDbContext context) // Constructor to inject the DbContext
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<VisitorMasterModel>>> Get()
        {
            await Task.Delay(500);
            var vData = await _context.VisitorMaster.ToListAsync();
            return Ok(vData);
        }
    }
}