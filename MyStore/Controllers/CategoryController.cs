using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.Interfaces;
using MyStore.Models;

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetAllCategories();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpGet("desc/{desc}")]
        public IActionResult GetCategoryByDesc(string desc)
        {
            var category = _categoryRepository.GetCategoryByDesc(desc);

            return Ok(category);
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] Category category)
        {
            if (category  == null)
            {
                return BadRequest("Category is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCategory = _categoryRepository.AddCategory(category);
            return Ok(newCategory);
        }
    }
}
