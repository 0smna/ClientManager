1. Integrantes finales del grupo
FERNANDEZ ARIAS KEMBLY STEFY
LEIVA SANCHEZ ALLAN ANDREY
TOKUC OSMAN EMRE
VILLAPLANA NAVARRO KENNETH

2. Enlace del repositorio
https://github.com/0smna/ClientManager.git

3. Especificación básica del proyecto
a. Arquitectura del proyecto
El proyecto ClienteManager tiene tres partes y un proyecto web MVC:

ClientManager (MVC)

Controllers: ClienteController.cs, HomeController.cs

Views:

Clientes: _Crear.cshtml, _Editar.cshtml, Index.cshtml

Home: Index.cshtml

Compartido: _Layout.cshtml, _ValidationScriptsPartial.cshtml, Error.cshtml

wwwroot:


JS: Cliente.js, 

Librerías externas: Bootstrap, DataTables, SweetAlert2

ClientManagerBLL (lógica de negocio)

Dtos: ClienteDto.cs, CustomResponse.cs

Servicios: ClienteServicio.cs, IClienteServicio.cs

Mapeo: MapeoClases.cs (AutoMapper)

ClientManagerDAL (acceso a datos)

Entities: Cliente.cs

Repositorios: ClienteRepositorio.cs, IClienteRepositorio.cs

b. Librerías o paquetes usados
Microsoft.AspNetCore.Mvc – Para MVC

AutoMapper – Para convertir DTOs y entidades

jQuery – Para AJAX y manipular HTML

Bootstrap – Para diseño responsivo

DataTables.js – Para tablas dinámicas

SweetAlert2 – Para alertas bonitas

AutoMapper.Extensions.Microsoft.DependencyInjection - para integrar AutoMapper con el sistema de inyección de dependencias de ASP.NET Core.

c. Principios y patrones usados

Patrones de diseño:

Repository Pattern: Todo acceso a datos en ClienteRepositorio.

Dependency Injection: Inyectamos servicios en controladores (IClienteServicio).

DTO Pattern: ClienteDto para separar datos de la lógica.

MVC: Separación de modelo, vista y controlador.