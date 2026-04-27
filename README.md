# Examen 1 - Sistema de Gestión de Tiquetes Aéreos

Este README resume las funcionalidades Requeridas en el examen y las especificaciones técnicas implementadas en el sistema de gestión de tiquetes.

## 🚀 Funcionalidades Principales

### 1. Gestión de Capacidad y Asientos
- **Capacidad Ampliada**: Cada vuelo ahora cuenta con una capacidad total de **90 asientos**, organizados en **15 filas** de 6 columnas (A-F).
- **Configuración de Clases**:
  - **Filas 1-2**: Primera Clase.
  - **Filas 3-5**: Clase Ejecutiva.
  - **Filas 6-15**: Clase Económica.
- **Selección Interactiva**: El proceso de reserva permite al usuario elegir primero la clase deseada y luego visualizar únicamente los asientos disponibles en dicha clase.

### 2. Sistema de Tarifas Visuales
- Se han implementado tarifas base para mejorar la experiencia de usuario:
  - **Económica**: $40.00
  - **Ejecutiva**: $70.00
  - **Primera Clase**: $100.00

### 3. Usuarios de Prueba (Seed Data)
El sistema garantiza la existencia de usuarios base para pruebas inmediatas:
- **Administrador (ROOT)**: (Admin)
  - Usuario: `ROOT`
  - Contraseña: `12345`
- **Cliente (Jolhver)**: (Cliente)
  - Usuario: `Jolhver`
  - Contraseña: `123`

---

## 🛠 Detalles Técnicos

### Arquitectura
- **Lenguaje**: C# (.NET 10)
- **Base de Datos**: MySQL
- **ORM**: Entity Framework Core
- **Interfaz**: Aplicación de consola interactiva con `Spectre.Console`.

### Base de Datos
- **Idempotencia**: El sistema de *seeding* asegura que los datos base (aerolíneas, aeropuertos, rutas, usuarios) se creen solo si no existen.
- **Limpieza Automática**: Se incluye lógica para limpiar columnas obsoletas y normalizar esquemas de bases de datos heredadas.

---

## 📖 Instrucciones de Uso
1. Ejecutar el proyecto: `dotnet run`.
2. Iniciar sesión con `Jolhver / 123` o crear cliente desde `ROOT -> Usuarios`.
3. Navegar a **Reservaciones** -> **Crear reservación**.
4. Seguir los pasos de selección de vuelo, clase y asiento.
