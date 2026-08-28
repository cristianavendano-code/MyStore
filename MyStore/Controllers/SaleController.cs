using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.DTO.Product;
using MyStore.DTO.Sale;
using MyStore.Interfaces;
using MyStore.Models;
using MyStore.Repository;

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public SaleController(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateSale([FromBody] SaleCreateDto sale)
        {
            if (sale == null)
            {
                return BadRequest("Sale is null");
            }
            var saleModel = _mapper.Map<Sale>(sale);

            var newProduct = _saleRepository.AddSale(saleModel);

            return Ok("Sale added succesfully!");
        }

        [HttpGet]
        public IActionResult GetSales()
        {
            var sales = _saleRepository.GetAllSales();
            var salesDto = _mapper.Map<List<SaleResponseDto>>(sales);
            return Ok(salesDto);
        }

        [HttpGet("ByUser/{userId}")]
        public IActionResult GetSalesByUser(int userId)
        {
            var sales = _saleRepository.GetSalesByUserId(userId);
            var salesDto = _mapper.Map<List<SaleResponseDto>>(sales);
            return Ok(salesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetSaleById(int id)
        {
            var sale = _saleRepository.GetSaleById(id);
            var saleDto = _mapper.Map<SaleResponseDto>(sale);
            return Ok(saleDto);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateStatus(int id)
        {
            var saleToUpdate = _saleRepository.GetSaleById(id);
            if (saleToUpdate == null)
            {
                return NotFound($"Sale with Id {id} doesn't exist");
            }
            saleToUpdate.Status = "Comprado";
            _saleRepository.SaveChangesOnly();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteSale(int id)
        {
            var saleToDelete = _saleRepository.GetSaleById(id);
            if (saleToDelete == null)
            {
                return NotFound($"Sale with Id {id} doesn't exist");
            }

            var saleDeleted = _saleRepository.DeleteSale(saleToDelete);
            return NoContent();
        }
    }
}
