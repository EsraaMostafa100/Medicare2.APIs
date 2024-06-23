using Medicare2.APIs.Helpers;
using Medicare2.Core.Entities;
using Medicare2.Core.Repositories;
using Medicare2.Repository;
using Medicare2.Repository.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Medicare2.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
           
            builder.Services.AddDbContext<StoreContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped(typeof(ICartRepository), typeof(CartRepository));
            builder.Services.AddScoped(typeof(IGenaricRepositories<>), typeof(GenaricRepsitory<>));
            builder.Services.AddAutoMapper(typeof( MappingProfiles));
            builder.Services.AddSingleton<IConnectionMultiplexer>(options=>
            {
                var Connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });
            var app = builder.Build();

            #region update-database
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var Loggerfactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbcontext = services.GetRequiredService<StoreContext>();
                await dbcontext.Database.MigrateAsync();
                await StoreDataSeed.SeedAsync(dbcontext);
            }
            catch (Exception ex)
            {
                var Logger = Loggerfactory.CreateLogger<Program>();
                Logger.LogError(ex, "Error occured during Migration");
            }
            #endregion

            // Configure the HTTP request pipeline.
          //  if (app.Environment.IsDevelopment())
          //  {
                app.UseSwagger();
                app.UseSwaggerUI();
          //  }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}