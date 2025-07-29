using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ServiceReportSystem;

namespace ServiceReportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServiceTypeWarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceTypeWarehouse>>> GetServiceTypeWarehouses()
        {
            return await _context.ServiceTypeWarehouses
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }
    }
}