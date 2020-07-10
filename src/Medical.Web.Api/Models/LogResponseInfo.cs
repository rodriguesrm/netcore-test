using System;
using System.Collections.Generic;

namespace Medical.Web.Api.Models
{

    /// <summary>
    /// Log response info model
    /// </summary>
    public class LogResponseInfo
    {

        #region Constructors

        /// <summary>
        /// Create a new object instance
        /// </summary>
        public LogResponseInfo()
        {
            Date = DateTime.Now;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Request id (trace)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Request sesssion id
        /// </summary>
        public string SessionId { get { return Id.Split('-')[0]; } }

        /// <summary>
        /// Request number
        /// </summary>
        public string RequestNumber { get { return Id.Split('-')[1]; } }

        /// <summary>
        /// Request date time
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Request headers data
        /// </summary>
        public IDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Http status response code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Response body expression
        /// </summary>
        public string Body { get; set; }

        #endregion

    }
}
