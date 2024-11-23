using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonnelManagement.RazorUI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CreateModel : PageModel
{
    private readonly DynamicFieldService _dynamicFieldService;

    public CreateModel(DynamicFieldService dynamicFieldService)
    {
        _dynamicFieldService = dynamicFieldService;
    }

    public List<DynamicFieldDto> DynamicFields { get; set; } = new List<DynamicFieldDto>();

    // Static fields for personnel info
    [BindProperty]
    public string Name { get; set; }

    [BindProperty]
    public string PersonnelCode { get; set; }

    [BindProperty]
    public string LastName { get; set; }

    // Dynamic fields data
    [BindProperty]
    public Dictionary<long, string> DynamicFieldValues { get; set; } = new Dictionary<long, string>();

    public async Task OnGetAsync()
    {
        // Fetch dynamic fields from the API
        DynamicFields = await _dynamicFieldService.GetAllFieldsAsync();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        // Handle form submission (static + dynamic fields)
        var staticPersonnelInfo = new
        {
            Name,
            PersonnelCode,
            LastName
        };

        var dynamicPersonnelInfo = new List<DynamicPersonnelInfo>();
        foreach (var field in DynamicFieldValues)
        {
            dynamicPersonnelInfo.Add(new DynamicPersonnelInfo
            {
                FieldDefinitionId = field.Key,
                FieldValue = field.Value
            });
        }

        // Save or process the data here...

        return RedirectToPage("Index");
    }
}

// DTO for saving dynamic fields
public class DynamicPersonnelInfo
{
    public long FieldDefinitionId { get; set; }
    public string FieldValue { get; set; }
}
