using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extensions
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Discount DB Migration Started");
                    ApplyMigrations(config);
                    logger.LogInformation("Discount DB Migration Completed");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return host;
        }

        private static void ApplyMigrations(IConfiguration config)
        {
            var retry = 5;
            while (retry > 0)
            {
                try
                {
                    using var connection =
                new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();
                    using var cmd = new NpgsqlCommand()
                    {
                        Connection = connection
                    };
                    cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"CREATE TABLE Coupon (Id SERIAL PRIMARY KEY, ProductName VARCHAR(24) NOT NULL, Description TEXT, Amount INT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Nike Air Max 270', 'Nike Air Max Discount', 10)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Adidas Terrex Swift', 'Adidas Discount', 15)";
                    cmd.ExecuteNonQuery();
                    break; // Exit loop if successful
                }
                catch (NpgsqlException e)
                {
                    Console.WriteLine(e);
                    retry--;
                    if (retry == 0)
                    {
                        throw;
                    }
                    Thread.Sleep(2000); // Wait before retrying
                }
            }

        }
    }
}