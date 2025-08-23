INSERT INTO NewsCategory (
    ID, 
    Name, 
    Slug, 
    Description, 
    ParentCategoryID, 
    IsDeleted, 
    CreatedDate, 
    UpdatedDate, 
    CreatedBy, 
    UpdatedBy
)
VALUES
-- Main category
(NEWID(), 'Technology', 'technology', 'All news related to technology', NULL, 0, GETDATE(), GETDATE(), '11111111-1111-1111-1111-111111111111', '11111111-1111-1111-1111-111111111111'),

-- Subcategory under Technology
(NEWID(), 'Artificial Intelligence', 'artificial-intelligence', 'News about AI and machine learning', 
 (SELECT TOP 1 ID FROM NewsCategory WHERE Name = 'Technology'), 0, GETDATE(), GETDATE(), '11111111-1111-1111-1111-111111111111', '11111111-1111-1111-1111-111111111111'),

-- Another main category
(NEWID(), 'Health', 'health', 'News and updates on healthcare and wellness', NULL, 0, GETDATE(), GETDATE(), '11111111-1111-1111-1111-111111111111', '11111111-1111-1111-1111-111111111111'),

-- Subcategory under Health
(NEWID(), 'Nutrition', 'nutrition', 'Articles and research about nutrition and diet', 
 (SELECT TOP 1 ID FROM NewsCategory WHERE Name = 'Health'), 0, GETDATE(), GETDATE(), '11111111-1111-1111-1111-111111111111', '11111111-1111-1111-1111-111111111111');
