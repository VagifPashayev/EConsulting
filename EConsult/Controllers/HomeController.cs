using Microsoft.AspNetCore.Mvc;
using EConsult.Database;
using EConsult.ViewModels;

namespace EConsult.Controllers;

public class HomeController : Controller //controller
{
    private readonly EConsultDbContext _pustokDbContext;

    public HomeController(EConsultDbContext pustokDbContext)
    {
        _pustokDbContext = pustokDbContext;
    }

    public IActionResult Index()
    {
        var model = new HomeViewModel
        {
            Products = _pustokDbContext.Products
               .OrderBy(p => p.Name)
               .ToList()
        };

        return View(model);
    }
}