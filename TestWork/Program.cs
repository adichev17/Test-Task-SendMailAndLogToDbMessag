using Microsoft.EntityFrameworkCore;
using TestWork;
using TestWork.DBContexts;
using TestWork.MailConfig;
using TestWork.Reposirory;
using TestWork.Services;

var builder = WebApplication.CreateBuilder(args);
var MailBuilder = new ConfigurationBuilder().AddJsonFile("mailconfig.json");
var MailConfiguration = MailBuilder.Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IEmailServices, EmailServices>();
builder.Services.Configure<MailConfigConfiguration>(MailConfiguration);



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
