using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Infrastructure.Extension
{
    public static class Extension
    {
        public static async Task<WebApplication> MigrateDatabase (this WebApplication host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var confg = services.GetRequiredService<IConfiguration>();
                //var Logger = services.GetRequiredService<ILogger>();

                try
                {
                    //Logger.LogInformation("Discount Db migration stary");
                    await ApplyMigrations(confg);
                    //Logger.LogInformation("discount Db migration completed");

                }
                catch(Exception ex) 

                {
                    //Logger.LogInformation(ex,"cann't create database migration ");
                    throw;
                }
                return host;
            }
        }

        private static async  Task ApplyMigrations(IConfiguration confg)
        {
            var retry = 5;
            while(retry > 0)
            {
                try
                {
                    await using var connection = new NpgsqlConnection(confg.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();
                    using var cmd = new NpgsqlCommand()
                    {
                        Connection = connection
                    };
                    cmd.CommandText = "DROP TABLE IF EXISTS  Coupon";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"CREATE TABLE Coupon (ID SERIAL PRIMARY KEY , ProductName VARCHAR(500) NOT NULL , Description TEXT , Amount INT )";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText =

                    "INSERT INTO Coupon ( ProductName , Description , Amount ) VALUES ('iPhone 15 Pro Max' , 'Apple' , 500)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText =

                    "INSERT INTO Coupon  ( ProductName , Description , Amount ) VALUES ('Samsung Galaxy S24 Ultra' , 'Samsung' , 1000)";
                    cmd.ExecuteNonQuery();

                    break;
                }
                catch
                {
                    retry--;
                    if (retry == 0)
                        throw;
                    Thread.Sleep(3000);

                }
            }
        }
    }
}
