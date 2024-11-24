using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.MVC.Models;
using PersonnelManagement.MVC.Services.Contracts;
using System.Threading.Tasks;

public class DynamicFieldsController : Controller
{
    private readonly IDynamicFieldService _dynamicFieldService;

    public DynamicFieldsController(IDynamicFieldService dynamicFieldService)
    {
        _dynamicFieldService = dynamicFieldService;
    }

    public async Task<IActionResult> Index()
    {
        var fields = await _dynamicFieldService.GetAllFieldsAsync();
        return View(fields);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(DynamicFieldDefinition model)
    {
        if (!ModelState.IsValid) return View(model);

        await _dynamicFieldService.CreateFieldAsync(model);
        return RedirectToAction("Index");
    }

    // Implement Edit, Delete actions
}
