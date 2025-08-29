using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.NewsPortalSystem;
using ControlTower.DTOs.NewsPortalSystem;
using ControlTower.DTOs;
using ControlTower.Attributes; // Add this import

namespace ControlTower.Controllers.NewsPortalSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NewsCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/NewsCategory
        [HttpGet]
        [NewsPortalAuthorization("User")] // Allow users to read categories
        public async Task<ActionResult<IEnumerable<NewsCategoryListDto>>> GetNewsCategory()
        {
            var categories = await _context.NewsCategory
                .Include(c => c.ParentCategory)
                .Include(c => c.SubCategories.Where(sc => !sc.IsDeleted))
                .Include(c => c.News.Where(n => !n.IsDeleted))
                .Where(c => !c.IsDeleted)
                .Select(c => new NewsCategoryListDto
                {
                    ID = c.ID,
                    Name = c.Name,
                    Slug = c.Slug,
                    Description = c.Description,
                    ParentCategoryName = c.ParentCategory != null ? c.ParentCategory.Name : null,
                    ParentCategoryID = c.ParentCategoryID != null ? c.ParentCategoryID : null,
                    CreatedDate = c.CreatedDate,
                    NewsCount = c.News.Count(n => !n.IsDeleted),
                    SubCategoriesCount = c.SubCategories.Count(sc => !sc.IsDeleted)
                })
                .OrderBy(c => c.Name)
                .ToListAsync();

            return categories;
        }

        // GET: api/NewsCategory/tree
        [HttpGet("tree")]
        [NewsPortalAuthorization("User")] // Allow users to read category tree
        public async Task<ActionResult<IEnumerable<NewsCategoryDto>>> GetNewsCategoryTree()
        {
            var categories = await _context.NewsCategory
                .Include(c => c.CreatedByUser)
                .Include(c => c.UpdatedByUser)
                .Include(c => c.SubCategories.Where(sc => !sc.IsDeleted))
                .Include(c => c.News.Where(n => !n.IsDeleted))
                .Where(c => !c.IsDeleted && c.ParentCategoryID == null)
                .Select(c => new NewsCategoryDto
                {
                    ID = c.ID,
                    Name = c.Name,
                    Slug = c.Slug,
                    Description = c.Description,
                    ParentCategoryID = c.ParentCategoryID,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    CreatedByUserName = c.CreatedByUser != null ? c.CreatedByUser.FirstName + " " + c.CreatedByUser.LastName : null,
                    UpdatedByUserName = c.UpdatedByUser != null ? c.UpdatedByUser.FirstName + " " + c.UpdatedByUser.LastName : null,
                    NewsCount = c.News.Count(n => !n.IsDeleted),
                    SubCategoriesCount = c.SubCategories.Count(sc => !sc.IsDeleted),
                    SubCategories = c.SubCategories.Where(sc => !sc.IsDeleted).Select(sc => new NewsCategoryDto
                    {
                        ID = sc.ID,
                        Name = sc.Name,
                        Slug = sc.Slug,
                        Description = sc.Description,
                        ParentCategoryID = sc.ParentCategoryID,
                        CreatedDate = sc.CreatedDate,
                        UpdatedDate = sc.UpdatedDate,
                        NewsCount = sc.News.Count(n => !n.IsDeleted),
                        SubCategoriesCount = sc.SubCategories.Count(ssc => !ssc.IsDeleted)
                    }).ToList()
                })
                .OrderBy(c => c.Name)
                .ToListAsync();

            return categories;
        }

        // GET: api/NewsCategory/5
        [HttpGet("{id}")]
        [NewsPortalAuthorization("User")] // Allow users to read individual categories
        public async Task<ActionResult<NewsCategoryDto>> GetNewsCategory(Guid id)
        {
            var category = await _context.NewsCategory
                .Include(c => c.ParentCategory)
                .Include(c => c.CreatedByUser)
                .Include(c => c.UpdatedByUser)
                .Include(c => c.SubCategories.Where(sc => !sc.IsDeleted))
                .Include(c => c.News.Where(n => !n.IsDeleted))
                .Where(c => c.ID == id && !c.IsDeleted)
                .Select(c => new NewsCategoryDto
                {
                    ID = c.ID,
                    Name = c.Name,
                    Slug = c.Slug,
                    Description = c.Description,
                    ParentCategoryID = c.ParentCategoryID,
                    ParentCategoryName = c.ParentCategory != null ? c.ParentCategory.Name : null,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    CreatedByUserName = c.CreatedByUser != null ? c.CreatedByUser.FirstName + " " + c.CreatedByUser.LastName : null,
                    UpdatedByUserName = c.UpdatedByUser != null ? c.UpdatedByUser.FirstName + " " + c.UpdatedByUser.LastName : null,
                    NewsCount = c.News.Count(n => !n.IsDeleted),
                    SubCategoriesCount = c.SubCategories.Count(sc => !sc.IsDeleted),
                    SubCategories = c.SubCategories.Where(sc => !sc.IsDeleted).Select(sc => new NewsCategoryDto
                    {
                        ID = sc.ID,
                        Name = sc.Name,
                        Slug = sc.Slug,
                        Description = sc.Description,
                        ParentCategoryID = sc.ParentCategoryID,
                        CreatedDate = sc.CreatedDate,
                        UpdatedDate = sc.UpdatedDate,
                        NewsCount = sc.News.Count(n => !n.IsDeleted),
                        SubCategoriesCount = sc.SubCategories.Count(ssc => !ssc.IsDeleted)
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // POST: api/NewsCategory
        [HttpPost]
        [ApplicationAuthorization("News Portal System", "Admin")] // Only admins can create
        public async Task<ActionResult<NewsCategoryDto>> PostNewsCategory(CreateNewsCategoryDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if parent category exists (if provided)
            if (createDto.ParentCategoryID.HasValue)
            {
                var parentExists = await _context.NewsCategory.AnyAsync(c => c.ID == createDto.ParentCategoryID.Value && !c.IsDeleted);
                if (!parentExists)
                {
                    return BadRequest("Invalid parent category ID");
                }
            }

            // Check if slug is unique
            var slugExists = await _context.NewsCategory.AnyAsync(c => c.Slug == createDto.Slug && !c.IsDeleted);
            if (slugExists)
            {
                return BadRequest("Slug already exists");
            }

            var category = new NewsCategory
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                Slug = createDto.Slug,
                Description = createDto.Description,
                ParentCategoryID = createDto.ParentCategoryID,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = Guid.Parse(createDto.CreatedBy),
                UpdatedBy = Guid.Parse(createDto.CreatedBy)
            };

            _context.NewsCategory.Add(category);
            await _context.SaveChangesAsync();

            return await GetNewsCategory(category.ID);
        }

        // PUT: api/NewsCategory/5
        [HttpPut("{id}")]
        [ApplicationAuthorization("News Portal System", "Admin")] // Only admins can update
        public async Task<IActionResult> PutNewsCategory(Guid id, UpdateNewsCategoryDto updateDto)
        {
            if (id != updateDto.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.NewsCategory.FindAsync(id);
            if (category == null || category.IsDeleted)
            {
                return NotFound();
            }

            // Check if parent category exists (if provided) and prevent circular reference
            if (updateDto.ParentCategoryID.HasValue)
            {
                if (updateDto.ParentCategoryID.Value == id)
                {
                    return BadRequest("Category cannot be its own parent");
                }

                var parentExists = await _context.NewsCategory.AnyAsync(c => c.ID == updateDto.ParentCategoryID.Value && !c.IsDeleted);
                if (!parentExists)
                {
                    return BadRequest("Invalid parent category ID");
                }

                // Check for circular reference
                var isCircular = await IsCircularReference(id, updateDto.ParentCategoryID.Value);
                if (isCircular)
                {
                    return BadRequest("Circular reference detected");
                }
            }

            // Check if slug is unique (excluding current category)
            var slugExists = await _context.NewsCategory.AnyAsync(c => c.Slug == updateDto.Slug && c.ID != id && !c.IsDeleted);
            if (slugExists)
            {
                return BadRequest("Slug already exists");
            }

            category.Name = updateDto.Name;
            category.Slug = updateDto.Slug;
            category.Description = updateDto.Description;
            category.ParentCategoryID = updateDto.ParentCategoryID;
            category.UpdatedDate = DateTime.UtcNow;
            category.UpdatedBy = !string.IsNullOrEmpty(updateDto.UpdatedBy) ? Guid.Parse(updateDto.UpdatedBy) : null;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/NewsCategory/5
        [HttpDelete("{id}")]
        [ApplicationAuthorization("News Portal System", "Admin")] // Only admins can delete
        public async Task<IActionResult> DeleteNewsCategory(Guid id)
        {
            var category = await _context.NewsCategory
                .Include(c => c.News.Where(n => !n.IsDeleted))
                .Include(c => c.SubCategories.Where(sc => !sc.IsDeleted))
                .FirstOrDefaultAsync(c => c.ID == id && !c.IsDeleted);

            if (category == null)
            {
                return NotFound();
            }

            // Check if category has news or subcategories
            if (category.News.Any() || category.SubCategories.Any())
            {
                return BadRequest("Cannot delete category that contains news or subcategories");
            }

            category.IsDeleted = true;
            category.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> IsCircularReference(Guid categoryId, Guid parentId)
        {
            var currentParentId = (Guid?)parentId; // Make it nullable from the start
            while (currentParentId.HasValue)
            {
                if (currentParentId.Value == categoryId)
                {
                    return true;
                }

                var parent = await _context.NewsCategory
                    .Where(c => c.ID == currentParentId.Value && !c.IsDeleted)
                    .FirstOrDefaultAsync();

                currentParentId = parent?.ParentCategoryID;
            }
            return false;
        }

        private bool NewsCategoryExists(Guid id)
        {
            return _context.NewsCategory.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}