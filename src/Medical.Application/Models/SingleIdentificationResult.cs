using System;

namespace Medical.Application.Models
{


    /// <summary>
    /// Single identification dto
    /// </summary>
    public class SingleIdentificationResult
    {

        /// <summary>
        /// Identification id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Person fullname
        /// </summary>
        public string FullName { get; set; }

    }
}
