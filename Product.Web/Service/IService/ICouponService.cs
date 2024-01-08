using Product.Web.Models;

namespace Product.Web.Service.IService;

public interface ICouponService
{
    Task<ResponseDto?> GetCouponAsync(string code);
    Task<ResponseDto?> GetAllCouponAsync();
    Task<ResponseDto?> GetCouponByIdAsync(int id);
    Task<ResponseDto?> DeleteCouponAsync(int id);
    Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);
    Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);

}