using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ServiceReportSystem;

namespace ServiceReportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueFoundWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IssueFoundWarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueFoundWarehouse>>> GetIssueFoundWarehouses()
        {
            return await _context.IssueFoundWarehouses
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }
    }
}