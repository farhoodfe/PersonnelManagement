using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PersonnelController : Controller
{
    private readonly DynamicFieldService _dynamicFieldService;

    public PersonnelController(DynamicFieldService dynamicFieldService)
    {
        _dynamicFieldService = dynamicFieldService;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        // Fetch dynamic fields from API
        var dynamicFields = await _dynamicFieldService.GetAllFieldsAsync();
        return View(dynamicFields); // Pass fields to the View
    }

    [HttpPost]
    public async Task<IActionResult> Create(PersonnelViewModel model)
    {
        //if (!ModelState.IsValid)
        //    return View(model);

        //// Handle Static Fields
        //var staticInfo = new StaticPersonnelInfo
        //{
        //    Name = model.Name,
        //    PersonnelCode = model.PersonnelCode,
        //    LastName = model.LastName
        //};

        //// Handle Dynamic Fields
        //var dynamicInfo = new List<DynamicPersonnelInfo>();
        //foreach (var dynamicField in model.DynamicFields)
        //{
        //    dynamicInfo.Add(new DynamicPersonnelInfo
        //    {
        //        FieldDefinitionId = dynamicField.Key,
        //        FieldValue = dynamicField.Value
        //    });
        //}

        //// Use the PersonnelManagement.Service layer to save data
        //await _personnelService.SavePersonnelAsync(staticInfo, dynamicInfo);

        return RedirectToAction("Index");
    }
}
