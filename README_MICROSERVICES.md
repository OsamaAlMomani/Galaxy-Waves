# Galaxy-Waves Microservices Starter

This workspace now contains a minimal microservices starter with two services:

- `ProductService` — minimal products API at `/products` and `/health` (port 5001)
- `OrderService` — minimal orders API at `/orders` and `/health` (port 5002)

Build and run locally (dotnet):
```powershell
dotnet build
dotnet run --project src\ProductService\ProductService.csproj
dotnet run --project src\OrderService\OrderService.csproj
```

Using Docker Compose (recommended):
```powershell
# from repo root
docker-compose build
docker-compose up --remove-orphans
```

Endpoints:
- http://localhost:5001/products
- http://localhost:5001/health
- http://localhost:5002/orders
- http://localhost:5002/health
Gateway (single entry):
- http://localhost:8085/ (routes `/products` and `/orders` through the gateway)

Next steps I can do for you:
- Add a gateway (API gateway / reverse proxy) to expose a single public port.
- Add a shared library and an example message bus (RabbitMQ) integration.
- Add CI to build images and run tests.