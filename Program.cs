using DevJobs.API.Persistence;
using DevJobs.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DevJobsCs");

builder.Services.AddDbContext<DebJobsContext>(options => {
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IJobVacancyRepository, JobVacancyRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "DebJobs.API",
        Version = "v1",
        Contact = new OpenApiContact {
            Name = "Adriano Cirino",
            Email = "adrianocirino.si@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/adriano-cirino-b6aab9124/")
        }
    });

    var xmlFile = $"DevJobs.API.xml";
    
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});

builder.Host.ConfigureAppConfiguration((context, config) => {
    Serilog.Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.MSSqlServer(connectionString,
            sinkOptions: new MSSqlServerSinkOptions() {
                AutoCreateSqlTable = true,
                TableName = "tb_logs"
            })
        .WriteTo.Console()
        .CreateLogger();
}).UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
