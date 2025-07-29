using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ServiceReportSystem;

namespace ServiceReportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurtherActionTakenWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FurtherActionTakenWarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FurtherActionTakenWarehouse>>> GetFurtherActionTakenWarehouses()
        {
            return await _context.FurtherActionTakenWarehouses
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }
    }
}