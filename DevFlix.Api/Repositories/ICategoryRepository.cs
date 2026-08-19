using DevFlix.Api.DTOs;
using DevFlix.Api.Entities;

namespace DevFlix.Api.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();

    Task<Category?> GetByIdAsync(int id);

    Task AddAsync(Category category);

    Task UpdateAsync(Category category);

    Task RemoveAsync(Category category);

    Task<bool> NameExistsAsync(string name, int? excludeId = null);

    Task<bool> HasChannelsAsync(int categoryId);
}
