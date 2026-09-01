using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Interfaces;
using MyStore.Models;

namespace MyStore.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool AddSale(Sale sale)
        {
            _context.Sales.Add(sale);
            return Save();
        }

        public bool DeleteSale(Sale sale)
        {
            _context.Remove(sale);
            return Save();
        }

        public ICollection<Sale> GetAllSales()
        {
            return _context.Sales.Include(s => s.Client).ToList();
        }

        public Sale? GetSaleById(int id)
        {
            return _context.Sales.Include(s => s.Client).FirstOrDefault(s => s.Id == id);
        }
         
        public ICollection<Sale> GetSalesByUserId(int userId)
        {
            return _context.Sales.Where(u => u.ClientId == userId).Include(s => s.Client).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool SaveChangesOnly()
        {
            return Save();
        }
    }
}
