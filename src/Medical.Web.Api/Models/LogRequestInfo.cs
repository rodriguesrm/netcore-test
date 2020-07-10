using System;
using System.Collections.Generic;

namespace Medical.Web.Api.Models
{

    /// <summary>
    /// Log request info model
    /// </summary>
    public class LogRequestInfo
    {

        #region Constructors

        /// <summary>
        /// Create a new object instance
        /// </summary>
        public LogRequestInfo()
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
        /// Request session id
        /// </summary>
        public string SessionId { get { return Id.Split('-')[0]; } }

        /// <summary>
        /// Request number
        /// </summary>
        public string RequestNumber { get { return Id.Split('-')[1]; } }

        /// <summary>
        /// Request date time
        /// </summary>
        public DateTime Date { get; protected set; }

        /// <summary>
        /// Connection schema (http/https/etc)
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Headers data
        /// </summary>
        public IDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Raw url request
        /// </summary>
        public string RawUrl
        {
            get
            {
                string result = $"{Scheme}://{Host}{Path}{QueryString}";
                return result;
            }
        }

        /// <summary>
        /// Request path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Http verb method (GET, POST, PUT, DELETE, HEAD, etc)
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Request host name
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Query-string expression
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// Request body expression
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Client certificate expression
        /// </summary>
        public string ClientCertificate { get; set; }

        /// <summary>
        /// Local ip address
        /// </summary>
        public string LocalIpAddress { get; set; }

        /// <summary>
        /// Local port number
        /// </summary>
        public int LocalPort { get; set; }

        /// <summary>
        /// Remove ip address
        /// </summary>
        public string RemoteIpAddress { get; set; }

        /// <summary>
        /// Remote port number
        /// </summary>
        public int RemotePort { get; set; }

        #endregion

    }
}
