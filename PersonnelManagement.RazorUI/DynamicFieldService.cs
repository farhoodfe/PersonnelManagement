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

public class DynamicFieldDto
{
    public long id { get; set; }
    public string fieldName { get; set; }
    public string displayName { get; set; }
    public int type { get; set; }
    public bool isRequired { get; set; }
    public bool? isDeleted { get; set; }
}
