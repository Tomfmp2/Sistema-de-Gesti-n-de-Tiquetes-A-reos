# ✈️ Sistema de Gestión de Tiquetes Aéreos (CLI)

Una solución empresarial robusta basada en la consola (**CLI**), escrita en **C# / .NET** con persistencia en **MySQL** (EF Core) y una interfaz de usuario interactiva y estilizada con **Spectre.Console**. El sistema está diseñado bajo una arquitectura limpia por capas y principios de Diseño Guiado por el Dominio (DDD).

---

## 👥 Autores y Desarrolladores
Este proyecto ha sido desarrollado por:
- 🧑‍💻 **Jeison Cristancho**
- 🧑‍💻 **Tomás Medina**
- 🧑‍💻 **Alejandro Escobar**

---

## 🎯 Características y Funciones Destacadas
Este sistema va más allá de un CRUD simple, ofreciendo una experiencia premium y funcionalidades avanzadas:

### 🏆 1. Sistema de Fidelización por Millas
Un completo programa de fidelización por puntos de vuelo con las siguientes características:
- ✈️ **Acumulación Automática**: Las millas se acreditan tras completar una reserva, calculadas según la distancia real de la ruta.
- 🎁 **Tramos de Redención Escalables**:
  - `25,000 millas` ➡️ 10% de descuento.
  - `80,000 millas` ➡️ 25% de descuento.
  - `150,000 millas` ➡️ 50% de descuento.
  - `500,000 millas` ➡️ ¡Vuelo gratis!
- 🛡️ **Reversión Automática**: Si no se completa el Check-in antes del despegue, las millas acumuladas y la reserva se revocan.
- 🎯 **Descuento Específico**: El beneficio aplica al primer tiquete de la reserva; los tiquetes adicionales pagan tarifa estándar.
- 📊 **Ranking de Viajeros**: Reportes analíticos de viajeros frecuentes con ranking detallado.

### 🏢 2. Gestión Operativa Integral
- 🗺️ **Rutas y Vuelos**: Gestión avanzada de trayectos, cálculo de distancias en kilómetros, duración estimada y millas generadas.
- 🛫 **Administración Aeroportuaria**: Catálogos de aerolíneas (IATA de 2-3 caracteres), aeropuertos, flotas, cabinas y configuración de asientos.
- 🎫 **Reservas y Tiquetaje**: Flujos completos desde la reserva de vuelos, asignación de asientos, pagos mediante múltiples métodos, emisión de tiquetes y Check-in con pase de abordar.
- 🧾 **Facturación**: Desglose detallado de impuestos, tarifas y ahorros aplicados por redención de puntos.

### 🖥️ 3. Interfaz de Usuario Avanzada (CLI)
- 🧭 **Menús Interactivos**: Navegación fluida por teclado con flechas direccionales gracias a **Spectre.Console**.
- ↩️ **Cancelación Consistente**: En cualquier formulario interactivo puedes cancelar la operación ingresando `0`, `c` o `cancelar`.
- 🔐 **Control de Acceso Basado en Roles (RBAC)**: Menús dinámicos y segregados por privilegios:
  - `ROOT` y `Administrador`: Acceso completo a catálogos y gestión total.
  - `Operaciones`: Módulos operativos diarios.
  - `Cliente`: Autogestión de reservaciones.

---

## ⚙️ Requisitos y Dependencias del Sistema

Para ejecutar el proyecto de forma local, es necesario cumplir con los siguientes requisitos previos:

### 🎒 1. SDK y Herramientas
| Dependencia | Versión Mínima | Propósito |
| :--- | :--- | :--- |
| 🛠️ **.NET SDK** | `net10.0` (10.0+) | Compilación y ejecución de la aplicación. |
| 🗄️ **MySQL Server** | `8.0` o superior | Motor de base de datos relacional. |
| 🚚 **Git** | Cualquier versión | Clonación y control de versiones. |

### 🛠️ Comandos de instalación del SDK de .NET 10
#### **Windows (vía winget)**
```powershell
winget install Microsoft.DotNet.SDK.10
```

#### **macOS (vía Homebrew)**
```bash
brew install --cask dotnet-sdk
```

#### **Linux (Ubuntu/Debian)**
```bash
wget https://dot.net/v1/dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 10.0
```

> [!TIP]
> Verifica la instalación exitosa ejecutando `dotnet --version` en la terminal. Debe retornar `10.x.x`.

---

## 🚀 Guía de Configuración y Ejecución

Sigue estos pasos para poner a funcionar el proyecto en tu propia máquina.

### 🔑 1. Configurar la Conexión a MySQL
Para que el sistema se comunique con tu base de datos local, debes ajustar la cadena de conexión. Tienes dos opciones para hacerlo:

#### **Opción A: Variable de Entorno (Recomendada)**
Configura la variable `MYSQL_CONNECTION` en tu terminal antes de ejecutar.
- **En PowerShell (Windows):**
  ```powershell
  $env:MYSQL_CONNECTION="server=localhost;port=3306;database=airlinesdb;user=root;password=TU_PASSWORD;"
  ```
- **En Bash (macOS/Linux):**
  ```bash
  export MYSQL_CONNECTION="server=localhost;port=3306;database=airlinesdb;user=root;password=TU_PASSWORD;"
  ```

#### **Opción B: Archivo `appsettings.json`**
Abre el archivo [appsettings.json](./appsettings.json) y actualiza el valor:
```json
{
  "ConnectionStrings": {
    "MySqlDB": "server=localhost;port=3306;database=airlinesdb;user=root;password=TU_PASSWORD;"
  }
}
```

---

### 📥 2. Primera Ejecución (Instalación y Bootstrap)
El proyecto incluye un mecanismo de **Bootstrap automático** que crea la base de datos, aplica las migraciones y carga los datos maestros (seeds) de forma inicial.

Ejecuta los siguientes comandos en la raíz del proyecto:
```powershell
# 1. Restaurar dependencias de NuGet
dotnet restore

# 2. Compilar el proyecto
dotnet build

# 3. Ejecutar por primera vez
dotnet run
```

---

### 🔄 3. Ejecuciones Posteriores
Una vez que la base de datos y los datos semilla ya han sido creados, puedes iniciar la aplicación normalmente con:
```powershell
dotnet run
```

Si necesitas forzar una migración de base de datos manual o actualizar los seeds en ejecuciones posteriores, puedes usar los siguientes parámetros especiales:
```powershell
# Aplicar migraciones pendientes manualmente
dotnet run -- --migrate

# Volver a cargar semillas de catálogo base (idempotente)
dotnet run -- --seed-defaults

# Volver a crear usuario ROOT por defecto
dotnet run -- --seed-root
```

---

## 👤 Cuentas de Acceso por Defecto
El sistema pre-carga usuarios de prueba para que explores todas las características de inmediato.

> [!IMPORTANT]
> **Contraseña universal de todas las cuentas por defecto: `12345`**

### 🔑 Administradores
| Usuario | Rol | Descripción |
| :--- | :--- | :--- |
| `ROOT` | Administrador total | Acceso total a todos los módulos y opciones. |

### ✈️ Clientes de Demostración
Clientes con millas cargadas y reservas activas para validar el sistema de fidelización:
| Usuario | Nombre | Millas Acumuladas |
| :--- | :--- | :--- |
| `valentina.rios` | Valentina Ríos | `95,000` |
| `carlos.mendoza` | Carlos Mendoza | `230,000` |
| `luisa.herrera` | Luisa Herrera | `42,500` |
| `sebastian.castro` | Sebastián Castro | `510,000` |
| `mariana.torres` | Mariana Torres | `18,750` |

---

## 🏗️ Arquitectura del Software
El código sigue una estructura inspirada en la arquitectura limpia (Clean Architecture) orientada a dominios:
```
src/
├── Modules/                  # Módulos del sistema
│   └── <NombreDelModulo>/    # Cada módulo está aislado
│       ├── Domain/           # Agregados, Value Objects y reglas de negocio
│       ├── Application/      # Casos de uso (Use Cases) y DTOs
│       ├── Infrastructure/   # Entidades de base de datos, repositorios y seeds
│       └── UI/               # Interfaz de consola con Spectre.Console
└── shared/                   # Componentes transversales
    ├── Context/              # AppDbContext y Unit of Work
    ├── Helpers/              # Utilidades compartidas (Formatos, Menús)
    └── Ui/                   # LoginShell y ApplicationShell
```

---

## 🛠️ Herramientas de Diagnóstico
La aplicación incluye comandos en línea útiles para el mantenimiento:
- **Validar mapeos de EF Core con la DB real:**
  ```powershell
  dotnet run -- --validate-mappings
  ```
- **Describir columnas de una tabla:**
  ```powershell
  dotnet run -- --describe-table=persons
  ```

---

## 📄 Documentación Relacionada
- 🏆 **[Informe de Fidelización de Millas](./Informe_Fidelizacion.md)**: Reporte detallado del programa de fidelización de puntos de vuelo.
- 📝 **[PROMPTS-PROYECTO.md](./PROMPTS-PROYECTO.md)**: Notas de ingeniería y prompts utilizados.


