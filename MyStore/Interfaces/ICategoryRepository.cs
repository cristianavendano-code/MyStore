using MyStore.Models;

namespace MyStore.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAllCategories();
        Category? GetCategoryById(int id);
        Category? GetCategoryByDesc(string desc);
        Category AddCategory(Category category);
    }
}
