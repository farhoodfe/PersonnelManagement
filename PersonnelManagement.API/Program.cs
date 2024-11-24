using AutoMapper;
using PersonnelManagement.Data;
using PersonnelManagement.Data.Entities;
using PersonnelManagement.Data.Repository;
using PersonnelManagement.Data.Repository.Contract;
using PersonnelManagement.Service;
using PersonnelManagement.Service.Contracts;
using PersonnelManagement.Service.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

var mapperConfig = new MapperConfiguration(mc => {
    mc.AddProfile(new MappingConfig());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IFieldDefinitionService, FieldDefinitionService>();
builder.Services.AddScoped<IPersonnelService, PersonnelService>();
builder.Services.AddScoped<IRepository<DynamicFieldDefinition>, Repository<DynamicFieldDefinition>>();
builder.Services.AddScoped<IRepository<PersonInfo>, Repository<PersonInfo>>();
builder.Services.AddScoped<IRepository<FieldSubmission>, Repository<FieldSubmission>>();

builder.Services.AddDbContext<PersonnelDBContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
