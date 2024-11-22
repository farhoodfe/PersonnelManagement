using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class DynamicFieldService
{
    private readonly HttpClient _httpClient;

    public DynamicFieldService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DynamicFieldDto>> GetAllFieldsAsync()
    {
        var response = await _httpClient.GetAsync("https://localhost:7164/api/DynamicField/GetAllFields");
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<DynamicFieldDto>>(jsonResponse);
    }
}

// DTO to represent the field JSON structure
public class DynamicFieldDto
{
    public int Id { get; set; }
    public string FieldName { get; set; }
    public string DisplayName { get; set; }
    public int Type { get; set; }
    public bool IsRequired { get; set; }
    public bool? IsDeleted { get; set; }
}
