using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ServiceReportSystem;

namespace ServiceReportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LocationWarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationWarehouse>>> GetLocationWarehouses()
        {
            return await _context.LocationWarehouses
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }
    }
}