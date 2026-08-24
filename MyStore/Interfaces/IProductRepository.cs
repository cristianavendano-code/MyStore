using MyStore.Models;

namespace MyStore.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetAllProducts();
        Product? GetProductById(int id);
        Product? GetProductByName(string name);
        bool AddProduct(Product product);
        bool Save();

    }
}
