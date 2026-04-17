## ToDo (Persona 1) — Sistema de Gestión de Tiquetes Aéreos

Este ToDo está organizado por **hitos** y **prioridad** para que puedas avanzar sin bloquearte.

### Convenciones

- **P0**: imprescindible para que el módulo funcione end-to-end
- **P1**: mejora importante / endurece reglas / reportes
- **P2**: nice-to-have
- **Definición de hecho (DoD)**: cada ítem “hecho” debe tener (a) migración aplicada o script equivalente, (b) flujo de consola usable, (c) validaciones mínimas, (d) un commit claro.

---

### Alcance (tablas del módulo)

- **Geografía y direcciones**
  - `continentes`, `paises`, `regiones`, `ciudades`
  - `tipos_via`, `direcciones`
- **Personas y contactos**
  - `tipos_documento`, `personas`
  - `dominios_email`, `codigos_telefono`
  - `personas_emails`, `personas_telefonos`
  - `clientes`, `pasajeros`, `tipos_pasajero`
- **Autenticación y sesiones**
  - `roles_sistema`, `permisos`, `roles_permisos`
  - `usuarios`, `sesiones`

---

### Documentación del modelo EF Core (qué hay en código y cómo se mapea)

Esta sección resume **solo la capa de infraestructura** (entidades + `IEntityTypeConfiguration`). No sustituye el script SQL del curso: si tu script usa otros nombres de tabla/columna o tipos, hay que **ajustar** `ToTable`, `HasColumnName` y longitudes en las clases `*EntityConfiguration` para que coincidan al 100 %.

#### Organización por carpetas (módulos)

| Carpeta bajo `Src/Modules` | Contenido |
|----------------------------|-----------|
| `Continents`, `Countries`, `Regions`, `Cities` | Geografía (ya existía). |
| `StreetsTypes` | `StreetTypeEntity` → tabla `street_type`. |
| `Directions` | `DirectionEntity` → tabla `directions`; además dominio/Application/repositorio de direcciones. |
| **`Persons`** | Catálogos y tablas de personas, contactos, clientes y pasajeros (nuevo). |
| **`Auth`** | Roles, permisos, relación rol–permiso, usuarios y sesiones (nuevo). |

#### Tabla ↔ entidad ↔ `DbSet` en `AppDbContext`

| Tabla SQL (objetivo del ToDo) | Clase C# | `DbSet` |
|------------------------------|----------|---------|
| `continents` | `ContinentEntity` | `Continents` |
| `countries` | `CountryEntity` | `Countries` |
| `regions` | `RegionEntity` | `Regions` |
| `cities` | `CityEntity` | `Cities` |
| `street_type` | `StreetTypeEntity` | `StreetTypes` |
| `directions` | `DirectionEntity` | `Directions` |
| `tipos_documento` | `DocumentTypeEntity` | `DocumentTypes` |
| `dominios_email` | `EmailDomainEntity` | `EmailDomains` |
| `codigos_telefono` | `PhoneCodeEntity` | `PhoneCodes` |
| `personas` | `PersonEntity` | `Persons` |
| `personas_emails` | `PersonEmailEntity` | `PersonEmails` |
| `personas_telefonos` | `PersonPhoneEntity` | `PersonPhones` |
| `clientes` | `ClientEntity` | `Clients` |
| `tipos_pasajero` | `PassengerTypeEntity` | `PassengerTypes` |
| `pasajeros` | `PassengerEntity` | `Passengers` |
| `roles_sistema` | `SystemRoleEntity` | `SystemRoles` |
| `permisos` | `PermissionEntity` | `Permissions` |
| `roles_permisos` | `RolePermissionEntity` | `RolePermissions` |
| `usuarios` | `UserEntity` | `Users` |
| `sesiones` | `SessionEntity` | `Sessions` |

#### Relaciones y reglas modeladas en Fluent API

- **`PersonEntity`**: FK a `DocumentTypeEntity` (obligatoria) y a `DirectionEntity` (opcional, `direccion_id`). Índice **único** `(tipo_documento_id, numero_documento)` para reflejar la regla de negocio de documento único por tipo.
- **`PersonEmailEntity`**: FK a `PersonEntity` (borrado en **cascada** respecto a la persona) y FK opcional a `EmailDomainEntity`. `email` único global (evita duplicar la misma dirección en el sistema).
- **`PersonPhoneEntity`**: FK a `PersonEntity` (cascada) y a `PhoneCodeEntity`. Índice único compuesto `(persona_id, codigo_telefono_id, numero)`.
- **`ClientEntity`**: FK a `PersonEntity`; **`persona_id` único** (una fila cliente por persona).
- **`PassengerEntity`**: FK a `PersonEntity` y `PassengerTypeEntity`; **`persona_id` único** (una fila pasajero por persona, alineado al ToDo).
- **`RolePermissionEntity`**: clave primaria compuesta `(rol_id, permiso_id)`; FKs a `SystemRoleEntity` y `PermissionEntity` con cascada al eliminar rol o permiso (solo filas puente; revisar si tu política de negocio prefiere `Restrict`).
- **`UserEntity`**: FK opcional a `PersonEntity` (**`persona_id` único** cuando no es null: como mucho un usuario vinculado a cada persona), FK obligatoria a `SystemRoleEntity`, **`nombre_usuario` único**, campos `password_hash` y `activo` (bool → `tinyint(1)` en MySQL).
- **`SessionEntity`**: FK a `UserEntity` (cascada); campos UTC de emisión y expiración, bandera `revocada` y `refresh_token` opcional.

#### Qué falta para estar “cerrado” con la base real

1. **Migración**: generar y aplicar cuando el equipo lo decida (`dotnet ef migrations add …` / `database update`). Hasta entonces el modelo **compila** pero el esquema en MySQL puede no coincidir.
2. **Alineación con el script oficial**: renombrar tablas/columnas en configuraciones si tu SQL usa `tipos_via` en lugar de `street_type`, `direcciones` en lugar de `directions`, etc.
3. **Consola / menús** (Hito 2 en adelante): el dominio, repositorios y servicios de aplicación de personas, contactos, clientes y pasajeros ya están en código; falta cablearlos a un flujo de consola o API.

---

### Estado actual (rápido)

- [x] Entidades creadas: `ContinentEntity`, `CountryEntity`, `RegionEntity`, `CityEntity`
- [x] `StreetTypeEntity` — tabla `street_type` (nombre en BD según `StreetTypeEntityConfiguration`)
- [x] `DirectionEntity` — tabla `directions`; dominio `Directions` (aggregate, value objects, `IDirectionRepository`, `DirectionRepository`)
- [x] Capa Application de direcciones: casos de uso (un archivo por operación CRUD) + `IDirectionService` / `DirectionService` que delega en ellos
- [x] **Módulo `Persons`**: `DocumentTypeEntity`, `EmailDomainEntity`, `PhoneCodeEntity`, `PersonEntity`, `PersonEmailEntity`, `PersonPhoneEntity`, `ClientEntity`, `PassengerTypeEntity`, `PassengerEntity` + configuraciones EF
- [x] **Módulo `Persons` (dominio + aplicación, mismo patrón que `DocumentType` / `Person`)**: agregados + repositorios + CRUD por caso de uso + servicio para `EmailDomain`, `PhoneCode`, `PassengerType`, `Client` (`IClientRecordService`), `Passenger` (`IPassengerRecordService`), `PersonEmail`, `PersonPhone`
- [x] **Módulo `Auth`**: `SystemRoleEntity`, `PermissionEntity`, `RolePermissionEntity`, `UserEntity`, `SessionEntity` + configuraciones EF
- [x] `AppDbContext` registra todos los `DbSet` anteriores
- [ ] Migración EF que cree/altere tablas nuevas: solo cuando lo pidan explícitamente
- [ ] Flujos de consola / casos de uso sobre personas, clientes, usuarios (Hito 2 en adelante)

---

### Hito 1 (P0) — Infraestructura EF Core + migración (que compile y migre)

#### 1.1 Entidades (primitivos)

- [x] `StreetTypeEntity` (`street_type` en BD; alinear nombre con convención del script si difiere de `tipos_via`)
- [x] `DirectionEntity` (`directions`; antes planificado como `direcciones` / `AddressEntity` — unificar nombres con el script oficial)
- [x] `DocumentTypeEntity` (`tipos_documento`)
- [x] `PersonEntity` (`personas`)
- [x] `EmailDomainEntity` (`dominios_email`)
- [x] `PhoneCodeEntity` (`codigos_telefono`)
- [x] `PersonEmailEntity` (`personas_emails`)
- [x] `PersonPhoneEntity` (`personas_telefonos`)
- [x] `ClientEntity` (`clientes`)
- [x] `PassengerTypeEntity` (`tipos_pasajero`)
- [x] `PassengerEntity` (`pasajeros`)
- [x] `SystemRoleEntity` (`roles_sistema`)
- [x] `PermissionEntity` (`permisos`)
- [x] `RolePermissionEntity` (`roles_permisos`) (clave compuesta `rol_id` + `permiso_id`)
- [x] `UserEntity` (`usuarios`)
- [x] `SessionEntity` (`sesiones`)

#### 1.2 Configuraciones (EntityTypeConfiguration)

- [x] Para cada entidad nueva y las ya existentes en el proyecto: `ToTable`, PK o clave compuesta, columnas (`HasColumnName` / tipos), índices únicos donde aplica, FKs y `OnDelete`
- [ ] Revisión final contra **tu script SQL** (nombres y longitudes exactos)

#### 1.3 AppDbContext + migración

- [x] Registrar `DbSet` para geografía, `StreetTypes`, `Directions` (revisar que coincida con lo que uses en la app)
- [x] En `OnModelCreating`: `ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly)`
- [ ] Migración del módulo:
  - [ ] `dotnet ef migrations add Init_Persona1_Core`
  - [ ] `dotnet ef database update`

**Criterio de hecho del hito**: compila, migra en BD limpia, y puedes insertar/consultar al menos `personas` y `usuarios` desde un flujo mínimo.

---

### Hito 2 (P0) — Flujos mínimos en consola (end-to-end)

#### 2.1 Personas / Clientes (P0)

- [ ] Registrar persona
  - [ ] Validar unicidad: `tipo_documento_id` + `numero_documento`
- [ ] Listar personas (documento + nombre completo)
- [ ] Registrar cliente (sobre persona existente)
  - [ ] Evitar duplicado: 1 persona → 1 cliente
- [ ] Listar clientes

#### 2.2 Usuarios / Roles (P0)

- [ ] Registrar rol
- [ ] Registrar permiso
- [ ] Asignar permisos a rol
- [ ] Crear usuario
  - [ ] `username` único
  - [ ] Guardar `password_hash`
- [ ] Listar usuarios con rol
- [ ] Activar / desactivar usuario (si existe el campo; si no existe, definir estrategia)

**Criterio de hecho del hito**: puedes crear persona → cliente, y persona → usuario, y volver a listarlos sin romper integridad.

---

### Hito 3 (P0/P1) — Direcciones + pasajeros

#### 3.1 Geografía (P0)

- [ ] Listar continentes
- [ ] Listar países por continente
- [ ] Listar regiones por país
- [ ] Listar ciudades por región

#### 3.2 Direcciones (P0)

- [x] CRUD de direcciones en código (`Direction` + repositorio + casos de uso + `DirectionService`)
- [x] Modelo EF: `PersonEntity` expone `DirectionId` / columna `direccion_id` (FK opcional a `directions`)
- [ ] Exponer CRUD en consola o API (menú / endpoints) consumiendo `IDirectionService`
- [ ] Flujo de negocio: crear dirección y **asignar** a persona (validar ciudad y tipo de vía; actualizar `personas.direccion_id` desde aplicación)

#### 3.3 Pasajeros (P1)

- [ ] CRUD básico de `tipos_pasajero`
- [ ] Registrar pasajero (persona + tipo_pasajero)
  - [ ] Evitar duplicado: 1 persona → 1 pasajero (si esa es la regla)
- [ ] Listar pasajeros

---

### Hito 4 (P1) — Reportes LINQ (2–3 reportes reales en consola)

- [ ] Personas con más de un email o teléfono (`GroupBy(persona_id)`)
- [ ] Clientes por ciudad o país (joins entre `clientes` → `personas` → `direcciones` → `ciudades` → `paises`)
- [ ] Pasajeros por tipo (`GroupBy(tipo_pasajero_id)`)
- [ ] Usuarios por rol (`GroupBy(rol_id)`)
- [ ] Sesiones activas por usuario (si existe flag/fecha fin)

**Regla**: cada consulta LINQ debe aparecer como opción de menú (listado o resumen).

---

### Reglas y validaciones (P0/P1)

- [ ] **No permitir**
  - [ ] Persona duplicada (`tipo_documento_id` + `numero_documento`)
  - [ ] Cliente duplicado para la misma persona
  - [ ] Pasajero duplicado para la misma persona
  - [ ] Usuario con `username` repetido
  - [ ] FKs inválidas al crear/relacionar (persona/rol/permiso/dirección)
- [ ] **Validar**
  - [ ] Roles y permisos existen antes de relacionarlos
  - [ ] Sesiones referencian usuarios válidos

---

### Decisiones de diseño (anótalas aquí y cúmplelas)

- [ ] **Password hashing**: decidir si el hash vive en dominio (VO `PasswordHash` + servicio) o en infraestructura (recomendado: servicio en infraestructura + interfaz en dominio).
- [ ] **Estrategia de “borrado”**: hard delete vs soft delete (si el esquema tiene `activo`, usarlo).
- [ ] **Unicidad e índices**: reflejar en EF lo que el SQL marca como UNIQUE.
- [ ] **Navegación vs consultas**: preferir consultas explícitas para reportes; usar navegación solo cuando simplifique sin traer datos de más.

---

### Documentación / informe (P1)

- [x] Lista de tablas y entidades (ver sección **Documentación del modelo EF Core** arriba)
- [x] Relaciones clave descritas en esa misma sección (pendiente ampliar en informe académico si lo piden)
- [ ] Resumen de aggregates y/o modelo (si aplicas DDD) y por qué
- [ ] Explicar 2–3 consultas LINQ implementadas (problema de negocio que resuelven)
- [ ] Describir menús y flujos de consola (inputs, validaciones, salidas)

---

### Git (higiene mínima)

- [ ] Commits pequeños y descriptivos (ej.: `feat: add person entities and EF config`)
- [ ] Separar commits por tipo:
  - [ ] EF entities + configs + migración
  - [ ] repos / servicios
  - [ ] menús y flujos
  - [ ] reportes LINQ
  - [ ] fixes