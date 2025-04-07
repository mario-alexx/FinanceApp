using System.Security.Claims;
using ATCMediator.Mediator.Interfaces;
using FinanceApp.Application.DTOs.Category;
using FinanceApp.Application.Interfaces;
using FinanceApp.Application.Services.Categories.CreateCategory;
using FinanceApp.Application.Services.Categories.GetCategoryAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {   
        private readonly ICategoryService _categoryService;
        private readonly IMediator _mediator;

        public CategoriesController(
            ICategoryService categoryService,
            IMediator mediator
        )
        {
            _categoryService = categoryService;
            _mediator = mediator;
        }

        private Guid GetUserId() 
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if(userIdClaim == null)
                throw new UnauthorizedAccessException("User ID not found");

            return Guid.Parse(userIdClaim.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var categories = await _mediator.SendQuery(new GetCategoryAllQuery { UserId = userId });
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();
            var category = await _categoryService.GetByIdAsync(id, userId);
            if (category == null)
                return NotFound();
            
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand createDto)
        {
            createDto.UserId = GetUserId();
            var category = await _mediator.SendCommand(createDto);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCategoryDto updateDto)
        {
            var userId = GetUserId();
            var updated = await _categoryService.UpdateAsync(updateDto, userId);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var deleted = await _categoryService.DeleteAsync(id, userId);
            return deleted ? NoContent() : NotFound();
        }
    }
}
