﻿using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponAPIController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private ResponseDto _responseDto;
    private IMapper _mapper;

    public CouponAPIController(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _responseDto = new ResponseDto();
        _mapper = mapper;
        
    }

    [HttpGet]
    public ResponseDto GetAll()
    {
        try
        {
            IEnumerable<Coupon> objList = _db.Coupons.ToList();
            _responseDto.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
        }
        catch(Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = ex.Message;
        }
        return _responseDto;
    }

    [HttpGet]
    [Route("{id:int}")]
    public ResponseDto Get(int id)
    {
        try
        {
            Coupon objCoupon = _db.Coupons.First(u => u.CouponID == id);
            _responseDto.Result = _mapper.Map<CouponDto>(objCoupon);
        }
        catch(Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = ex.Message;
        }
        return _responseDto;
    }

    [HttpGet]
    [Route("GetByCode/{code}")]
    public ResponseDto GetByCode(string code)
    {
        try
        {
            Coupon objCoupon = _db.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
            if (objCoupon == null)
            {
                _responseDto.IsSuccess =false;
            }
            _responseDto.Result = _mapper.Map<CouponDto>(objCoupon);
        }
        catch (Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = ex.Message;
        }
        return _responseDto;
    }

    [HttpPost]
    public ResponseDto Post([FromBody] CouponDto couponDto)
    {
        try
        {
           Coupon obj = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Add(obj);   
            _db.SaveChanges();

            _responseDto.Result = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = ex.Message;
        }
        return _responseDto;
    }

    [HttpPut]
    public ResponseDto Put([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon obj = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Update(obj);
            _db.SaveChanges();

            _responseDto.Result = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = ex.Message;
        }
        return _responseDto;
    }

    [HttpDelete]
    public ResponseDto Delete(int id)
    {
        try
        {
            Coupon obj = _db.Coupons.First(u => u.CouponID == id);
            _db.Coupons.Remove(obj);
            _db.SaveChanges();

            _responseDto.Result = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception ex)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = ex.Message;
        }
        return _responseDto;
    }
}
