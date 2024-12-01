using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersService.Data.Configuration;

namespace UsersService.Data;

public static class DbContextExtensions
{
    public static IServiceCollection AddPostgresDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var postgresSettings = configuration.GetSection(nameof(PostgresConfiguration));
        
        serviceCollection.AddDbContext<UserDbContext>(opt =>
        {
            opt.UseNpgsql(postgresSettings["ConnectionString"]);
        });
        
        return serviceCollection;
    }
    
    public static async Task ApplyMigrationAsync(this IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        var dbContextReserve = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        try
        {
            await dbContextReserve.Database.MigrateAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Exception {nameof(UserDbContext)}: {exception.Message}");
            Console.WriteLine($"Ошибка: при создании базы данных {exception.Message}");
            Console.WriteLine($"{exception?.InnerException?.Message}");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Выход из приложения exit(1), сервер ASPNET не запущен!");
            Environment.Exit(333);
        }
    }
}