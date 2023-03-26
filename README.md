## WebAPI

Projekt został napisany w ASP.NET Core 6. Starałem się go wykonać zgodnie z konceptem Clean Architecture.

Szczegóły implementacji:

  - Do stworzenia warstwy bazy danych został użyty Entity Framework Core.
  - Metody walidujące dane podane przy rejestracji i logowaniu są zawarte w klasie ValidationService. Do sprawdzania poprawności odbieranych DTO (np. required, max length) zostały użyte DataAnnotations. Metody te są wykorzystywane w celach walidacyjnych w klasie AccountService.
  - Błędy są obsługiwane przez Global Exception Handler, który jest skonfigurowany w klasie ExceptionHandlerExtensions.
  - Hasła są zabezpieczane przez hashowanie HMACSHA512 i dodawanie soli.
  - Uwierzytelnianie jest realizowane przy użyciu AspNetCore.Authentication i AspNetCore.Authorization. Po zalogowaniu się zwracany jest JSON Web Token, który zawiera unikalny E-mail użytkownika jako claim.
  - Metoda zwracająca dane wszystkich użytkowników wymaga uwierzytelnienia, a metoda zwracająca dane pojedynczego użytkownika jest dostępna w dwóch wersjach. Jedna wymaga uwierzytelnienia, ponieważ zwraca dane zalogowanego użytkownika. Druga zwraca dane wybranego użytkownika na podstawie podanego E-maila i hasła.
