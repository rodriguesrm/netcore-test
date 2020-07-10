using Medical.Web.Api.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Medical.Web.Api
{

    /// <summary>
    /// Provides helpers methods
    /// </summary>
    public static class Helpers
    {


        /// <summary>
        /// Get critical/error notification for invalid model state
        /// </summary>
        /// <param name="modelState">Model state dictionary</param>
        public static IEnumerable<GenericNotificationResponse> GetCriticals(ModelStateDictionary modelState)
        {

            IEnumerable<GenericNotificationResponse> result = modelState.Select(x => new GenericNotificationResponse()
            {
                Property = x.Key,
                Message = string.Join('|', x.Value.Errors.Select(e => e.ErrorMessage))
            });

            return result;
        }

        /// <summary>
        /// Get critical/error notification for messages dictionary
        /// </summary>
        /// <param name="dic">Messages dictionary</param>
        public static IEnumerable<GenericNotificationResponse> GetCriticals(IDictionary<string, string> dic)
        {

            IEnumerable<GenericNotificationResponse> result = dic.Select(s => new GenericNotificationResponse()
            {
                Property = s.Key,
                Message = s.Value
            });

            return result;
        }

    }

}
