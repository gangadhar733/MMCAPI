using MMC.Api.Data;
using MMC.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MMC");
builder.Services.AddSqlite<UserContext>(connectionString);

var app = builder.Build();

app.MapGet("/", () => "Welcome to MMC API!");

app.MapUsersEndpoints();
app.MapPaymentsEndpoints();

await app.MigrateDbAsync();
app.Run();
