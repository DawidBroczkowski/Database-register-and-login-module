Projekt to WebAPI napisane w ASP.NET Core 6. Starałem się go wykonać zgodnie z konceptem Clean Architecture.

Sposób wykonania wymagać:

  - Do stworzenia warstwy bazy danych został użyty Entity Framework Core.
  - Metody walidujące pola rejestracji i logowania zawarte są w klasie ValidationService, do sprawdzania poprawności odbieranych DTO użyte zostały jeszcze DataAnnotations. Późniejsza walidacja przeprowadzana jest w klasie AccountService.
  - Błędy są obsługiwane przez Global Exception Handler, który jest skonfigurowany w klasie ExceptionHandlerExtensions.
  - Hasła są zabezpieczane przez hashowanie HMACSHA512 i dodanie soli.
  - Uwierzytelnianie realizowane jest przy użyciu AspNetCore.Authentication i AspNetCore.Authorization. Po zalogowaniu zwracany jest JSON Web Token, który zawiera unikalny E-mail użytkownika jako claim.
  - Metoda zwracająca dane wszystkich użytkowników wymaga uwierzytelnienia, metoda zwracająca dane pojedynczego użytkownika jest w wersji wymagającej uwierzytelnienia dla zalogowanego użytkownika oraz w wersji, która wymaga podania maila i hasła użytkownika, którego dane chce się dostać.
