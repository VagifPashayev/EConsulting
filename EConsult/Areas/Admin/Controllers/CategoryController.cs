using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EConsult.Areas.Admin.ViewModels.Category;
using EConsult.Contracts;
using EConsult.Database;
using EConsult.Database.Models;
using EConsult.Services.Abstracts;

namespace EConsult.Areas.Admin.Controllers;

[Route("admin/categories")]
[Area("admin")]
[Authorize(Roles = Role.Names.SuperAdmin)]
public class CategoryController : Controller
{
    private readonly EConsultDbContext _dbContext;
    private readonly IFileService _fileService;

    public CategoryController(EConsultDbContext dbContext, IFileService fileService)
    {
        _dbContext = dbContext;
        _fileService = fileService;
    }

    #region Index

    [HttpGet]
    public IActionResult Index()
    {
        var categories = _dbContext.Categories.ToList();
        var categoryViewModels = categories
            .Select(c => new CategoryListItemViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        return View(categoryViewModels);
    }

    #endregion

    #region Add

    [HttpGet("add")]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost("add")]
    public IActionResult Add(CategoryAddViewModel model)
    {
        if (!ModelState.IsValid)
            return View();

        var category = new Category
        {
            Name = model.Name,
        };

        _dbContext.Categories.Add(category);
        _dbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Update

    [HttpGet("{id}/update")]
    public IActionResult Update(int id)
    {
        var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null) return NotFound();

        var model = new CategoryUpdateViewModel
        {
            Id = category.Id,
            Name = category.Name
        };

        return View(model);
    }

    [HttpPost("{id}/update")]
    public IActionResult Update(CategoryUpdateViewModel model)
    {
        if (!ModelState.IsValid)
            return View();

        var category = _dbContext.Categories.FirstOrDefault(c => c.Id == model.Id);
        if (category == null)
        {
            ModelState.AddModelError("Name", "Category not found");
            return View();
        }

        category.Name = model.Name;

        _dbContext.Categories.Update(category);
        _dbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Delete

    [HttpPost("{id}/delete")]
    public IActionResult Delete(int id)
    {
        var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null) return NotFound();

        _dbContext.Categories.Remove(category);
        _dbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    #endregion
}
