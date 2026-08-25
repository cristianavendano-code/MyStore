using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.DTO.Product;
using MyStore.Interfaces;
using MyStore.Models;
using MyStore.Repository;

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            var productsDto = _mapper.Map<List<ProductResponseDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            var productDto = _mapper.Map<ProductResponseDto>(product);
            return Ok(productDto);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetProductByName(string name)
        {
            var product = _productRepository.GetProductByName(name);
            var productDto = _mapper.Map<ProductResponseDto>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductCreateDto product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }
            var productModel = _mapper.Map<Product>(product);

            var productInDb = _productRepository.GetAllProducts()
                .Where(p => p.Name.ToUpper() == product.Name.ToUpper())
                .FirstOrDefault();
            if (productInDb != null)
            {
                return BadRequest("This product was added before!");
            }

            var newProduct = _productRepository.AddProduct(productModel);

            return Ok("Product added succesfully!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductUpdateDto product)
        {
            var productToModify = _productRepository.GetProductById(id);
            if (productToModify == null)
            {
                return NotFound($"Product with Id {id} doesn't exist");
            }

            if (product == null)
            {
                return BadRequest("Product is null");
            }

            _mapper.Map(product, productToModify);
            var productUpdated = _productRepository.UpdateProduct(productToModify);
            return Ok($"Product with Id {id} was succesfully updated!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var productToDelete = _productRepository.GetProductById(id);
            if (productToDelete == null)
            {
                return NotFound($"Product with Id {id} doesn't exist");
            }

            var productDeleted = _productRepository.DeleteProduct(productToDelete);
            return NoContent();
        }

        [HttpPatch("{id}/addStock")]
        public IActionResult AddStock(int id, [FromBody] ProductAddStockDto stock)
        {
            if (stock == null || stock.QuantityToAdd <= 0)
            {
                return BadRequest("The quantity to add must be bigger or equal to 0");
            }

            var productToUpdate = _productRepository.GetProductById(id);
            if (productToUpdate == null)
            {
                return NotFound($"Product with Id {id} doesn't exist");
            }

            productToUpdate.Stock += stock.QuantityToAdd;
            _productRepository.UpdateProduct(productToUpdate);

            return Ok($"Stock for product with Id {id} was successfully updated!");
        }

        [HttpPatch("{id}/subtractStock")]
        public IActionResult SubStock(int id, [FromBody] ProductSubStockDto stock)
        {
            if (stock == null || stock.QuantityToSubtract <= 0)
            {
                return BadRequest("The quantity to subtract must be bigger or equal to 0");
            }

            var productToUpdate = _productRepository.GetProductById(id);
            if (productToUpdate == null)
            {
                return NotFound($"Product with Id {id} doesn't exist");
            }

            productToUpdate.Stock -= stock.QuantityToSubtract;
            _productRepository.UpdateProduct(productToUpdate);

            return Ok($"Stock for product with Id {id} was successfully updated!");
        }
    }
}
