using MyStore.Models;

namespace MyStore.Interfaces
{
    public interface ISaleItemRepository
    {
        ICollection<SaleItem> GetAllItemsBySaleId(int saleId);
        SaleItem? GetSaleItem(int id);
        bool CreateSaleItem(SaleItem saleItem);
        bool DeleteSaleItem(SaleItem saleItem);
        bool Save();
    }
}
