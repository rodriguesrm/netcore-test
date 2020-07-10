using System;

namespace Medical.Web.Api.Models
{

    /// <summary>
    /// Simple data person response
    /// </summary>
    public class SimplePersonResponse
    {

        /// <summary>
        /// Person identification id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Person full name
        /// </summary>
        public string FullName { get; set; }

    }
}
