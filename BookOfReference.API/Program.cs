using BookOfReference.API.Models;
using BookOfReference.API.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using BookOfReference.API.DependencyInjection;
using BookOfReference.API.Contracts;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BookOfReferenceDbContextConnection");
// Add services to the container.

builder.Services.AddControllers()
                .AddFluentDependency()
                .AddJsonConverterDependency()
                .AddJsonOptions(sp => sp.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IRelatedPersonRepository, RelatedPersonRepository>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddDbContext<BookOfReferenceDbContext>(options =>
options.UseSqlServer(connectionString));

//builder.Services.AddValidatorsFromAssemblyContaining<PersonDTOValidation>();
//builder.Services.AddScoped<IValidator<PersonDTO>, PersonDTOValidation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<GlobalErrorHandlerMiddleware>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
