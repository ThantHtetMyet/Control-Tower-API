using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models;
using ControlTower.Models.ServiceReportSystem;

namespace ControlTower.Controllers.ServiceReportSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionTakenWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ActionTakenWarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActionTakenWarehouse>>> GetActionTakenWarehouses()
        {
            /*
            return await _context.ActionTakenWarehouses
                .Where(x => !x.IsDeleted)
                .ToListAsync();
            */
            return null;
        }
    }
}