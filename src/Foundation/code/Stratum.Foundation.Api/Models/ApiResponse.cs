
namespace Stratum.Foundation.Api.Models
{
    using System.Net;

    public class ApiResponse
    {
        public string StatusCode { get; set; }
        public string ResponseMessage { get; set; }
        public bool IsSuccess { get { return (StatusCode == HttpStatusCode.OK.ToString() || StatusCode == HttpStatusCode.Created.ToString()); } }
    }
}
