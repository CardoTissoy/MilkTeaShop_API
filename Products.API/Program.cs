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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ProductMappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddDbContext<IAppDbContextProduct, AppDbContextProduct1>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString1"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>(); 
builder.Services.AddMediatR(typeof(GetProductListQueryHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetProductByIdQueryHandler).GetTypeInfo().Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
