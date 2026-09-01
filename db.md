## Movies
- Id
- Title 
- ReleaseYear
- DirectorId (FK -> Person)
- Genres
- Actors

## Genres
- Id
- Name

## MovieGenre
- MovieId
- GenreId

## Person
- Id
- Name
- Surname
- BirthYear

## MovieActor
- MovieId
- PersonId