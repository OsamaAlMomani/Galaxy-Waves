using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

var routes = new[]
{
    new RouteConfig
    {
        RouteId = "products",
        ClusterId = "productCluster",
        Match = new RouteMatch { Path = "/products{**catch-all}" }
    },
    new RouteConfig
    {
        RouteId = "orders",
        ClusterId = "orderCluster",
        Match = new RouteMatch { Path = "/orders{**catch-all}" }
    }
};

var clusters = new[]
{
    new ClusterConfig
    {
        ClusterId = "productCluster",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            { "dest1", new DestinationConfig { Address = "http://product-service:80/" } }
        }
    },
    new ClusterConfig
    {
        ClusterId = "orderCluster",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            { "dest1", new DestinationConfig { Address = "http://order-service:80/" } }
        }
    }
};

builder.Services.AddReverseProxy().LoadFromMemory(routes, clusters);

var app = builder.Build();
app.MapGet("/health", () => Results.Ok(new { status = "Gateway healthy" }));
app.MapReverseProxy();
app.Run();
