using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ServiceReportSystem;

namespace ControlTower.Controllers.ServiceReportSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowupActionWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FollowupActionWarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FollowupActionWarehouse>>> GetFollowupActionWarehouses()
        {
            return await _context.FollowupActionWarehouses
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }
    }
}