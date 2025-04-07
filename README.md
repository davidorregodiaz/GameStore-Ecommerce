# GameStore - Tienda de Videojuegos en ASP.NET Core MVC

Este es un proyecto de una tienda virtual de videojuegos desarrollado con ASP.NET Core MVC, siguiendo una arquitectura por capas (MVC) para mantener una separación clara de responsabilidades.

## Características

- Gestión de usuarios (registro, login, roles)
- CRUD de videojuegos y categorías
- Búsqueda y filtrado de juegos
- Carrito de compras
- Uso de Entity Framework Core + SQL Server
- Arquitectura modular con proyectos separados para Data, Domain, y Utilidades
- Uso de SignalR para comunicacion a tiempo real

## Estructura del Proyecto

/GameStore.sln
|-- GameStore.MVC              --> Proyecto principal MVC (Vistas + Controladores)
|-- GameStore.Domain           --> Entidades del dominio (Modelos)
|-- GameStore.Application      --> Lógica de negocio (Interfaces, DTOs, Servicios)
|-- GameStore.Infrastructure   --> Acceso a datos con Entity Framework Core
|-- GameStore.Utilities        --> Clases auxiliares, helpers, etc.

Tecnologías:

ASP.NET Core MVC
Entity Framework Core
C# 10 / .NET 6+
SQL Server
Bootstrap 5
Identity


Configuración Rápida

1. Clona el repositorio:
git clone https://github.com/tuusuario/GameStore.git

2. Configura la cadena de conexión en appsettings.json del proyecto MVC:
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=GameStoreDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}

3. Ejecuta las migraciones y actualiza la base de datos:
dotnet ef database update --project GameStore.Infrastructure --startup-project GameStore.MVC

4. Ejecuta la aplicación:
dotnet run --project GameStore.MVC

Próximas Mejoras:

Carrito de compras con pedidos y pasarela de pago simulada
API REST para consumir desde frontend moderno (Angular, React, etc)
Dockerización del proyecto
