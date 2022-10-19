using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/getproduct/{code}", ([FromRoute] string code) => {
    var product = ProductRepository.GetBy(code);
    return product;
});

app.MapPost("/saveproduct", (Product product) => {
    ProductRepository.Add(product);
});

app.MapPut("/editproduct", (Product product) => {
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
});

app.MapDelete("/deleteproduct", ([FromRoute] string code) => {
    var product = ProductRepository.GetBy(code);
    ProductRepository.Remove(product);
});

app.Run();

//Classes estáticas sobrevivem a requisições sem persistência de dados.

public static class ProductRepository {

    public static List<Product>? Products { get; set; }

    public static void Add(Product product){
        if(Products == null)
        Products = new List<Product>();

        Products.Add(product);
    }

    public static Product GetBy(string code) {
        return Products.FirstOrDefault(p => p.Code == code);
    }

    public static void Remove(Product product){
        Products.Remove(product);
    }
}

public class Product {

    public string Code { get; set; }

    public string? Name { get; set; }

}