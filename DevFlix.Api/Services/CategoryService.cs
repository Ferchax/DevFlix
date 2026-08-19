using DevFlix.Api.DTOs;
using DevFlix.Api.Entities;
using DevFlix.Api.Repositories;

namespace DevFlix.Api.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        return categoryRepository.GetAllAsync();
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return null;
        }

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var nameExists = await categoryRepository.NameExistsAsync(dto.Name);
        if (nameExists)
        {
            throw new InvalidOperationException($"Category with name '{dto.Name}' already exists.");
        }

        var category = new Category
        {
            Name = dto.Name
        };

        await categoryRepository.AddAsync(category);

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return null;
        }

        var nameExists = await categoryRepository.NameExistsAsync(dto.Name, id);
        if (nameExists)
        {
            throw new InvalidOperationException($"Category with name '{dto.Name}' already exists.");
        }

        category.Name = dto.Name;

        await categoryRepository.UpdateAsync(category);

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return false;
        }

        var hasChannels = await categoryRepository.HasChannelsAsync(id);
        if (hasChannels)
        {
            throw new InvalidOperationException("Cannot delete category with associated channels.");
        }

        await categoryRepository.RemoveAsync(category);

        return true;
    }
}
