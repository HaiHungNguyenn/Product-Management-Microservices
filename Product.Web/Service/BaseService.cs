﻿using System.Net;
using System.Text;
using Newtonsoft.Json;
using Product.Web.Models;
using Product.Web.Service.IService;
using Product.Web.Utility;

namespace Product.Web.Service;
public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private IBaseService _baseService;

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


    public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
    {
        try
        { 
             HttpClient client = _httpClientFactory.CreateClient("ProductAPI");
             HttpRequestMessage message = new();
             message.Headers.Add("Accept","application/json");
             //token here
             message.RequestUri = new Uri(requestDto.Url);
             if (requestDto.Data != null)
             {
                 message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8,"application/json");
             }
     
            
             switch (requestDto.ApiType)
             {
                 case SD.ApiType.POST:
                     message.Method = HttpMethod.Post;
                     break;
                 case SD.ApiType.PUT :
                     message.Method = HttpMethod.Put;
                     break;
                 case SD.ApiType.DELETE:
                     message.Method = HttpMethod.Delete;
                     break;
                 default:
                     message.Method = HttpMethod.Get;
                     break;
             }
             HttpResponseMessage? apiResponse = null;
             apiResponse = await client.SendAsync(message);
             switch (apiResponse.StatusCode)
             {
                 case HttpStatusCode.NotFound:
                     return new() { IsSuccess = false, Message = "Not Found" };
                 case HttpStatusCode.Forbidden:
                     return new() { IsSuccess = false, Message = "Forbidden" };
                 case HttpStatusCode.InternalServerError:
                     return new() { IsSuccess = false, Message = "Internal Server Error" };
                 case HttpStatusCode.Unauthorized:
                     return new() { IsSuccess = false, Message = "Unauthorized" };
                 default:
                     var apiContent = await apiResponse.Content.ReadAsStringAsync();
                     var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                     return apiResponseDto;
             }
        }
        catch (Exception e)
        {
            var dto = new ResponseDto
            {
                Message = e.Message,
                IsSuccess = false
            };
            return dto;
        }
    }

}