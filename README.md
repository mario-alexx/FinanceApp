# FinanceApp

## Descripción

FinanceApp es una aplicación para la gestión de finanzas personales, permitiendo registrar ingresos, gastos, presupuestos y generar reportes básicos. La aplicación sigue los principios de **Clean Architecture** y está desarrollada con **ASP.NET Core 8** y **Angular**.

## Tecnologías Utilizadas

- **Backend:** ASP.NET Core 8
- **Base de Datos:** SQL Server 2022, Redis
- **Frontend:** Angular
- **Contenedores:** Docker
- **Control de Versiones:** Git y GitHub
- **Pruebas:** xUnit

## Configuración Inicial

### 1. Clonar el repositorio

```sh
git clone https://github.com/tuusuario/FinanceApp.git
cd FinanceApp
```

### 2. Configurar archivos de entorno

Copiar el archivo `.env.example` como `.env` y configurar las variables necesarias.

### 3. Ejecutar la aplicación con Docker

```sh
docker-compose up -d
```

Esto levantará la API, la base de datos SQL Server y Redis.

### 4. Aplicar migraciones

Ejecutar dentro del contenedor o en el entorno local:

```sh
dotnet ef database update --project src/FinanceApp.Infrastructure --startup-project src/FinanceApp.API
```

### 5. Ejecutar la API

```sh
dotnet run --project src/FinanceApp.API
```

La API estará disponible en `http://localhost:5000`.

## Endpoints Clave

### Autenticación (Identity + JWT)

- `POST /api/auth/register` - Registro de usuario
- `POST /api/auth/login` - Inicio de sesión
- `POST /api/auth/refresh-token` - Renovación de token JWT

### Finanzas

- `GET /api/transactions` - Obtener transacciones
- `POST /api/transactions` - Crear una transacción
- `DELETE /api/transactions/{id}` - Eliminar transacción (Soft Delete)

## Pruebas

Ejecutar las pruebas con:

```sh
dotnet test
```

## Contribución

1. Crear una nueva rama para la funcionalidad:
   ```sh
   git checkout -b feature/nueva-funcionalidad
   ```
2. Realizar cambios y realizar un commit:
   ```sh
   git commit -m "Agregada nueva funcionalidad"
   ```
3. Subir cambios y crear un Pull Request:
   ```sh
   git push origin feature/nueva-funcionalidad
   ```

