using System.Data;
using BigFood_Reviews.Repositories.Interfaces;
using BigFood_Reviews.Repositories;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();


builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);

// SWAGER
builder.Services.AddSwaggerGen();

//DATABASE
builder.Services.AddScoped((s) => new NpgsqlConnection(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddScoped<IDbTransaction>(s =>
{
    NpgsqlConnection conn = s.GetRequiredService<NpgsqlConnection>();
    conn.Open();
    return conn.BeginTransaction();
});


//DEPENDENCY INJECTION
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();