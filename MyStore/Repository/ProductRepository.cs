using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Interfaces;
using MyStore.Models;

namespace MyStore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool AddProduct(Product product)
        {
            _context.Products.Add(product);
            return Save();
        }

        public ICollection<Product> GetAllProducts()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }

        public Product? GetProductById(int id)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(p  => p.Id == id);
        }

        public Product? GetProductByName(string name)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Name == name);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
