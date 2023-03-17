using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse ResponseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.ResponseModel = new(); 
            this.httpClient = httpClient;

        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "Application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if(apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "Application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                    default:
                        message.Method=HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiResponse = null;

                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                try
                {
                    APIResponse APiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if(apiResponse.StatusCode==System.Net.HttpStatusCode.BadRequest || apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        APiResponse.StatusCodes = System.Net.HttpStatusCode.BadRequest;
                        APiResponse.IsSuccess = false;
                        var Res = JsonConvert.SerializeObject(APiResponse);
                        var APIResObj = JsonConvert.DeserializeObject<T>(Res);
                        return APIResObj;
                    }

                }
                catch (Exception e) 
                {
                    var ExcemptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return ExcemptionResponse;

                }
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;


            }
            catch(Exception ex)
            {
                var Dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var Res = JsonConvert.SerializeObject(Dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(Res);
                return APIResponse;
            };
        }
    }
}
