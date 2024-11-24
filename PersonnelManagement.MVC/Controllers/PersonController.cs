using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.MVC.Models.DTOs;
using PersonnelManagement.MVC.Models;
using PersonnelManagement.MVC.Services.Contracts;

namespace PersonnelManagement.MVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _PersonService;

        public PersonController (IPersonService personService)
        {
            _PersonService = personService;
        }

        public async Task<IActionResult> Index()
        {
            var fields = await _PersonService.GetAllPersons();
            var viewModel = fields.Select(p => new PersonnelViewModel
            {
                PersonId = p.id,
                FName = p.fName,
                LName = p.lName,
                PersonnelCode = p.personnelCode,
                DynamicFields = p.submissions.ToDictionary(df => df.displayName, df => df.fieldValue)
            }).ToList();
            return View(viewModel);
        }

        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(DynamicFieldDefinition model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    await _dynamicFieldService.CreateFieldAsync(model);
        //    return RedirectToAction("Index");
        //}
    }
}
