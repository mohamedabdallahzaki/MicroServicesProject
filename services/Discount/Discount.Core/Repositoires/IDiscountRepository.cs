
using Discount.Core.Entities;

namespace Discount.Core.Repositoires
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);

        Task<bool> CreateDsicount(Coupon coupon);

        Task<bool> UpdateDiscount(Coupon coupon);

        Task<bool> DeleteDiscount(string productName);
    }
}
