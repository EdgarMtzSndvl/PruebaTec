using Microsoft.EntityFrameworkCore;
using Prueba.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PruebaContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));
builder.Services.AddControllers().AddJsonOptions(opt=> {
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var CORSrules = "ReglasCORS";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: CORSrules, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyOrigin().AllowAnyMethod();
    });

});
var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(CORSrules);

app.UseAuthorization();

app.MapControllers();

app.Run();
