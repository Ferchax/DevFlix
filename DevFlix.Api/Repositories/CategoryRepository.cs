using DevFlix.Api.Data;
using DevFlix.Api.DTOs;
using DevFlix.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFlix.Api.Repositories;

public class CategoryRepository(DevFlixDbContext context) : ICategoryRepository
{
    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        return await context.Categories
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await context.Categories.FindAsync(id);
    }

    public async Task AddAsync(Category category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Category category)
    {
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }

    public async Task<bool> NameExistsAsync(string name, int? excludeId = null)
    {
        if (excludeId.HasValue)
        {
            return await context.Categories.AnyAsync(c => c.Name == name && c.Id != excludeId.Value);
        }

        return await context.Categories.AnyAsync(c => c.Name == name);
    }

    public async Task<bool> HasChannelsAsync(int categoryId)
    {
        return await context.Channels.AnyAsync(c => c.CategoryId == categoryId);
    }
}
