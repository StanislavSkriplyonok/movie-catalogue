# Movie Catalog API

A REST API for managing a movie catalog, built with ASP.NET Core and Entity Framework Core. Supports full CRUD operations for movies, genres, and people (directors/actors), with proper relational data modeling, DTOs, validation, and centralized error handling.

## Tech Stack

- **ASP.NET Core 9** — Web API with controllers
- **Entity Framework Core 9** (Code First) — ORM and migrations
- **PostgreSQL** — database
- **Swagger** — interactive API documentation

## Features

- Full CRUD for **Movies**, **Genres**, and **People**
- Relational data model:
  - One-to-many: a movie has one director
  - Many-to-many: movies ↔ genres, movies ↔ actors
- **DTOs** for all endpoints — EF entities are never exposed directly to clients
- **Data validation** via DataAnnotations (required fields, string length, year ranges)
- **Global exception-handling middleware** — consistent JSON error format across the API
- Explicit delete protection: a director cannot be deleted while they have movies assigned (returns `409 Conflict`)
- **Filtering, sorting, and pagination** on the movies endpoint

## Data Model

```
Movie
 ├── DirectorId (FK → Person)
 ├── Genres (many-to-many → Genre)
 └── Actors (many-to-many → Person)

Genre
 └── Movies (many-to-many)

Person
 ├── DirectedMovies (one-to-many)
 └── ActedMovies (many-to-many)
```

## API Endpoints

### Movies
| Method | Route | Description |
|---|---|---|
| GET | `/api/movies` | List movies (supports filtering, sorting, pagination) |
| GET | `/api/movies/{id}` | Get a single movie |
| POST | `/api/movies` | Create a movie |
| PUT | `/api/movies/{id}` | Update a movie |
| DELETE | `/api/movies/{id}` | Delete a movie |

**Query parameters for `GET /api/movies`:**
- `genre` — filter by genre name
- `year` — filter by release year
- `sortBy` — `title` (default) or `year`
- `descending` — `true`/`false`
- `page`, `pageSize` — pagination (default: page 1, size 10, max size 100)

Example:
```
GET /api/movies?genre=Drama&sortBy=year&descending=false&page=1&pageSize=10
```

### Genres
| Method | Route | Description |
|---|---|---|
| GET | `/api/genres` | List genres |
| GET | `/api/genres/{id}` | Get a single genre |
| POST | `/api/genres` | Create a genre |
| PUT | `/api/genres/{id}` | Update a genre |
| DELETE | `/api/genres/{id}` | Delete a genre |

### People (directors/actors)
| Method | Route | Description |
|---|---|---|
| GET | `/api/people` | List people |
| GET | `/api/people/{id}` | Get a single person |
| POST | `/api/people` | Create a person |
| PUT | `/api/people/{id}` | Update a person |
| DELETE | `/api/people/{id}` | Delete a person (blocked with `409` if they are a director of existing movies) |

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)

### Setup

1. Clone the repository
   ```bash
   git clone https://github.com/StanislavSkriplyonok/movie-catalogue.git
   cd movie-catalogue
   ```

2. Set the connection string. Either edit `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=MovieCatalogDb;Username=postgres;Password=your_password"
   }
   ```
   or use .NET User Secrets (recommended, keeps credentials out of source control):
   ```bash
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=MovieCatalogDb;Username=postgres;Password=your_password"
   ```

3. Apply migrations to create the database and tables:
   ```bash
   dotnet ef database update
   ```

4. Run the project:
   ```bash
   dotnet run
   ```

5. Open Swagger UI in your browser:
   ```
   http://localhost:<port>/swagger
   ```
   (the port is printed in the console after `dotnet run`)

## Example Request

Creating a movie:

```http
POST /api/movies
Content-Type: application/json

{
  "title": "Inception",
  "releaseYear": 2010,
  "directorId": 1,
  "genreIds": [1, 2],
  "actorIds": [1, 3]
}
```

Response:

```json
{
  "id": 5,
  "title": "Inception",
  "releaseYear": 2010,
  "directorName": "Christopher Nolan",
  "genres": ["Sci-Fi", "Thriller"],
  "actors": ["Leonardo DiCaprio", "Tom Hardy"]
}
```

## Project Structure

```
MovieCatalog.Api/
├── Controllers/     # API controllers (Movies, Genres, People)
├── Models/          # EF Core entities
├── DTOs/            # Request/response data transfer objects
├── Mapping/         # Entity ↔ DTO mapping extension methods
├── Data/            # AppDbContext
├── Middleware/       # Global exception handling
└── Migrations/       # EF Core migrations
```