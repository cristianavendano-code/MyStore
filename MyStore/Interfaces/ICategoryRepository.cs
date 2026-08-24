using MyStore.Models;

namespace MyStore.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAllCategories();
        Category? GetCategoryById(int id);
        Category? GetCategoryByDesc(string desc);
        bool AddCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
