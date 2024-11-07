using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Catalog_WebApi.Infrastructure;
using Catalog_DAL.Repositories;
using Catalog_DAL.Entities;
using Catalog_DAL.Repositories.Contracts;
using Catalog_DAL.UOF;
using Catalog_BLL.Services.Contracts;
using Catalog_BLL.Services;
using Catalog_BLL.DTO.Requests;
using Catalog_BLL.Validation;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        //builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();

        builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
        builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);

        // SWAGER
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<CatalogContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

        // REPOSITORIES
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // SERVICES
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IProductService, ProductService>();

        // VALIDATORS
        builder.Services.AddScoped<IValidator<CategoryRequest>, CategoryRequestValidator>();
        builder.Services.AddScoped<IValidator<ProductRequest>, ProductRequestValidator>();

        // AUTOMAPPER
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseExceptionHandler();
        app.MapControllers();


        app.Run();
    }
}


