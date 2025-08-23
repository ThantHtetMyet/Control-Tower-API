using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.NewsPortalSystem;
using ControlTower.DTOs.NewsPortalSystem;
using ControlTower.DTOs;

namespace ControlTower.Controllers.NewsPortalSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public NewsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/News
        [HttpGet]
        public async Task<ActionResult<PagedResult<NewsListDto>>> GetNews(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] Guid? categoryId = null,
            [FromQuery] bool? isPublished = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var query = _context.News
                .Include(n => n.NewsCategory)
                .Include(n => n.CreatedByUser)
                .Include(n => n.NewsImages.Where(img => !img.IsDeleted))
                .Include(n => n.NewsComments.Where(c => !c.IsDeleted))
                .Include(n => n.NewsReactions.Where(r => !r.IsDeleted))
                .Where(n => !n.IsDeleted);

            // Apply filters
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(n => n.Title.Contains(search) || 
                                        n.Description.Contains(search) ||
                                        n.Excerpt.Contains(search));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(n => n.NewsCategoryID == categoryId.Value);
            }

            if (isPublished.HasValue)
            {
                query = query.Where(n => n.IsPublished == isPublished.Value);
            }

            var totalCount = await query.CountAsync();

            var news = await query
                .OrderByDescending(n => n.UpdatedDate > n.CreatedDate ? n.UpdatedDate : n.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new NewsListDto
                {
                    ID = n.ID,
                    Title = n.Title,
                    Slug = n.Slug,
                    Excerpt = n.Excerpt,
                    CategoryName = n.NewsCategory.Name,
                    ViewCount = n.ViewCount,
                    PublishDate = n.PublishDate,
                    IsPublished = n.IsPublished,
                    CreatedDate = n.CreatedDate,
                    CreatedByUserName = n.CreatedByUser != null ? n.CreatedByUser.FirstName + " " + n.CreatedByUser.LastName : null,
                    CommentsCount = n.NewsComments.Count(c => !c.IsDeleted),
                    ReactionsCount = n.NewsReactions.Count(r => !r.IsDeleted),
                    FeaturedImageUrl = n.NewsImages.FirstOrDefault(img => img.IsFeatured && !img.IsDeleted) != null ? 
                                     n.NewsImages.FirstOrDefault(img => img.IsFeatured && !img.IsDeleted).StoredDirectory + "/" + n.NewsImages.FirstOrDefault(img => img.IsFeatured && !img.IsDeleted).Name : null
                })
                .ToListAsync();

            return new PagedResult<NewsListDto>
            {
                Items = news,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsDto>> GetNews(Guid id)
        {
            var news = await _context.News
                .Include(n => n.NewsCategory)
                .Include(n => n.CreatedByUser)
                .Include(n => n.UpdatedByUser)
                .Include(n => n.NewsImages.Where(img => !img.IsDeleted))
                .Include(n => n.NewsComments.Where(c => !c.IsDeleted))
                .Include(n => n.NewsReactions.Where(r => !r.IsDeleted))
                .Where(n => n.ID == id && !n.IsDeleted)
                .Select(n => new NewsDto
                {
                    ID = n.ID,
                    Title = n.Title,
                    Slug = n.Slug,
                    Description = n.Description,
                    Excerpt = n.Excerpt,
                    Remark = n.Remark,
                    CategoryID = n.NewsCategoryID,
                    CategoryName = n.NewsCategory.Name,
                    Rate = n.Rate,
                    ViewCount = n.ViewCount,
                    PublishDate = n.PublishDate,
                    IsPublished = n.IsPublished,
                    CreatedDate = n.CreatedDate,
                    UpdatedDate = n.UpdatedDate,
                    CreatedByUserName = n.CreatedByUser != null ? n.CreatedByUser.FirstName + " " + n.CreatedByUser.LastName : null,
                    UpdatedByUserName = n.UpdatedByUser != null ? n.UpdatedByUser.FirstName + " " + n.UpdatedByUser.LastName : null,
                    CommentsCount = n.NewsComments.Count(c => !c.IsDeleted),
                    ReactionsCount = n.NewsReactions.Count(r => !r.IsDeleted),
                    ImagesCount = n.NewsImages.Count(img => !img.IsDeleted),
                    Images = n.NewsImages.Where(img => !img.IsDeleted).Select(img => new NewsImageDto
                    {
                        ID = img.ID,
                        NewsID = img.NewsID,
                        Name = img.Name,
                        StoredDirectory = img.StoredDirectory,
                        AltText = img.AltText,
                        Caption = img.Caption,
                        IsFeatured = img.IsFeatured,
                        UploadedDate = img.UploadedDate,
                        ImageUrl = img.StoredDirectory + "/" + img.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (news == null)
            {
                return NotFound();
            }

            // Increment view count
            var newsEntity = await _context.News.FindAsync(id);
            if (newsEntity != null)
            {
                newsEntity.ViewCount++;
                await _context.SaveChangesAsync();
            }

            return news;
        }

        // GET: api/News/slug/{slug}
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<NewsDto>> GetNewsBySlug(string slug)
        {
            var news = await _context.News
                .Include(n => n.NewsCategory)
                .Include(n => n.CreatedByUser)
                .Include(n => n.UpdatedByUser)
                .Include(n => n.NewsImages.Where(img => !img.IsDeleted))
                .Include(n => n.NewsComments.Where(c => !c.IsDeleted))
                .Include(n => n.NewsReactions.Where(r => !r.IsDeleted))
                .Where(n => n.Slug == slug && !n.IsDeleted && n.IsPublished)
                .Select(n => new NewsDto
                {
                    ID = n.ID,
                    Title = n.Title,
                    Slug = n.Slug,
                    Description = n.Description,
                    Excerpt = n.Excerpt,
                    Remark = n.Remark,
                    CategoryID = n.NewsCategoryID,
                    CategoryName = n.NewsCategory.Name,
                    Rate = n.Rate,
                    ViewCount = n.ViewCount,
                    PublishDate = n.PublishDate,
                    IsPublished = n.IsPublished,
                    CreatedDate = n.CreatedDate,
                    UpdatedDate = n.UpdatedDate,
                    CreatedByUserName = n.CreatedByUser != null ? n.CreatedByUser.FirstName + " " + n.CreatedByUser.LastName : null,
                    UpdatedByUserName = n.UpdatedByUser != null ? n.UpdatedByUser.FirstName + " " + n.UpdatedByUser.LastName : null,
                    CommentsCount = n.NewsComments.Count(c => !c.IsDeleted),
                    ReactionsCount = n.NewsReactions.Count(r => !r.IsDeleted),
                    ImagesCount = n.NewsImages.Count(img => !img.IsDeleted),
                    Images = n.NewsImages.Where(img => !img.IsDeleted).Select(img => new NewsImageDto
                    {
                        ID = img.ID,
                        NewsID = img.NewsID,
                        Name = img.Name,
                        StoredDirectory = img.StoredDirectory,
                        AltText = img.AltText,
                        Caption = img.Caption,
                        IsFeatured = img.IsFeatured,
                        UploadedDate = img.UploadedDate,
                        ImageUrl = img.StoredDirectory + "/" + img.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (news == null)
            {
                return NotFound();
            }

            // Increment view count
            var newsEntity = await _context.News.Where(n => n.Slug == slug).FirstOrDefaultAsync();
            if (newsEntity != null)
            {
                newsEntity.ViewCount++;
                await _context.SaveChangesAsync();
            }

            return news;
        }

        // POST: api/News
        [HttpPost]
        public async Task<ActionResult<NewsDto>> PostNews([FromForm] CreateNewsDto createDto, [FromForm] List<IFormFile>? images = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if category exists
            var categoryExists = await _context.NewsCategory.AnyAsync(c => c.ID == createDto.NewsCategoryID && !c.IsDeleted);
            if (!categoryExists)
            {
                return BadRequest("Invalid category ID");
            }

            // Check if slug is unique
            var slugExists = await _context.News.AnyAsync(n => n.Slug == createDto.Slug && !n.IsDeleted);
            if (slugExists)
            {
                return BadRequest("Slug already exists");
            }

            var news = new News
            {
                ID = Guid.NewGuid(),
                Title = createDto.Title,
                Slug = createDto.Slug,
                Description = createDto.Description,
                Excerpt = createDto.Excerpt,
                Remark = createDto.Remark,
                NewsCategoryID = createDto.NewsCategoryID,
                Rate = createDto.Rate,
                ViewCount = 0,
                PublishDate = createDto.PublishDate,
                IsPublished = createDto.IsPublished,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = Guid.Parse(createDto.CreatedBy),
                UpdatedBy = Guid.Parse(createDto.CreatedBy)
            };

            _context.News.Add(news);
            await _context.SaveChangesAsync();

            // Handle image uploads if provided
            if (images != null && images.Any())
            {
                var basePath = _configuration["NewsFileStorage:BasePath"] ?? "C:\\Temp\\NewsImages";
                var newsDirectory = Path.Combine(basePath, "NewsImages", news.ID.ToString());
                
                if (!Directory.Exists(newsDirectory))
                {
                    Directory.CreateDirectory(newsDirectory);
                }

                bool isFirstImage = true;
                foreach (var image in images)
                {
                    // Validate file type
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                    var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        continue; // Skip invalid files
                    }

                    // Generate unique filename
                    var fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}{fileExtension}";
                    var filePath = Path.Combine(newsDirectory, fileName);

                    // Save file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Create database record
                    var newsImage = new NewsImages
                    {
                        ID = Guid.NewGuid(),
                        NewsID = news.ID,
                        Name = fileName,
                        StoredDirectory = newsDirectory,
                        UploadedStatus = "Uploaded",
                        AltText = null,
                        Caption = null,
                        IsFeatured = isFirstImage, // First image is featured by default
                        IsDeleted = false,
                        UploadedDate = DateTime.UtcNow,
                        UploadedBy = Guid.Parse(createDto.CreatedBy)
                    };

                    _context.NewsImages.Add(newsImage);
                    isFirstImage = false;
                }

                await _context.SaveChangesAsync();
            }

            return await GetNews(news.ID);
        }

        // PUT: api/News/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNews(Guid id, UpdateNewsDto updateDto)
        {
            if (id != updateDto.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var news = await _context.News.FindAsync(id);
            if (news == null || news.IsDeleted)
            {
                return NotFound();
            }

            // Check if category exists
            var categoryExists = await _context.NewsCategory.AnyAsync(c => c.ID == updateDto.NewsCategoryID && !c.IsDeleted);
            if (!categoryExists)
            {
                return BadRequest("Invalid category ID");
            }

            // Check if slug is unique (excluding current news)
            var slugExists = await _context.News.AnyAsync(n => n.Slug == updateDto.Slug && n.ID != id && !n.IsDeleted);
            if (slugExists)
            {
                return BadRequest("Slug already exists");
            }

            news.Title = updateDto.Title;
            news.Slug = updateDto.Slug;
            news.Description = updateDto.Description;
            news.Excerpt = updateDto.Excerpt;
            news.Remark = updateDto.Remark;
            news.NewsCategoryID = updateDto.NewsCategoryID;
            news.Rate = updateDto.Rate;
            news.PublishDate = updateDto.PublishDate;
            news.IsPublished = updateDto.IsPublished;
            news.UpdatedDate = DateTime.UtcNow;
            news.UpdatedBy = !string.IsNullOrEmpty(updateDto.UpdatedBy) ? Guid.Parse(updateDto.UpdatedBy) : null;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsExists(id))
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

        // DELETE: api/News/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(Guid id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null || news.IsDeleted)
            {
                return NotFound();
            }

            news.IsDeleted = true;
            news.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/News/5/publish
        [HttpPost("{id}/publish")]
        public async Task<IActionResult> PublishNews(Guid id, [FromBody] string? updatedBy = null)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null || news.IsDeleted)
            {
                return NotFound();
            }

            news.IsPublished = true;
            news.PublishDate = DateTime.UtcNow;
            news.UpdatedDate = DateTime.UtcNow;
            news.UpdatedBy = !string.IsNullOrEmpty(updatedBy) ? Guid.Parse(updatedBy) : null;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/News/5/unpublish
        [HttpPost("{id}/unpublish")]
        public async Task<IActionResult> UnpublishNews(Guid id, [FromBody] string? updatedBy = null)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null || news.IsDeleted)
            {
                return NotFound();
            }

            news.IsPublished = false;
            news.PublishDate = null;
            news.UpdatedDate = DateTime.UtcNow;
            news.UpdatedBy = !string.IsNullOrEmpty(updatedBy) ? Guid.Parse(updatedBy) : null;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool NewsExists(Guid id)
        {
            return _context.News.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}