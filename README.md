# ✈️ Sistema de Gestión de Tiquetes Aéreos (CLI)

Aplicación de consola en **C#/.NET** para gestionar un sistema de tiquetes aéreos con persistencia en **MySQL** (EF Core) y una interfaz interactiva construida con **Spectre.Console**.

Incluye módulos de catálogos (aerolíneas, aeropuertos, rutas, flota, etc.), operación (vuelos, asignaciones, equipaje), reservas y procesos asociados (tickets, check-ins, pagos, facturación) con un enfoque modular por capas.

---

## 🏆 Implementación Destacada: Sistema de Fidelización por Millas

> 📄 **[Ver documentación completa del Sistema de Millas → `Informe_Fidelizacion.md`](./Informe_Fidelizacion.md)**

Como parte del desarrollo del sistema, se implementó un **programa completo de fidelización por puntos de vuelo** con las siguientes capacidades:

| Característica | Descripción |
|---|---|
| ✈️ **Acumulación automática** | Las millas se acreditan al completar la reserva, calculadas por distancia de ruta |
| 🎁 **Tramos de redención** | 25k → 10% · 80k → 25% · 150k → 50% · 500k → vuelo gratis |
| 🛡️ **Reversión automática** | Si no se hace check-in antes del despegue, las millas y la reserva se revocan automáticamente |
| 🎯 **Descuento en 1 tiquete** | El beneficio de millas aplica solo al primer tiquete; el resto pagan precio normal |
| 📊 **Ranking de viajeros** | Reporte analítico de viajeros frecuentes con millas acumuladas y vuelos completados |
| 📋 **Recibo detallado** | Al finalizar la reserva se muestra un desglose por tiquete con ahorro, millas usadas y millas ganadas |

---

## 👤 Usuarios por defecto

Estos usuarios se crean automáticamente al iniciar la aplicación por primera vez (seed idempotente). Todos usan la misma contraseña.

> 🔑 **Contraseña de todos los usuarios: `12345`**

### Administrador

| Usuario | Contraseña | Rol |
|---|---|---|
| `ROOT` | `12345` | Administrador (acceso total) |

### Clientes de demostración

> Estos clientes tienen millas acumuladas y reservas confirmadas para que los reportes del módulo de fidelización muestren datos reales desde el primer arranque.

| Usuario | Contraseña | Nombre | Millas acumuladas | Saldo actual |
|---|---|---|---|---|
| `valentina.rios` | `12345` | Valentina Ríos | 95.000 | 95.000 |
| `carlos.mendoza` | `12345` | Carlos Mendoza | 230.000 | 80.000 |
| `luisa.herrera` | `12345` | Luisa Herrera | 42.500 | 42.500 |
| `sebastian.castro` | `12345` | Sebastián Castro | 510.000 | 10.000 |
| `mariana.torres` | `12345` | Mariana Torres | 18.750 | 18.750 |

---

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
- **Menú por rol**:
  - **ROOT**: menú administrativo + “Mis reservaciones”.
  - **Administrador**: menú único “Administración aeroportuaria” (ahí están los módulos administrativos).
  - **Operaciones**: menú “Operación aeroportuaria” (módulos operativos).
  - **Cliente**: solo sus reservaciones.
- **Validaciones de datos**:
  - No permite crear **aerolíneas duplicadas** (por nombre o por IATA).
  - IATA de **aerolíneas** acepta **2 o 3** caracteres (IATA de **aeropuertos** sigue siendo de 3).
- **Prompts más amigables**:
  - Donde aplique, se solicita **nombre** de país/ciudad en lugar de Id.
- **Herramientas de diagnóstico**: describe tablas, valida mapeos EF vs DB real.

## ⚙️ Requisitos

| Requisito | Versión mínima |
|---|---|
| **.NET SDK** | **10.0** (`net10.0`) |
| **MySQL** | 8.0+ |

### Instalar .NET 10 SDK

Si aún no tienes el SDK instalado, descárgalo desde la página oficial o usa los siguientes comandos:

**Windows (winget):**
```powershell
winget install Microsoft.DotNet.SDK.10
```

**macOS (Homebrew):**
```bash
brew install --cask dotnet-sdk
```

**Linux (script oficial):**
```bash
wget https://dot.net/v1/dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 10.0
```

Verifica la instalación con:
```powershell
dotnet --version
# Debe mostrar 10.x.x
```

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

- **ROOT**: usuario especial (seed idempotente) con acceso a módulos administrativos y “Mis reservaciones”.
- **Administrador**: rol de administración. En consola ve el menú “Administración aeroportuaria”.
- **Operaciones**: rol operativo. En consola ve el menú “Operación aeroportuaria”.
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

> Nota: la UI principal se guía principalmente por rol (ROOT/Administrador/Operaciones/Cliente). Los permisos se usan como base del modelo de seguridad en la BD.

## Cómo usarlo (guía rápida por menús)

### Cliente

- **Reservaciones**: crear, listar, confirmar, cambiar expiración, cancelar y realizar check-in (según estado).

### Administrador / ROOT

#### ROOT

- Menú administrativo + “Mis reservaciones”.

#### Administrador

- Menú principal: **Administración aeroportuaria**.

Dentro de **Administración aeroportuaria** (según módulos disponibles en el proyecto):

- **Catálogos / Operación**: aerolíneas, aeropuertos, rutas, temporadas, flota, cabinas, asientos, tripulación, equipaje.
- **Vuelos**: crear/editar/listar (código, ruta, avión, fechas, cupos, estado).
- **Tarifas**: precios por ruta/cabina/pasajero/temporada.
- **Pagos**: registrar/consultar pagos asociados a reservaciones.
- **Tickets**: emisión/consulta/actualización.
- **Check-ins**: registro de check-in y pase de abordar.
- **Facturas**: emisión y consulta.
- **Usuarios**: gestión de usuarios.

### Operaciones

- Menú principal: **Operación aeroportuaria**.
- Incluye módulos operativos: vuelos, tarifas, reservaciones (consulta/gestión), pagos, tickets, check-ins, facturas.

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

## 📚 Documentación adicional

- 🏆 [`Informe_Fidelizacion.md`](./Informe_Fidelizacion.md): documentación técnica completa del sistema de millas y fidelización.
- `PROMPTS-PROYECTO.md`: guía de prompts/diseño/implementación.
- `ToDo.md`: backlog del proyecto.

## Autores

- Jeison Cristancho
- Tomás Medina
- Alejandro Escobar

