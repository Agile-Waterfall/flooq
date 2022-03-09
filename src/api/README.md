# API

## Getting Started

1. Install dependencies
   - [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

2. If you're using Visual Studio Code press F5 to run the API. Otherwise you can use

```bash
dotnet run
```

3. Run the tests with

```bash
dotnet test
```

Open [https://localhost:7035/](https://localhost:7035/) with your browser to see the result. The current API definitions are accessible via the [Swagger UI](https://localhost:7035/swagger/index.html).


## Important Links

- [Entity Framework](https://docs.microsoft.com/en-us/ef/core/)
  - [Migrations](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/migrations?view=aspnetcore-6.0&tabs=visual-studio-code)
  - [DB Provider](https://www.npgsql.org/efcore/index.html)
- [Unit Testing](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)
  - [Moq](https://github.com/moq/moq4)
  - [FluentAssertions](https://fluentassertions.com/)