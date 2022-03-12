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

- [ASP.NET Core Tutorial](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio#examine-the-get-methods)
- [Entity Framework](https://docs.microsoft.com/en-us/ef/core/)
  - [Migrations](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/migrations?view=aspnetcore-6.0&tabs=visual-studio-code)
  - [DB Provider](https://www.npgsql.org/efcore/index.html)
- [Unit Testing](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)
  - [Moq](https://github.com/moq/moq4)
  - [FluentAssertions](https://fluentassertions.com/)


## Setup Database

The easiest way to install the DB is by running an instance in a local docker container, however if you already have a DB instance this is not necessary.

To setup the container simply run:

```bash
docker run -d --name FlooqDatabase -v my_dbdata:/var/lib/postgresql/data -p 54320:5432 -e POSTGRES_PASSWORD=test123 postgres:13
```

And then to create a new database use:

```
docker exec -it FlooqDatabase psql -U postgres -c "create database FlooqDatabase"
```

To apply the current database scheme you have to use the Entity Framework tools these can be installed via:

```bash
dotnet tool install --global dotnet-ef
```

After that update the database like so:

```bash
dotnet ef database update
```
