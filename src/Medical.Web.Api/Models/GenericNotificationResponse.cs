namespace Medical.Web.Api.Models
{

    /// <summary>
    /// Generic critical/error notification model
    /// </summary>
    public class GenericNotificationResponse
    {

        /// <summary>
        /// Property name
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Critical/error message
        /// </summary>
        public string Message { get; set; }

    }

}