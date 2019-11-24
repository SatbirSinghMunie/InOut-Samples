using APIResource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIResource.Infrastructure.Services
{
    public interface IHttpRequestHelper
    {
        Task<BaseResponse<T>> GetRequestWithToken<T>(string url);
        Task<BaseResponse<T>> PostRequestWithToken<T, D>(string url, D data);
        Task<BaseResponse<bool>> DeleteRequestWithToken(string url);
        Task<BaseResponse<T>> PutRequestWithToken<T, D>(string url, D data);
    }
}
