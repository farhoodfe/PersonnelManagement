using PersonnelManagement.MVC.Models;
using PersonnelManagement.MVC.Services.Contracts;
using System.Net.Http;
using System.Text.Json;

namespace PersonnelManagement.MVC.Services
{

    public class PersonService : IPersonService
    {
        private readonly HttpClient _httpClient;

        public PersonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<PersonnelViewModel>> GetAllPersons()
        {
            var response = await _httpClient.GetAsync("https://localhost:7164/api/Person/GetAllPersonInfos");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<PersonnelViewModel>>(jsonResponse);
            var personnelData = await response.Content.ReadFromJsonAsync<List<PersonnelViewModel>>();

            // Map API response to ViewModel
            //var viewModel = personnelData.Select(p => new PersonnelViewModel
            //{
            //    personId = p.PersonId,
            //    FName = p.FName,
            //    LName = p.LName,
            //    PersonnelCode = p.PersonnelCode,
            //    DynamicFields = p.DynamicFields.ToDictionary(df => df.Key, df => df.Value)
            //}).ToList();

            //return viewModel;
             
        }
    }
}
