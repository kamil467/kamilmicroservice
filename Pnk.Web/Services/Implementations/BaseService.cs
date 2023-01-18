using Newtonsoft.Json;
using Pnk.Web.Models.Dto;
using Pnk.Web.Services.IServices;
using System.Text;
using System.Text.Json.Serialization;
using static Pnk.Web.ServiceConfiguration;

namespace Pnk.Web.Services.Implementations
{
    public class BaseService : IBaseService
    {
        private IHttpClientFactory httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
          
        }

        public ResponseDto ResponseModel { get ; set ; }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public async Task<T> SendRequestAysnc<T>(APIRequest aPIRequest)
        {
            try
            {
                using(var client = this.httpClientFactory.CreateClient("MangoAPI"))
                {

                    var message = new HttpRequestMessage();
                    message.Headers.Add("accept", "application/json");
                    message.RequestUri = new Uri(aPIRequest.RequestURL);
                    client.DefaultRequestHeaders.Clear();

                    if(aPIRequest.Payload != null)
                    {
                        // serialize and attach to the request.
                        message.Content = new StringContent(JsonConvert.SerializeObject(aPIRequest.Payload), 
                                         Encoding.UTF8,
                                         "application/json");

                    }
                    HttpResponseMessage responseMessage = null;
                    switch (aPIRequest.CallType)
                    {
                        case CallType.GET:
                            message.Method = HttpMethod.Get;
                            break;
                        case CallType.POST:
                            message.Method = HttpMethod.Post;
                            break;
                        case CallType.PUT:
                            message.Method = HttpMethod.Put;
                            break;
                        case CallType.DELETE:
                            message.Method = HttpMethod.Delete;
                            break;
                        default:
                            message.Method = HttpMethod.Get;
                            break;

                    }
                    responseMessage = await client.SendAsync(message);

                    // below two steps not require if method Type is ResponseDTO
                    var apiContent = await responseMessage.Content
                                    .ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return apiResponse;

                }
            }
            catch(Exception execption)
            {

                var errorResponse = new ResponseDto
                {
                    Message = "Error Occurred",
                    IsSuccess = false,
                    ErrorMessages = new List<string>
                    {
                        execption.ToString()
                    },

                };
                var apiResponse = JsonConvert.SerializeObject(errorResponse);

                return JsonConvert.DeserializeObject<T>(apiResponse);

            }
        }
    }
}
