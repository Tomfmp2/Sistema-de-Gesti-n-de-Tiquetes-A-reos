# Sistema de Gestión de Tiquetes Aéreos (CLI)

Aplicación de consola en **C#/.NET** para gestionar un sistema de tiquetes aéreos con persistencia en **MySQL** (EF Core) y una interfaz interactiva construida con **Spectre.Console**.

Incluye módulos de catálogos (aerolíneas, aeropuertos, rutas, flota, etc.), operación (vuelos, asignaciones, equipaje), reservas y procesos asociados (tickets, check-ins, pagos, facturación) con un enfoque modular por capas.

## Qué es este proyecto

- **Tipo**: aplicación de consola (CLI) con menús, tablas y formularios.
- **Persistencia**: MySQL + EF Core (migraciones y seed idempotente).
- **Arquitectura**: módulos por dominio bajo `src/Modules/` con capas `Domain / Application / Infrastructure / UI`.
- **Objetivo**: servir como base académica/práctica para un sistema de reservas y tiquetaje, manteniendo buenas prácticas (DDD ligero, validaciones por value objects y casos de uso).

## Características principales

- **UI interactiva**: navegación con flechas, tablas, prompts y mensajes.
- **Cancelación consistente**: en formularios puedes cancelar con `0`, `c` o `cancelar`.
- **Bootstrap automático**: al iniciar, intenta:
  - crear la DB si falta (cuando el servidor está accesible),
  - aplicar migraciones,
  - ejecutar seeds mínimos,
  - asegurar el usuario **ROOT** y permisos base (idempotente).
- **Menú por rol/permisos**:
  - ROOT/Admin ven catálogos y operación.
  - Cliente ve operaciones sobre sus reservaciones.
  - La visibilidad de módulos puede depender de permisos (`role_permissions`).
- **Herramientas de diagnóstico**: describe tablas, valida mapeos EF vs DB real.

## Requisitos

- **.NET SDK** compatible con `net10.0`.
- **MySQL 8.0+**.

## Configuración (conexión a MySQL)

La aplicación obtiene la cadena de conexión desde:

1. Variable de entorno **`MYSQL_CONNECTION`** (prioridad), o
2. `appsettings.json` → `ConnectionStrings:MySqlDB`

Ejemplo en PowerShell:

```powershell
$env:MYSQL_CONNECTION="server=localhost;port=3306;database=airlinesdb;user=root;password=TU_PASSWORD;"
```

### Recomendación de permisos en MySQL

El usuario debe poder:
- crear DB (solo si quieres auto-creación),
- crear/alterar tablas (migraciones),
- leer/escribir datos (operación normal).

## Cómo ejecutar (paso a paso)

Desde la raíz del repo:

```powershell
dotnet restore
dotnet build
dotnet run
```

En el primer arranque con conexión válida, el sistema aplicará migraciones + seeds automáticamente (idempotente).

## Inicio de sesión, roles y permisos

### Usuario ROOT

- Se garantiza por seed (idempotente).
- Usuario: `ROOT`
- Contraseña: `12345`

### Roles típicos

- **ROOT / Administrador**: gestión completa de catálogos y operación.
- **Cliente**: gestiona *sus* reservaciones y operaciones relacionadas.

### Permisos (alto nivel)

La app usa permisos (tabla `permissions`) y asignaciones por rol (tabla `role_permissions`) para habilitar apartados como:

- `catalogs.manage` (catálogos)
- `flights.manage` (vuelos)
- `fares.manage` (tarifas)
- `payments.manage` (pagos)
- `tickets.manage` (tickets)
- `checkins.manage` (check-ins)
- `invoices.manage` (facturas)
- `security.manage` (roles/permisos)

> Si tu DB no tiene permisos poblados, el sistema mantiene compatibilidad mostrando módulos de admin por defecto (según configuración del menú).

## Cómo usarlo (guía rápida por menús)

### Cliente

- **Reservaciones**: crear, listar, confirmar, cambiar expiración, cancelar y realizar check-in (según estado).

### Administrador / ROOT

Apartados comunes:

- **Catálogos / Operación**: aerolíneas, aeropuertos, rutas, temporadas, flota, cabinas, asientos, tripulación, equipaje.
- **Vuelos**: crear/editar/listar (código, ruta, avión, fechas, cupos, estado).
- **Tarifas**: precios por ruta/cabina/pasajero/temporada.
- **Pagos**: registrar/consultar pagos asociados a reservaciones.
- **Tickets**: emisión/consulta/actualización.
- **Check-ins**: registro de check-in y pase de abordar.
- **Facturas**: emisión y consulta.
- **Seguridad**: administrar roles, permisos y asignaciones `Rol ↔ Permiso`.

#### Búsqueda optimizada de Países (IDs)

Para formularios que piden `CountryId`, existe un apartado:
- **“Países (por continente)”**: primero lista **Continentes (Id/Nombre)**, luego permite filtrar y listar **Países (Id/Nombre/ISO)**.
- También incluye **búsqueda rápida por texto** (Nombre/ISO) para ubicar IDs sin navegar por continentes.

## Arquitectura (estructura del código)

El código vive bajo `src/`:

- `src/Modules/<ModuleName>/`
  - `Domain/`: agregados y value objects (validaciones/ reglas).
  - `Application/`: DTOs y casos de uso (Create/Get/List/Update/Delete).
  - `Infrastructure/`: entidades EF, configuraciones, repositorios, seeds.
  - `UI/`: pantallas de consola (Spectre.Console) y flujos de menú.
- `src/shared/`: helpers transversales (`SpectreUi`, `MenuLogic`, `DbContextFactory`, shells).

Flujo general:

1. `Program.cs` inicializa UI y DB.
2. `LoginShell` autentica y crea sesión.
3. `ApplicationShell` arma el menú según rol/permisos.
4. Cada módulo UI usa casos de uso/repo del módulo.

## Comandos útiles (flags)

Estos flags están implementados en `Program.cs`:

- Aplicar migraciones:

```powershell
dotnet run -- --migrate
```

- Seed por defecto (catálogos mínimos, idempotente):

```powershell
dotnet run -- --seed-defaults
```

- Seed usuario ROOT (idempotente, contraseña `12345`):

```powershell
dotnet run -- --seed-root
```

- Validar mapeos EF vs columnas reales en MySQL:

```powershell
dotnet run -- --validate-mappings
```

- Describir columnas de una tabla:

```powershell
dotnet run -- --describe-table=persons
```

## Troubleshooting

### Error MSB3027 / `.exe` en uso (Windows)

Si al compilar aparece que el ejecutable está “en uso”, finaliza el proceso:

```powershell
taskkill /IM sistema-gestor-de-tiquetes-aereos.exe /F
```

Y luego:

```powershell
dotnet build
```

### No conecta a MySQL

- Verifica que MySQL esté encendido y escuchando en el puerto.
- Confirma usuario/contraseña en `MYSQL_CONNECTION` o `appsettings.json`.
- Si hay errores de columnas/tablas, usa:

```powershell
dotnet run -- --validate-mappings
```

## Documentación adicional

- `PROMPTS-PROYECTO.md`: guía de prompts/diseño/implementación.
- `ToDo.md`: backlog del proyecto.

## Autores

- Jeison Cristancho
- Tomás Medina
- Alejandro Escobar

