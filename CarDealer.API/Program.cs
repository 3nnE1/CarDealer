using CarDealer.Data;
using CarDealer.Services;
using CarDealer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        ConfigurationManager configuration = builder.Configuration;
        string connection = configuration.GetConnectionString("DefaultConnection")!;


        #region Adding DB Context
        builder.Services.AddDbContext<CarDealerContext>
            (
                options => options
                .UseSqlServer(connection)
                .EnableDetailedErrors()
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
            );
        #endregion


        #region Inversion of Control - Dependency Injection
        builder.Services.AddScoped<IAvailableCarService, AvailableCarService>();
        builder.Services.AddScoped<ISoldCarService, SoldCarService>();
        builder.Services.AddScoped<ISalesManagerService, SalesManagerService>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        #endregion


        #region Adding Controllers
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        #endregion


        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}