using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ServiceReportSystem;

namespace ControlTower.Controllers.ServiceReportSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormStatusWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FormStatusWarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormStatusWarehouses>>> GetFormStatusWarehouses()
        {
            return await _context.FormStatusWarehouses
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }
    }
}