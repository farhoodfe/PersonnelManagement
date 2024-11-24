using PersonnelManagement.MVC.Models;
using PersonnelManagement.MVC.Services.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class DynamicFieldService : IDynamicFieldService
{
    private readonly HttpClient _httpClient;

    public DynamicFieldService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DynamicFieldDefinition>> GetAllFieldsAsync()
    {
        var response = await _httpClient.GetAsync("https://localhost:7164/api/DynamicField/GetAllFields");
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<DynamicFieldDefinition>>(jsonResponse);
         
    }

    public async Task<bool> CreateFieldAsync(DynamicFieldDefinition field)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7164/api/DynamicField/CreateDynamicField", field);
        return response.IsSuccessStatusCode;
    }

    // Implement Edit, Delete, etc.
}
