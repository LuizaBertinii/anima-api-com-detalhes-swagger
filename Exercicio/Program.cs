using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Exercicio.Db;
using Exercicio.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//// Habilita para geral /////
// builder.Services.AddControllers(options =>
// {
//     options.Filters.Add<CustomValidationExceptionFilter>();
// });

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "API com os alunos Ânima", 
        Version = "1.1.0",
        Description = "API para gerenciar alunos",
        Contact = new OpenApiContact
        {
            Name = "Turma Ânima",
            Email = "danilo@gama.academy",
            Url = new Uri("https://www.gama.academy")
        }
    });

    // options.DocInclusionPredicate((docName, apiDesc) =>
    // {
    //     // Verificar se o grupo de ações corresponde ao grupo configurado
    //     if (apiDesc.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
    //     {
    //         var groupName = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<ApiExplorerSettingsAttribute>()?.GroupName;
    //         return string.Equals(groupName, docName, StringComparison.OrdinalIgnoreCase);
    //     }

    //     return false;
    // });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbAppContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("conexao"),
        new MySqlServerVersion(new Version(8, 0, 21)));
});


var app = builder.Build();

var cultureInfo = new CultureInfo("pt-BR");
app.UseRequestLocalization(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultureInfo);
});

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
