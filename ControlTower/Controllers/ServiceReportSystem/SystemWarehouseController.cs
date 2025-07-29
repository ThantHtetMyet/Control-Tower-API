using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ServiceReportSystem;

namespace ServiceReportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SystemWarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SystemWarehouse>>> GetSystemWarehouses()
        {
            return await _context.SystemWarehouses
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }
    }
}