using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyStore.DTO.Category;
using MyStore.Interfaces;
using MyStore.Models;

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetAllCategories();
            var categoriesDto = _mapper.Map<List<CategoryResponseDto>>(categories);

            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            var categoryDto = _mapper.Map<CategoryResponseDto>(category);

            return Ok(categoryDto);
        }

        [HttpGet("desc/{desc}")]
        public IActionResult GetCategoryByDesc(string desc)
        {
            var category = _categoryRepository.GetCategoryByDesc(desc);
            var categoryDto = _mapper.Map<CategoryResponseDto>(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryCreateDto category)
        {
            if (category == null)
            {
                return BadRequest("Category is null");
            }
            var categoryModel = _mapper.Map<Category>(category);

            var categoryInDb = _categoryRepository.GetAllCategories()
                .Where(c => c.Description.ToUpper() == category.Description.ToUpper())
                .FirstOrDefault();
            if (categoryInDb != null)
            {
                return BadRequest("This category was added before!");
            }

            var newCategory = _categoryRepository.AddCategory(categoryModel);

            return Ok("Category added succesfully!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryUpdateDto category)
        {
            var categoryToModify = _categoryRepository.GetCategoryById(id);
            if (categoryToModify == null)
            {
                return NotFound($"Category with Id {id} doesn't exist");
            }

            if (category == null)
            {
                return BadRequest("Category is null");
            }

            _mapper.Map(category, categoryToModify);
            var categoryUpdated = _categoryRepository.UpdateCategory(categoryToModify);
            return Ok($"Category with Id {id} was succesfully updated!");

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var categoryToDelete = _categoryRepository.GetCategoryById(id);
            if (categoryToDelete == null)
            {
                return NotFound($"Category with Id {id} doesn't exist");
            }

            var categoryDeleted = _categoryRepository.DeleteCategory(categoryToDelete);
            return NoContent();
        }
    }
}
