using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.MVC.Models.DTOs;
using PersonnelManagement.MVC.Models;
using PersonnelManagement.MVC.Services.Contracts;
using System.Net.Http;

namespace PersonnelManagement.MVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _PersonService;
        private readonly HttpClient _httpClient;

        public PersonController (IPersonService personService, HttpClient httpClient)
        {
            _PersonService = personService;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var fields = await _PersonService.GetAllPersons();
            var viewModel = fields.Select(p => new PersonnelViewModel
            {
               // PersonId = p.id,
                FName = p.fName,
                LName = p.lName,
                PersonnelCode = p.personnelCode,
                DynamicFields = p.submissions.Select(r => new SubmissionDTO
                {
                    //FieldId = r.fk_FieldDefinition,
                    FieldName = r.displayName,
                    FieldValue = r.fieldValue
                }).ToList() // p.submissions.ToDictionary(df => df.displayName, df => df.fieldValue)
            }).ToList();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Fetch the list of dynamic fields from the API
            var response = await _httpClient.GetAsync("https://localhost:7164/api/DynamicField/GetAllFields");
            response.EnsureSuccessStatusCode();

            var dynamicFields = await response.Content.ReadFromJsonAsync<List<DynamicFieldDefinition>>();

            // Prepare a blank ViewModel with empty values for dynamic fields
            var viewModel = new PersonnelViewModel
            {
                DynamicFields = dynamicFields.Select(df => new SubmissionDTO
                {
                    FieldId = df.id,  // Store the FieldId
                    FieldName = df.displayName,
                    FieldValue = string.Empty,
                    FieldType = df.type
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonnelViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map ViewModel to DTO
                var newPersonnelDto = new PersonnelData
                {
                    fName = model.FName,
                    lName = model.LName,
                    personnelCode = model.PersonnelCode,
                    submissions = model.DynamicFields.Select(df => new FieldSubmission
                    {
                        fieldId = df.FieldId,   // Include FieldId here
                        //displayName = df.FieldName,
                        fieldValue = (df.FieldValue!=null)? df.FieldValue.ToString():null
                    }).ToList() 
                };

                // Send the data to the API
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7164/api/Person/CreatePerson", newPersonnelDto);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                // Handle error if the creation fails
                ModelState.AddModelError(string.Empty, "خطا در ثبت اطلاعات شخص");
            }

            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var person = await _PersonService.GetPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Edit Person
        [HttpPost]
        public async Task<IActionResult> Edit(PersonInfoDTO personDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(personDTO);
            }

            try
            {
                await _personnelService.UpdatePersonAsync(personDTO);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error updating person: {ex.Message}");
                return View(personDTO);
            }
        }
    }
}
