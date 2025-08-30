

using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositoires;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.ComponentModel;

namespace Discount.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Coupon> GetDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await
                connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @productName", new {ProductName = productName});
            if(coupon == null)
            {
                return new Coupon { Amount = 0,Description = "This Product Not Has Discount" , ProductName = productName};
            }

            return coupon;
        }
        public async Task<bool> CreateDsicount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var res = await
                connection
                .ExecuteAsync("INSERT INTO Coupon (ProductName , Description , Amount) VALUES (@productName , @Description , @Amount)", new
                {
                    ProductName  = coupon.ProductName,
                    Amount = coupon.Amount,
                    Description = coupon.Description
                });
            if(res == 0) return false;
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {

            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var res = await
                connection
                .ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName", new
                {
                    ProductName = productName,
                
                });
            if (res == 0) return false;
            return true;
        }


        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var res = await
                connection
                .ExecuteAsync("UPDATE  Coupon SET ProductName = @productName , Description = @Description , Amount = @Amount WHERE Id = @Id", new
                {
                    ProductName = coupon.ProductName,
                    Amount = coupon.Amount,
                    Description = coupon.Description,
                    Id = coupon.Id,
                });
            if (res == 0) return false;
            return true;
        }
    }
}
