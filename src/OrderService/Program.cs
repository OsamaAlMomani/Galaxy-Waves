using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok(new { status = "OrderService healthy" }));

app.MapGet("/orders", async (IHttpClientFactory httpClientFactory, IConfiguration config) =>
{
    var client = httpClientFactory.CreateClient();
    // Get JWT from ProductService
    var tokenResp = await client.PostAsJsonAsync("http://product-service:80/login", new { });
    var tokenObj = await tokenResp.Content.ReadFromJsonAsync<dynamic>();
    string token = tokenObj?.token;
    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    var productsResp = await client.GetAsync("http://product-service:80/products");
    var products = await productsResp.Content.ReadFromJsonAsync<object[]>();
    return Results.Ok(new[] {
        new { id = 1, productId = 1, quantity = 2, products }
    });
}).RequireAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
