using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Interfaces;
using MyStore.Models;

namespace MyStore.Repository
{
    public class SaleItemRepository : ISaleItemRepository
    {
        private readonly AppDbContext _context;

        public SaleItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool CreateSaleItem(SaleItem saleItem)
        {
            _context.SalesItems.Add(saleItem);
            return Save();
        }

        public bool DeleteSaleItem(SaleItem saleItem)
        {
            _context.SalesItems.Remove(saleItem);
            return Save();
        }

        public ICollection<SaleItem> GetAllItemsBySaleId(int saleId)
        {
            return _context.SalesItems.Where(c => c.SaleId == saleId).Include(c => c.Product).ToList();
        }

        public SaleItem? GetSaleItem(int id)
        {
            return _context.SalesItems.FirstOrDefault(s => s.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
