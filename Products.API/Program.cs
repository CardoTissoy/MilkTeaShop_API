using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.API.Core.Features.Queries.GetProduct;
using Products.API.Core.Features.Queries.GetProductById;
using Products.API.Core.MapperProfiles;
using Products.API.Infrastructure.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ProductMappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddDbContext<IAppDbContextProduct, AppDbContextProduct>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>(); 
builder.Services.AddMediatR(typeof(GetProductListQueryHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetProductByIdQueryHandler).GetTypeInfo().Assembly);


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

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
