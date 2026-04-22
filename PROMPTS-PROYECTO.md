# Prompts guía — Sistema de Gestión de Tiquetes Aéreos (C#/.NET + MySQL + EF Core + Spectre.Console)

Este documento contiene **6 prompts** (para usar con un asistente/IA) que describen cómo **diseñar, construir, poblar, depurar y presentar** el proyecto siguiendo la **estructura modular** y el estilo usado en este repositorio.

> Nota: Los prompts están redactados para obtener salidas **concretas** (modelo de datos, estructura de módulos, seeders, use cases, UI) y con **criterios verificables**.

---

## Prompt 1 — Diseño y normalización (hasta 4FN)

**Rol**: Eres un arquitecto de datos y analista de sistemas experto en normalización (1FN–4FN) para MySQL.

**Objetivo**: Diseñar el modelo relacional completo para un sistema de gestión de tiquetes aéreos y entregar un diseño **normalizado hasta 4FN**, usando convenciones `snake_case` y claves/relaciones correctas.

**Contexto del dominio (mínimo)**:
- Usuarios, personas (documento), roles, permisos.
- Clientes (relacionados a persona).
- Aerolíneas, aeropuertos, rutas, vuelos.
- Reservas, estados de reserva, pagos, facturas (si aplica), tickets, check-in, asientos.
- Contactos: emails y teléfonos (múltiples por persona).
- Direcciones (para facturación u otros usos).

**Reglas de negocio obligatorias**:
- **Una persona** se identifica de forma única por **(document_type_id, document_number)**.
- **Varios usuarios** pueden apuntar a la **misma persona** (no duplicar personas).
- Emails y teléfonos pueden ser **múltiples** por persona.
- Evitar dependencias multivaluadas dentro de una misma tabla; separar entidades si hay grupos repetibles.

**Requisitos técnicos**:
- Usar **MySQL 8+**.
- Todas las tablas y columnas en **`snake_case`**.
- Incluir: PK, FK, índices, constraints (UNIQUE/CHECK si aplica).
- Definir entidades “catálogo” (estados, tipos, dominios, códigos telefónicos, etc.) cuando sea necesario.
- Asegurar integridad referencial y borrados/actualizaciones coherentes (RESTRICT/CASCADE según criterio).

**Salida esperada (bien estructurada)**:
1. **Listado de entidades** con breve descripción.
2. **Esquema relacional** por tabla:
   - nombre de tabla
   - columnas (tipo, nullability, default)
   - PK
   - FKs (tabla/columna destino, on delete/on update)
   - UNIQUE constraints
   - índices recomendados
3. **Justificación de normalización**:
   - cómo cumple 1FN, 2FN, 3FN, BCNF (si aplica) y **4FN**
   - qué multivaluadas se separaron y por qué
4. (Opcional) **Diagrama textual** de relaciones (ej. Mermaid ERD en texto).

**Criterios de aceptación**:
- No hay listas/arrays en columnas (emails/teléfonos/direcciones modelados en tablas propias).
- La regla de unicidad de persona por documento existe como constraint.
- El diseño no mezcla conceptos distintos en una sola tabla (por ejemplo: datos de pago dentro de reservas sin relación clara).

---

## Prompt 2 — Creación de un módulo (estructura usada en el proyecto)

**Rol**: Eres un tech lead C# y arquitecto de software que aplica arquitectura modular por “Modules” y capas.

**Objetivo**: Crear un **módulo nuevo** en un proyecto de consola .NET con EF Core, siguiendo estrictamente la estructura existente bajo `src/Modules/<Modulo>/...`.

**Entradas**:
- Nombre del módulo (singular/plural): `<Modulo>`
- Entidad principal del módulo: `<Entity>`
- Tabla objetivo en MySQL: `<table_name>` (snake_case)
- Campos y reglas: lista de propiedades con validaciones

**Requisitos de estructura** (mínimo, ajusta si ya existe una convención en el repo):
- `src/Modules/<Modulo>/Domain/`:
  - `Aggregate/` (o entidad de dominio)
  - `ValueObjects/` (si aplica)
- `src/Modules/<Modulo>/Application/`:
  - `Dtos/` (requests/responses)
  - `UseCases/` (Create/List/GetById/Update/Delete)
- `src/Modules/<Modulo>/Infrastructure/`:
  - `Entity/` (Entity Framework Entity)
  - `Configuration/` (IEntityTypeConfiguration)
  - `repository/` (repositorio, si aplica)
  - `Data/` (seeders idempotentes, si aplica)
- `src/Modules/<Modulo>/UI/`:
  - `<Modulo>ConsoleUI.cs` con menú estandarizado

**Convenciones obligatorias**:
- Tablas/columnas `snake_case` y mapeo explícito en configuración EF.
- `Nullable` habilitado: evitar warnings con validación y null checks.
- Uso de helpers existentes:
  - menús: `MenuLogic.RunMenu`
  - prompts: `SpectreUi.Prompt...Cancelable`
  - tablas: `SpectreUi.ShowTable`
  - errores: `ExceptionFormatting.GetDiagnosticMessage(ex)`

**Salida esperada**:
- Árbol de archivos a crear.
- Código base (clases mínimas) para:
  - entidad de dominio
  - DTOs
  - UseCases CRUD
  - EF Entity + Configuration (mapeo snake_case)
  - UI con menú
- Instrucciones de integración:
  - registrar en el “shell”/menú principal si aplica
  - registrar DbSet en `AppDbContext` si aplica

**Criterios de aceptación**:
- Compila (`dotnet build`) y el módulo aparece en la UI donde corresponda.
- Operaciones CRUD funcionan con prompts cancelables.

---

## Prompt 3 — Seeders y datos por defecto (funcionamiento del sistema)

**Rol**: Eres un ingeniero backend especialista en EF Core + MySQL y bootstrapping idempotente.

**Objetivo**: Implementar **seeders idempotentes** que garanticen que la aplicación puede arrancar y funcionar con catálogos mínimos, sin duplicar registros al ejecutarse múltiples veces.

**Entradas**:
- Lista de catálogos/maestros requeridos (ej.: estados de reserva, roles, permisos, tipos de documento, etc.)
- Registros por defecto por catálogo (id opcional, nombre, descripción)
- Reglas de idempotencia (por clave natural: `name`, `code`, etc.)

**Requisitos técnicos**:
- Seeder por módulo: `src/Modules/<Modulo>/Infrastructure/Data/<Algo>Seeder.cs`
- Exponer un método `EnsureAsync(AppDbContext context)` por seeder.
- Operar con:
  - `AsNoTracking()` para lecturas
  - `AnyAsync/FirstOrDefaultAsync` para verificar existencia
  - Inserts condicionados
- Evitar `HasData` si la DB puede venir de SQL legacy; preferir **inserciones seguras** (y opcionalmente SQL raw controlado).
- Debe existir un seeder/orquestador global (ej. `Program.cs --seed-defaults`) que llame a los seeders necesarios.

**Salida esperada**:
- Lista de seeders a crear y su responsabilidad.
- Código de cada seeder (idempotente).
- Comando/flag para ejecutar seed (ej. `dotnet run -- --seed-defaults`).
- Qué claves naturales usa cada seeder para evitar duplicados.

**Criterios de aceptación**:
- Ejecutar seed 2+ veces no crea duplicados.
- El sistema puede iniciar sesión y navegar menús básicos después del seed.

---

## Prompt 4 — Capa Application (UseCases) dentro de módulos

**Rol**: Eres un desarrollador senior C# aplicando casos de uso (Application layer) con EF Core.

**Objetivo**: Crear la capa `Application` para un módulo, con **DTOs y UseCases** claros, transaccionales cuando corresponda, y con validación.

**Entradas**:
- Módulo: `<Modulo>`
- Entidad principal: `<Entity>`
- Reglas de negocio y validaciones
- Operaciones requeridas:
  - Crear
  - Listar (con filtros/paginación si aplica)
  - Consultar por ID
  - Actualizar
  - Eliminar (soft delete si aplica)

**Requisitos**:
- UseCases bajo `src/Modules/<Modulo>/Application/UseCases/`.
- DTOs bajo `src/Modules/<Modulo>/Application/Dtos/`.
- Manejo de errores:
  - lanzar `InvalidOperationException`/`ArgumentException` con mensajes útiles
  - en UI, mostrar con `ExceptionFormatting.GetDiagnosticMessage(ex)`
- Transacciones:
  - cuando haya múltiples inserts/updates relacionados, usar transacción EF.
- Salidas ricas para UI:
  - devolver DTO de respuesta con los campos necesarios para mostrar tablas/fichas.

**Salida esperada**:
- Lista de DTOs (Request/Response) y su forma.
- Código de UseCases con firmas `Task<...>`.
- Validaciones aplicadas (en dominio o en use case, justificar).

**Criterios de aceptación**:
- UseCases reutilizables desde distintas UIs.
- No hay lógica de presentación en Application.
- Compila y pasa un “smoke test” manual de CRUD.

---

## Prompt 5 — Debug y análisis de errores (compilación, runtime, datos)

**Rol**: Eres un especialista en debugging de .NET/EF Core/MySQL con enfoque sistemático.

**Objetivo**: Investigar y resolver errores de compilación, warnings críticos y fallos en runtime (conexión, mapeo EF, tracking, datos inconsistentes), dejando el proyecto en estado estable.

**Entradas**:
- Log de error completo (stacktrace, mensajes)
- Comando ejecutado (`dotnet build`, `dotnet run`, etc.)
- Contexto: módulo/pantalla donde ocurre

**Protocolo de debugging (obligatorio)**:
1. Reproducir el error con un comando mínimo.
2. Identificar tipo:
   - compilación (CSxxxx)
   - EF mapping (“Unknown column”, tabla inexistente)
   - tracking (`InvalidOperationException: already tracked`)
   - conexión MySQL (SSL, timeout, transport)
   - datos (FK faltante, catálogo incompleto)
3. Proponer hipótesis y validar con evidencia (código/consulta/log).
4. Aplicar fix con el menor cambio posible.
5. Verificar: recompilar y repetir el flujo.

**Herramientas y técnicas esperadas**:
- `dotnet build` para verificar estado.
- Revisión de `DbContextFactory` y connection string.
- Comparación de modelo EF vs DB (si existe flag `--validate-mappings`, usarlo).
- Para tracking: preferir `ExecuteUpdateAsync`/`AsNoTracking`/evitar `Attach` de stubs cuando hay entidad ya cargada.

**Salida esperada**:
- Diagnóstico (causa raíz).
- Fix propuesto (archivos a tocar).
- Pasos de verificación.
- (Opcional) Recomendaciones para evitar recurrencia.

**Criterios de aceptación**:
- Error original no reaparece.
- No se introducen nuevos errores.

---

## Prompt 6 — UI y menús (Spectre.Console, patrón estándar)

**Rol**: Eres un especialista en UX para CLI y en Spectre.Console.

**Objetivo**: Implementar una UI de consola consistente para cada módulo, con **menú estándar**, prompts cancelables, tablas/fichas para mostrar datos, y manejo amigable de errores.

**Estándar obligatorio de UI**:
- `SpectreUi.ModuleHeader("<Título>", "<Subtítulo opcional>")`
- Menú con `MenuLogic.RunMenu` y opciones en orden:
  - **Crear**
  - **Listar**
  - **Consultar por ID**
  - **Actualizar**
  - **Eliminar**
  - **Volver**
- Entradas por prompts:
  - `SpectreUi.PromptRequiredCancelable(...)`
  - `SpectreUi.PromptOptionalCancelable(...)`
  - y variantes `Int/Bool` cancelables si existen
- Salida:
  - listados en `SpectreUi.ShowTable`
  - detalle tipo ficha: tabla `Campo/Valor`
- Cancelación universal:
  - el usuario puede escribir `0`, `c` o `cancelar` en los prompts → se lanza `OperationCanceledException`
  - capturar `OperationCanceledException` para volver al menú sin stacktrace
- Errores:
  - capturar `Exception ex` y mostrar `ExceptionFormatting.GetDiagnosticMessage(ex)`

**Entradas**:
- Módulo y sus UseCases disponibles
- Campos a solicitar y validaciones

**Salida esperada**:
- Código completo del `<Modulo>ConsoleUI.cs` con el menú estándar.
- Implementaciones de acciones (Create/List/Get/Update/Delete) usando los UseCases.
- Ejemplos de tablas mostradas (headers y filas).

**Criterios de aceptación**:
- La UI es consistente con otros módulos.
- No hay `Console.ReadLine()` ni `Console.WriteLine()` “sueltos” para interacción principal (usar helpers).
- El usuario puede cancelar cualquier formulario sin romper el flujo.

