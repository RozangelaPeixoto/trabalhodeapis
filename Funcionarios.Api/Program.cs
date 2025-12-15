using Funcionarios.Api.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

DotNetEnv.Env.Load("../.env");
var builder = WebApplication.CreateBuilder(args);

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT")  ;
var dbName = "empresas_db";
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connectionString =
    $"server={dbHost};Port={dbPort};Database={dbName};User={dbUser};Password={dbPassword};" +
    $"SslMode=None;AllowPublicKeyRetrieval=True";

//Configura o DbContext com o Mysql(Pomelo)
builder.Services.AddDbContext<ApplicationDbContext>(option => 
    option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//Controllers
builder.Services.AddControllers();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseUrls("http://localhost:5001");

var app = builder.Build();

// Tratamento global de exceções
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        var problem = new ProblemDetails
        {
            Instance = context.Request.Path
        };

        switch (exception)
        {
            case MySqlException:
                problem.Status = StatusCodes.Status503ServiceUnavailable;
                problem.Title = "Serviço indisponível";
                problem.Detail = "Não foi possível conectar ao banco de dados.";
                break;

            case DbUpdateException:
                problem.Status = StatusCodes.Status400BadRequest;
                problem.Title = "Erro ao atualizar o banco de dados";
                problem.Detail = "Ocorreu um erro ao tentar salvar as alterações no banco de dados.";
                break;

            default:
                problem.Status = StatusCodes.Status500InternalServerError;
                problem.Title = "Erro interno do servidor";
                problem.Detail = "Ocorreu um erro inesperado.";
                break;
        }

        context.Response.StatusCode = problem.Status.Value;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problem);
    });
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();


app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.MapControllers();

app.Run();
