using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Products.API.Models;
namespace Products.API.Infrastructure.Data;

public partial class AppDbContextProduct : DbContext, IAppDbContextProduct
{

    public AppDbContextProduct(DbContextOptions<AppDbContextProduct> options)
    : base(options)
    {
        Database.EnsureCreated();
    }
    public virtual DbSet<Product> Products { get; set; }

    public virtual void SetModified(object entity)
    {
        Entry(entity).State = EntityState.Modified;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
