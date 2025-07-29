using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ServiceReportSystem;

namespace ServiceReportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectNoWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectNoWarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectNoWarehouse>>> GetProjectNoWarehouses()
        {
            return await _context.ProjectNoWarehouses
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        [HttpGet("projectnumber/{projectNumber}")]
        public async Task<ActionResult<ProjectNoWarehouse>> GetProjectNoByProjectNumber(string projectNumber)
        {
            var project = await _context.ProjectNoWarehouses
                .Where(x => !x.IsDeleted && x.ProjectNumber == projectNumber)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }
    }
}