using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EConsult.Contracts;
using EConsult.Database;
using EConsult.Services.Abstracts;
using EConsult.ViewModels;

namespace EConsult.Controllers;

[Route("products")]
public class ProductController : Controller
{
    private readonly EConsultDbContext _dbContext;
    private readonly IFileService _fileService;


    public ProductController(EConsultDbContext dbContext, IFileService fileService)
    {
        _dbContext = dbContext;
        _fileService = fileService;
    }

    [HttpGet("{id}/details")]
    public IActionResult GetDetails(int id)
    {
        var product = _dbContext.Products.SingleOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        var model = new ProductDetailsViewModel
        {
            ProductId = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Categories = _dbContext.CategoryProducts
                .Where(cp => cp.ProductId == product.Id)
                .Select(cp => cp.Category.Name)
                .ToList(),
            ImageUrl = _fileService
                .GetStaticFilesUrl(CustomUploadDirectories.Products, product.PhysicalImageName)
        };

        return View("details", model);
    }
}
