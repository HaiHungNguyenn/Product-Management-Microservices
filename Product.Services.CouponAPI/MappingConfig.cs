using AutoMapper;

using Product.Services.CouponAPI.Models;
using Product.Services.CouponAPI.Models.Dto;

namespace Product.Services.CouponAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<CouponDto, Coupon>();
            config.CreateMap<Coupon, CouponDto>();
        });
        return mappingConfig;
    }
}