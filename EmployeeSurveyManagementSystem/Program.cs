using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Service;
using DataAccessLayer.Interface;
using DataAccessLayer.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();  
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUserDAL, UserDAL>();

builder.Services.AddScoped<IUserBLL, UserBLL>();

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
