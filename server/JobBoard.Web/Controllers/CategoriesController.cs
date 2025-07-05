using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using JobBoard.Application.DTOs;
using JobBoard.Core.Entities;
using JobBoard.Application.Validators;
using JobBoard.Core.Interfaces;

namespace JobBoard.Web.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IValidator<CategoryDto> _validator;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IValidator<CategoryDto> validator, IMapper mapper)
    {
        _categoryService = categoryService;
        _validator = validator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryDto dto)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var category = _mapper.Map<Category>(dto);
            var result = await _categoryService.CreateCategoryAsync(category);
            var resultDto = _mapper.Map<CategoryDto>(result);
            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
            return NotFound();

        var categoryDto = _mapper.Map<CategoryDto>(category);
        return Ok(categoryDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
        return Ok(categoryDtos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CategoryDto dto)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            category.Name = dto.Name;
            await _categoryService.UpdateCategoryAsync(category);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}