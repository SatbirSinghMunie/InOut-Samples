using APIResource.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIResource.Infrastructure.Services
{
    public class HttpRequestHelper : IHttpRequestHelper
    {
        private IConfiguration _configuration;
        private IHttpContextAccessor _context;

        public HttpRequestHelper(IConfiguration configuration, IHttpContextAccessor context)
        {
            _configuration = configuration;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        private long SampleApplicationId { get { return Convert.ToInt64(_configuration["SampleApplicationId"]); } }

        public async Task<BaseResponse<T>> GetRequestWithToken<T>(string url)
        {
            var apiClient = new HttpClient();

            // Add a new Request Message
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            await AddHeadersToRequest(requestMessage);

            // Send the request to the server
            HttpResponseMessage response = await apiClient.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentNullException(nameof(response));
            }
            else
            {
                string responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BaseResponse<T>>(responseData);
            }
        }

        public async Task<BaseResponse<T>> PostRequestWithToken<T, D>(string url, D data)
        {
            using (var apiClient = new HttpClient())
            {
                var jsonData = JsonConvert.SerializeObject(data);
                var requestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Post,
                    Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
                };

                await AddHeadersToRequest(requestMessage);

                var response = apiClient.SendAsync(requestMessage).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new ArgumentNullException(nameof(response));
                }
                else
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<BaseResponse<T>>(responseData);
                }
            }
        }

        public async Task<BaseResponse<bool>> DeleteRequestWithToken(string url)
        {
            var apiClient = new HttpClient();

            // Add a new Request Message
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);

            await AddHeadersToRequest(requestMessage);

            // Send the request to the server
            HttpResponseMessage response = await apiClient.SendAsync(requestMessage);
            //check the response if not SUCCESS or not NO CONTENT status code then throw exception
            if (!response.IsSuccessStatusCode || !(response.StatusCode == System.Net.HttpStatusCode.NoContent))
            {
                throw new ArgumentNullException(nameof(response));
            }
            else
            {
                string responseData = await response.Content.ReadAsStringAsync();
                return new BaseResponse<bool>() { Data = true };
            }
        }


        public async Task<BaseResponse<T>> PutRequestWithToken<T, D>(string url, D data)
        {
            // call api
            using (var apiClient = new HttpClient())
            {
                var jsonData = JsonConvert.SerializeObject(data);
                var requestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Put,
                    Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
                };

                await AddHeadersToRequest(requestMessage);

                var response = apiClient.SendAsync(requestMessage).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new ArgumentNullException(nameof(response));
                }
                else
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<BaseResponse<T>>(responseData);
                }
            }
        }

        private async Task AddHeadersToRequest(HttpRequestMessage requestMessage)
        {
            string accessToken = await GetAccessToken();

            requestMessage.Headers.Add("Authorization", "Bearer " + accessToken);
            requestMessage.Headers.Add(InOutConstants.OrganizationIdHeader, _context.HttpContext.Request.Headers[InOutConstants.OrganizationIdHeader].ToString());
            requestMessage.Headers.Add(InOutConstants.ApplicationIdHeader, SampleApplicationId.ToString());
            requestMessage.Headers.Add(InOutConstants.UserEmailHeader, _context.HttpContext.Request.Headers[InOutConstants.UserEmailHeader].ToString().ToLower());
        }

        private async Task<string> GetAccessToken()
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _configuration["IdentityAuthority"],
                Policy =
                {
                    RequireHttps = Convert.ToBoolean(_configuration["IdentityRequiresHttps"])
                }
            });

            if (disco.IsError)
            {
                return null;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = _configuration["IdentityClientId"],
                ClientSecret = _configuration["IdentityClientSecret"],

                Scope = _configuration["Auth_IdentityScope"]
            });

            if (tokenResponse.IsError)
            {
                return null;
            }

            return tokenResponse.AccessToken;
        }
    }
}
