using System.Net;

namespace MagicVila_WebAPI.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCodes { get; set; }
        public bool IsSuccess { get; set; } = true;

        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
