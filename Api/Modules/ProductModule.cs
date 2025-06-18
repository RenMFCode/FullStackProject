using Microsoft.EntityFrameworkCore;

namespace Api.Modules;

public static class ProductModule
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/products", async (AppDbContext db) => await db.Products.ToListAsync());

        app.MapGet("/products/{id}", async (int id, AppDbContext db) =>
            await db.Products.FindAsync(id) is Product product ? Results.Ok(product) : Results.NotFound());

        app.MapPost("/products", async (Product product, AppDbContext db) =>
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/products/{product.Id}", product);
        });

        app.MapPut("/products/{id}", async (int id, Product inputProduct, AppDbContext db) =>
        {
            var product = await db.Products.FindAsync(id);
            if (product is null) return Results.NotFound();
            product.Name = inputProduct.Name;
            product.Price = inputProduct.Price;
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapDelete("/products/{id}", async (int id, AppDbContext db) =>
        {
            var product = await db.Products.FindAsync(id);
            if (product is null) return Results.NotFound();
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapPatch("/products/{id}/price", async (int id, decimal price, AppDbContext db) =>
        {
            var product = await db.Products.FindAsync(id);
            if (product is null) return Results.NotFound();
            if (price < 0) return Results.BadRequest("Price must be non-negative.");
            product.Price = price;
            await db.SaveChangesAsync();
            return Results.Ok(product);
        });
    }
}
