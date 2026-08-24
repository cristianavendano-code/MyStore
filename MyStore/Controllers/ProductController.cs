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
    }
}
