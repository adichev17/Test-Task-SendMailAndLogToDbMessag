using Microsoft.EntityFrameworkCore;
using TestWork;
using TestWork.DBContexts;
using TestWork.Reposirory;
using TestWork.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IEmailServices, EmailServices>();

SD.SmtpServer = configuration["EmailConfiguration:SmtpServer"];
SD.SmtpPort = int.Parse(configuration["EmailConfiguration:SmtpPort"]);
SD.SmtpUsername = configuration["EmailConfiguration:SmtpUsername"];
SD.SmtpPassword = configuration["EmailConfiguration:SmtpPassword"];
SD.SmtpSSL = bool.Parse(configuration["EmailConfiguration:SmtpSSL"]);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
