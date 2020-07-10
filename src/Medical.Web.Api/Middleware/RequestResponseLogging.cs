using Medical.Web.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Medical.Web.Api.Middleware
{

    /// <summary>
    /// Request/Response logginf middleware
    /// </summary>
    public class RequestResponseLogging
    {

        #region Local objects/variables

        private readonly RequestDelegate _next;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new middleware instance
        /// </summary>
        /// <param name="next">RequestDelegate object</param>
        public RequestResponseLogging(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Local methods

        /// <summary>
        /// Extract request body expression
        /// </summary>
        /// <param name="req">Http request object</param>
        private async Task<string> GetBodyRequest(HttpRequest req)
        {

            if ((req.ContentLength ?? 0) == 0)
                return null;

            if (req.ContentType.StartsWith("multipart/form-data"))
                return "*** MULTIPART/FORM-DATA ***";

            Stream body = req.Body;
            byte[] buffer = new byte[Convert.ToInt32(req.ContentLength)];

            await body.ReadAsync(buffer, 0, buffer.Length);

            string result = Encoding.UTF8.GetString(buffer);

            return result;

        }

        /// <summary>
        /// Extract response body expression
        /// </summary>
        /// <param name="resp">Http response object</param>
        private async Task<string> GetBodyResponse(HttpResponse resp)
        {
            resp.Body.Seek(0, SeekOrigin.Begin);
            string textResp = await new StreamReader(resp.Body).ReadToEndAsync();
            resp.Body.Seek(0, SeekOrigin.Begin);
            return textResp;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Capture request data before and after the action/endpoint execution
        /// </summary>
        /// <param name="ctx">Http context object</param>
        public async Task Invoke(HttpContext ctx)
        {

            ILogger logger = ctx.RequestServices.GetService<ILogger>();

            Stream stmBody = ctx.Response.Body;
            ctx.Request.EnableBuffering();

            LogRequestInfo reqInfo = new LogRequestInfo()
            {
                Id = ctx.TraceIdentifier.Replace(":", "-"),
                Scheme = ctx.Request.Scheme,
                Headers = ctx.Request.Headers.ToDictionary(k => k.Key, v => v.Value.ToString()),
                Path = ctx.Request.Path,
                Method = ctx.Request.Method.ToUpper(),
                Host = ctx.Request.Host.ToString(),
                QueryString = ctx.Request.QueryString.Value,
                Body = await GetBodyRequest(ctx.Request),
                ClientCertificate = ctx.Connection.ClientCertificate?.SerialNumber,
                LocalIpAddress = ctx.Connection.LocalIpAddress.ToString(),
                LocalPort = ctx.Connection.LocalPort,
                RemoteIpAddress = ctx.Connection.RemoteIpAddress.ToString(),
                RemotePort = ctx.Connection.RemotePort
            };

            ctx.Request.Body.Position = 0;

            string logReqMsg = JsonSerializer.Serialize(reqInfo, new JsonSerializerOptions() { WriteIndented = true });

            logger.Information(logReqMsg);

            try
            {

                ctx.Response.Body = new MemoryStream();

                await _next.Invoke(ctx);

                string resp = null;
                if (ctx.Response.StatusCode != StatusCodes.Status204NoContent)
                {
                    if (ctx.Response.ContentType != null && ctx.Response.ContentType.StartsWith("application/json"))
                    {
                        resp = await GetBodyResponse(ctx.Response);
                        if (!string.IsNullOrWhiteSpace(resp))
                        {
                            byte[] bytesResp = Encoding.UTF8.GetBytes(resp);
                            await stmBody.WriteAsync(bytesResp, 0, bytesResp.Length);
                        }
                    }
                    else
                    {
                        ctx.Response.Body.Position = 0;
                        await ctx.Response.Body.CopyToAsync(stmBody);
                        resp = "*** BINARY CONTENT ***";
                    }

                }

                LogResponseInfo respInfo = new LogResponseInfo()
                {
                    Id = ctx.TraceIdentifier.Replace(":", "-"),
                    Headers = ctx.Response.Headers.ToDictionary(k => k.Key, v => v.Value.ToString()),
                    StatusCode = ctx.Response.StatusCode,
                    Body = resp
                };

                string logRespMsg = JsonSerializer.Serialize(respInfo, new JsonSerializerOptions() { WriteIndented = true });

                logger.Information(logRespMsg);

            }
            catch (Exception ex)
            {

                Exception baseEx = ex.GetBaseException();
                GerericExceptionResponse exResp = new GerericExceptionResponse("500", baseEx.Message);

                string response = JsonSerializer.Serialize(exResp);
                byte[] dataResp = Encoding.UTF8.GetBytes(response);

                ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
                ctx.Response.ContentType = "application/json";

                await stmBody.WriteAsync(dataResp, 0, dataResp.Length);

                logger.Error(ex, ex.Message);
            }

        }

        #endregion

    }

}
