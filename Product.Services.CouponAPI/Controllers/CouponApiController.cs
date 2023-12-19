using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Services.CouponAPI.Data;
using Product.Services.CouponAPI.Models;
using Product.Services.CouponAPI.Models.Dto;

namespace Product.Services.CouponAPI.Controllers;
[Route("/api/[controller]")]
[ApiController]
public class CouponApiController : ControllerBase
{
    private readonly AppDbContext _db;
    private IMapper _mapper;
    private ResponseDto _response;
    public CouponApiController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        _response = new ResponseDto();
    }

    [HttpGet]
    public async Task<ResponseDto> Get()
    {
        try
        {
            IEnumerable<Coupon> list = await _db.Coupons.ToListAsync();
            _response.Result = _mapper.Map<IEnumerable<CouponDto>>(list);

        }
        catch (Exception e)
        {
            _response.Message = e.Message;
            _response.IsSuccess = false;
            
        }
        return _response;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ResponseDto> GetById(int id)
    {
        try
        {
            Coupon coupon = await _db.Coupons.FirstAsync(x => x.CouponId == id);
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.Message = e.Message;
            _response.IsSuccess = false;
        }
        return _response;

    }

    [HttpPost]
    public async Task<ResponseDto> Add([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            await _db.AddAsync(coupon);
            await _db.SaveChangesAsync();
            _response.Result = coupon;
        }
        catch (Exception e)
        {
            _response.Message = e.Message;
            _response.IsSuccess = false;
        }
        return _response;
    }

    [HttpPut]
    public async Task<ResponseDto> Update([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Update(coupon);
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _response.Message = e.Message;
            _response.IsSuccess = false;
        }
        return _response;
    }
    [HttpDelete]
    public async Task<ResponseDto> Delete(int id)
    {
        try
        {
            Coupon coupon = await _db.Coupons.FirstAsync(x => x.CouponId == id);
            _db.Remove(coupon);
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            _response.Message = e.Message;
            _response.IsSuccess = false;
        }
        return _response;
    }
}