# Sistema de Gestión de Tiquetes Aéreos (CLI)

Aplicación de consola en **C#/.NET** para gestionar un sistema básico de tiquetes aéreos: usuarios/roles, catálogos, reservas y procesos asociados, con una interfaz interactiva construida con **Spectre.Console** y persistencia en **MySQL** usando **Entity Framework Core**.

## Características

- **Interfaz de consola interactiva** (menús con flechas, tablas y formularios).
- **Persistencia en MySQL** con **EF Core**.
- **Migraciones** y comandos auxiliares (semillas/validaciones) desde `Program.cs`.
- **Seed idempotente** para usuario **ROOT** (admin) y permisos base.
- **Prompts cancelables** en formularios (por ejemplo: `0`, `c`, `cancelar`).

## Estructura del proyecto

El código vive bajo `src/` y está organizado por módulos en `src/Modules/`.

Estructura típica por módulo:

- **Domain**: reglas de negocio (agregados / value objects).
- **Application**: DTOs y casos de uso (Create/List/Get/Update/Delete).
- **Infrastructure**: entidades EF, configuraciones, repositorios y seeders por módulo.
- **UI**: pantallas de consola con menús estandarizados (Spectre.Console).

Además existe una carpeta `src/shared/` con helpers transversales (UI, `DbContextFactory`, formato de errores, etc.).

## Flujo general de ejecución

- `Program.cs` inicializa la consola (`SpectreUi.InitializeConsoleUi()`), crea el `DbContext` y, si hay conexión, permite:
  - ejecutar flags (`--migrate`, `--seed-defaults`, `--seed-root`, etc.)
  - iniciar sesión (`LoginShell`) y luego abrir el menú principal (`ApplicationShell`)

## Convenciones y reglas importantes

- **DB en `snake_case`**: tablas y columnas (MySQL).
- **Personas por documento**: el par `(document_type_id, document_number)` identifica una **única persona**; varios usuarios pueden apuntar a la misma persona.
- **Cancelación en UI**: los formularios permiten cancelar con `0`, `c` o `cancelar` y vuelven al menú sin romper el flujo.

## Tecnologías

- **.NET**: `net10.0`
- **ORM**: Entity Framework Core
- **DB**: MySQL (provider: `Pomelo.EntityFrameworkCore.MySql`)
- **UI**: `Spectre.Console`

## Requisitos

- **.NET SDK 10** (o el SDK compatible con `net10.0`)
- **MySQL 8.0+**

## Configuración

La app obtiene la cadena de conexión desde:

- **Variable de entorno** `MYSQL_CONNECTION` (prioridad), o
- `appsettings.json` → `ConnectionStrings:MySqlDB`

Ejemplo (PowerShell):

```powershell
$env:MYSQL_CONNECTION="server=localhost;port=3306;database=airlinesdb;user=root;password=TU_PASSWORD;"
```

## Guía rápida (para usarlo en otro computador)

Esta guía asume que solo quieres clonar/abrir el proyecto y ejecutar `dotnet run`.

### 1) Instalar y levantar MySQL

- Instala **MySQL 8.0+**.
- Asegúrate de que el servicio esté **ejecutándose** y escuchando en el puerto (por defecto `3306`).

### 2) Crear un usuario de MySQL (recomendado)

Usa un usuario que tenga permisos para:
- crear base de datos (solo la primera vez) y
- crear/alterar tablas (para migraciones).

Si ya usarás `root`, omite este paso.

### 3) Configurar la conexión

Opción A (recomendada): variable de entorno `MYSQL_CONNECTION`:

```powershell
$env:MYSQL_CONNECTION="server=localhost;port=3306;database=airlinesdb;user=root;password=TU_PASSWORD;"
```

Opción B: editar `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "MySqlDB": "server=localhost;port=3306;database=airlinesdb;user=root;password=TU_PASSWORD;"
  }
}
```

### 4) Ejecutar

```powershell
dotnet run
```

En el arranque, si hay conexión al servidor MySQL, la app ejecuta automáticamente (de forma **idempotente**):

- **Creación de base de datos si no existe** (`CREATE DATABASE IF NOT EXISTS`) cuando el servidor está accesible.
- **Migraciones EF Core** (`Migrate()`).
- **Seed de data default** (catálogos mínimos).
- **Seed de usuario ROOT** (admin) con contraseña **`12345`**.

> Si el servidor MySQL está apagado/no instalado, la app no puede funcionar y mostrará un mensaje de conexión fallida.

## Base de datos (MySQL)

1. Crea una base de datos (por ejemplo `airlinesdb`).
2. Configura la cadena de conexión (variable de entorno o `appsettings.json`).
3. Aplica migraciones y seeds según necesites.

### Migraciones

Para aplicar migraciones desde la app:

```powershell
dotnet run -- --migrate
```

Si prefieres usar el tooling de EF Core:

```powershell
dotnet ef database update
```

> Nota: el proyecto incluye `Microsoft.EntityFrameworkCore.Design` y `Microsoft.EntityFrameworkCore.Tools`.

### Seeds (datos mínimos)

- **Seeds de catálogos mínimos**:

```powershell
dotnet run -- --seed-defaults
```

- **Usuario ROOT** (idempotente, contraseña `12345`) y permisos base:

```powershell
dotnet run -- --seed-root
```

## Ejecutar el proyecto

Restaurar dependencias y compilar:

```powershell
dotnet restore
dotnet build
```

Ejecutar (modo interactivo):

```powershell
dotnet run
```

## Comandos útiles (flags)

Estos flags están implementados en `Program.cs`:

- **Aplicar migraciones**:

```powershell
dotnet run -- --migrate
```

- **Seed por defecto** (catálogos mínimos, idempotente):

```powershell
dotnet run -- --seed-defaults
```

- **Seed usuario ROOT** (idempotente, contraseña `12345`):

```powershell
dotnet run -- --seed-root
```

- **Validar mapeos EF vs columnas reales en MySQL**:

```powershell
dotnet run -- --validate-mappings
```

- **Describir columnas de una tabla**:

```powershell
dotnet run -- --describe-table=persons
```

## Troubleshooting

### Error MSB3027 / .exe en uso

Si al compilar aparece un error indicando que no se puede copiar el ejecutable porque está siendo usado por otro proceso, cierra el proceso en ejecución o finalízalo:

```powershell
taskkill /IM sistema-gestor-de-tiquetes-aereos.exe /F
```

Luego vuelve a ejecutar:

```powershell
dotnet build
```

### No conecta a MySQL

- Verifica que MySQL esté levantado y el puerto/usuario/contraseña sean correctos.
- Asegúrate de estar usando `MYSQL_CONNECTION` o `appsettings.json`.
- Si el usuario MySQL no tiene permisos, la creación automática de DB/migraciones puede fallar.
- Si hay dudas de mapeos (columnas que no existen), usa:

```powershell
dotnet run -- --validate-mappings
```

## Documentación adicional

- `PROMPTS-PROYECTO.md`: prompts guía para diseñar/implementar el sistema (DB 4FN, módulos, seeders, Application, debugging, UI).
- `ToDo.md`: backlog/tareas pendientes del proyecto.

## Autores

- Jeison
- Tomás Medina
- Alejandro Escobar

