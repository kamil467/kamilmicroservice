

using static Pnk.Web.ServiceConfiguration;

namespace Pnk.Web.Models.Dto
{
    public class APIRequest
    {
        /// <summary>
        /// Payload data
        /// </summary>
        public  object Payload { get; set; }

        /// <summary>
        /// Request ID.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// HTTP Request Type - GET, POST, PUT, DELETE
        /// Default = GET
        /// </summary>
        public CallType CallType { get; set; } = CallType.GET;

        public string RequestURL { get; set; }

        public string AccessToken { get; set; }

    }
}
