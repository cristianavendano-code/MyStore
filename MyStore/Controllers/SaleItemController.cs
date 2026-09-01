using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.DTO.Product;
using MyStore.DTO.SaleItem;
using MyStore.Interfaces;
using MyStore.Models;
using MyStore.Repository;

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleItemController : ControllerBase
    {
        private readonly ISaleItemRepository _saleItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public SaleItemController(ISaleItemRepository saleItemRepository, IMapper mapper, IProductRepository productRepository, ISaleRepository saleRepository)
        {
            _saleItemRepository = saleItemRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _saleRepository = saleRepository;
        }

        [HttpPost]
        public IActionResult AddItemSale([FromBody] SaleItemCreateDto saleItem)
        {
            if (saleItem == null)
            {
                return BadRequest("Item is null");
            }
            var productInDb = _productRepository.GetProductById(saleItem.ProductId);
            if (productInDb == null)
            {
                return BadRequest("This product id doesn't exist");
            }

            var saleInDb = _saleRepository.GetSaleById(saleItem.SaleId);
            if (saleInDb == null)
            {
                return BadRequest("This sale id doesn't exist");
            }

            var saleItemModel = _mapper.Map<SaleItem>(saleItem);

            saleItemModel.Total = productInDb.Price * saleItemModel.Quantity;
            _saleItemRepository.CreateSaleItem(saleItemModel);

            saleInDb.Total += saleItemModel.Total;
            _saleRepository.SaveChangesOnly();
            
            return Ok("Product added to the cart!!");
        }

        [HttpGet("{saleId}")]
        public IActionResult GetAllBySaleId(int saleId)
        {
            var items = _saleItemRepository.GetAllItemsBySaleId(saleId);
            if (items == null)
            {
                return BadRequest("Sale with that id was not found");
            }
            var ItemsDto = _mapper.Map<List<SaleItemResponseDto>>(items);
            return Ok(ItemsDto);
        }

        [HttpDelete("{itemId}")]
        public IActionResult DeleteItem(int itemId) 
        {
            var saleItemToDelete = _saleItemRepository.GetSaleItem(itemId);
            if (saleItemToDelete == null)
            {
                return NotFound($"Item with Id {itemId} doesn't exist");
            }

            var saleInDb = _saleRepository.GetSaleById(saleItemToDelete.SaleId);

            var saleItemDeleted = _saleItemRepository.DeleteSaleItem(saleItemToDelete);

            saleInDb.Total -= saleItemToDelete.Total;
            _saleRepository.SaveChangesOnly();

            return NoContent();
        }
    }
}
