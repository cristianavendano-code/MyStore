using MyStore.Models;

namespace MyStore.Interfaces
{
    public interface ISaleRepository
    {
        ICollection<Sale> GetAllSales();
        Sale? GetSaleById(int id);
        ICollection<Sale> GetSalesByUserId(int userId);
        bool AddSale(Sale sale);
        bool ChangeStatus(Sale sale);
        bool DeleteSale(Sale sale);
        bool Save();
        bool SaveChangesOnly();
    }
}
