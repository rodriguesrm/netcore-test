namespace Medical.Web.Api.Models
{

    /// <summary>
    /// Generic exception response model
    /// </summary>
    public class GerericExceptionResponse
    {

        /// <summary>
        /// Create a new generic exception model object
        /// </summary>
        /// <param name="code">Exception code</param>
        /// <param name="message">Exception message</param>
        public GerericExceptionResponse(string code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Exception code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Expression message
        /// </summary>
        public string Message { get; set; }

    }
}
