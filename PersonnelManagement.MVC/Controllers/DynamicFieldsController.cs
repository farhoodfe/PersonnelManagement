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

    private readonly Dictionary<int, string> fieldTypeMapping = new Dictionary<int, string>
    {
        { 0, "متن" },
        { 1, "عدد" },
        { 2, "تاریخ" },
        { 3, "اعشار" }
    };
    public async Task<IActionResult> Index()
    {
        var fields = await _dynamicFieldService.GetAllFieldsAsync();
        ViewBag.StatusMapping = fieldTypeMapping;

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
