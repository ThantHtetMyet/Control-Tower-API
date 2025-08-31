using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.NewsPortalSystem;
using ControlTower.DTOs.NewsPortalSystem;
using ControlTower.DTOs;
using System.IO;
using ControlTower.Attributes; // Add this if missing

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
        [NewsPortalAuthorization("User")]
        public async Task<ActionResult<PagedResult<NewsListDto>>> GetNews(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] Guid? categoryId = null,
            [FromQuery] bool? isPublished = null)
        {
            // Get the base URL from configuration
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7145";
            
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
                .OrderByDescending(n => n.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new NewsListDto
                {
                    ID = n.ID,
                    Title = n.Title,
                    Slug = n.Slug,
                    Excerpt = n.Excerpt,
                    NewsCategoryID = n.NewsCategoryID,  // Now this property exists
                    CategoryName = n.NewsCategory.Name,
                    ViewCount = n.ViewCount,
                    PublishDate = n.PublishDate,
                    IsPublished = n.IsPublished,
                    CreatedDate = n.CreatedDate,
                    CreatedByUserName = n.CreatedByUser != null ? n.CreatedByUser.FirstName + " " + n.CreatedByUser.LastName : null,
                    CommentsCount = n.NewsComments.Count(c => !c.IsDeleted),
                    ReactionsCount = n.NewsReactions.Count(r => !r.IsDeleted),
                    FeaturedImageUrl = n.NewsImages.FirstOrDefault(img => img.IsFeatured && !img.IsDeleted) != null ? 
                  $"{apiBaseUrl}/api/News/image/{n.ID}/{n.NewsImages.FirstOrDefault(img => img.IsFeatured && !img.IsDeleted).Name}" : null
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
            // Get the base URL from configuration
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7145";

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
                    FeaturedHeaderImageUrl = n.NewsImages.FirstOrDefault(img => img.IsFeatured && !img.IsDeleted && img.ImageType == "header") != null ? 
                      $"{apiBaseUrl}/api/News/image/{n.ID}/{n.NewsImages.FirstOrDefault(img => img.IsFeatured && !img.IsDeleted).Name}" : null,
                    FeaturedThumbnailImageUrl = n.NewsImages.FirstOrDefault(img => img.IsFeatured && !img.IsDeleted && img.ImageType == "thumbnail") != null ? 
                      $"{apiBaseUrl}/api/News/image/{n.ID}/{n.NewsImages.FirstOrDefault(img => img.IsFeatured && !img.IsDeleted && img.ImageType == "thumbnail").Name}" : null,
                    Images = n.NewsImages.Where(img => !img.IsDeleted).Select(img => new NewsImageDto
                    {
                        ID = img.ID,
                        NewsID = img.NewsID,
                        ImageType = img.ImageType, 
                        Name = img.Name,
                        StoredDirectory = img.StoredDirectory,
                        AltText = img.AltText,
                        Caption = img.Caption,
                        IsFeatured = img.IsFeatured,
                        UploadedDate = img.UploadedDate,
                        ImageUrl = $"{apiBaseUrl}/api/News/image/{n.ID}/{img.Name}"
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
            // Get the base URL from configuration
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7145";

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
                    // Around line 210 - Same fix for GetNewsBySlug method
                    Images = n.NewsImages.Where(img => !img.IsDeleted).Select(img => new NewsImageDto
                    {
                        ID = img.ID,
                        NewsID = img.NewsID,
                        Name = img.Name,
                        StoredDirectory = img.StoredDirectory,
                        AltText = img.AltText,
                        Caption = img.Caption,
                        ImageType = img.ImageType, // Add this line
                        IsFeatured = img.IsFeatured,
                        UploadedDate = img.UploadedDate,
                        ImageUrl = $"{apiBaseUrl}/api/News/image/{n.ID}/{img.Name}"
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
        [ApplicationAuthorization("News Portal System", "Admin")]
        public async Task<ActionResult<NewsDto>> PostNews([FromForm] CreateNewsDto createDto)
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
        
            if (!Guid.TryParse(createDto.CreatedBy, out var createdByGuid))
            {
                return BadRequest("Invalid CreatedBy ID");
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
                CreatedBy = createdByGuid,
                UpdatedBy = createdByGuid
            };
        
            _context.News.Add(news);
            await _context.SaveChangesAsync();
        
            return await GetNews(news.ID);
        }

        // PUT: api/News/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNews(Guid id, [FromForm] UpdateNewsDto updateDto)
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
        [ApplicationAuthorization("News Portal System", "Admin")] // Add authorization like categories
        public async Task<IActionResult> DeleteNews(Guid id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null || news.IsDeleted)
            {
                return NotFound("News article not found or already deleted");
            }
        
            // Optional: Check if user has permission to delete this specific news
            // You could add additional checks here if needed
        
            news.IsDeleted = true;
            news.UpdatedDate = DateTime.UtcNow;
            
            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while deleting the news article");
            }
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
            
        // POST: api/News/{id}/thumbnail
        [HttpPost("{id}/thumbnail")]
        public async Task<IActionResult> UploadThumbnail(Guid id, [FromForm] UploadThumbnailDto uploadDto)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null || news.IsDeleted)
            {
                return NotFound("News not found");
            }

            if (uploadDto.ThumbnailImage == null)
            {
                return BadRequest("Thumbnail image is required");
            }

            // Remove existing thumbnail if any
            var existingThumbnail = await _context.NewsImages
                .FirstOrDefaultAsync(img => img.NewsID == id && img.ImageType == "thumbnail" && !img.IsDeleted);
            if (existingThumbnail != null)
            {
                existingThumbnail.IsDeleted = true;
            }

            var basePath = _configuration["NewsFileStorage:BasePath"] ?? "C:\\Temp\\NewsImages";
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            await SaveNewsImage(uploadDto.ThumbnailImage, id, "thumbnail", basePath, uploadDto.UploadedBy);
            await _context.SaveChangesAsync();

            return Ok("Thumbnail uploaded successfully");
        }

        // POST: api/News/{id}/header
        [HttpPost("{id}/header")]
        public async Task<IActionResult> UploadHeader(Guid id, [FromForm] UploadHeaderDto uploadDto)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null || news.IsDeleted)
            {
                return NotFound("News not found");
            }

            if (uploadDto.HeaderImage == null)
            {
                return BadRequest("Header image is required");
            }

            // Remove existing header if any
            var existingHeader = await _context.NewsImages
                .FirstOrDefaultAsync(img => img.NewsID == id && img.ImageType == "header" && !img.IsDeleted);
            if (existingHeader != null)
            {
                existingHeader.IsDeleted = true;
            }

            var basePath = _configuration["NewsFileStorage:BasePath"] ?? "C:\\Temp\\NewsImages";
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            await SaveNewsImage(uploadDto.HeaderImage, id, "header", basePath, uploadDto.UploadedBy);
            await _context.SaveChangesAsync();

            return Ok("Header image uploaded successfully");
        }

        // Helper method for saving news images
        private async Task SaveNewsImage(IFormFile image, Guid newsId, string imageType, string basePath, string uploadedBy)
        {
            // Validate file type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return; // Skip invalid files
            }

            // Create news-specific directory
            var newsDirectory = Path.Combine(basePath, newsId.ToString());
            if (!Directory.Exists(newsDirectory))
            {
                Directory.CreateDirectory(newsDirectory);
            }

            // Generate unique filename with image type prefix
            var fileName = $"{imageType}_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}{fileExtension}";
            var filePath = Path.Combine(newsDirectory, fileName);

            // Save file to news-specific directory
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Create database record
            var newsImage = new NewsImages
            {
                ID = Guid.NewGuid(),
                NewsID = newsId,
                Name = fileName,
                StoredDirectory = newsDirectory,
                UploadedStatus = "Uploaded",
                AltText = null,
                Caption = null,
                ImageType = imageType,
                IsFeatured = imageType == "thumbnail", // Thumbnail is featured by default
                IsDeleted = false,
                UploadedDate = DateTime.UtcNow,
                UploadedBy = Guid.Parse(uploadedBy)
            };

            _context.NewsImages.Add(newsImage);
        }

                // GET: api/News/image/{newsId}/{imageName}
        [HttpGet("image/{newsId}/{imageName}")]
        public async Task<IActionResult> GetNewsImage(Guid newsId, string imageName)
        {
            var basePath = _configuration["NewsFileStorage:BasePath"] ?? "C:\\Temp\\NewsImages";
            var fullPath = Path.Combine(basePath, newsId.ToString(), imageName);

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            // Detect content type dynamically
            var extension = Path.GetExtension(imageName).ToLowerInvariant();
            var contentType = extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "image/jpeg"
            };

            var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            return File(fileBytes, contentType);
        }
    }

}

