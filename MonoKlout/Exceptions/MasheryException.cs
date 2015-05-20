using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MonoKlout.Exceptions
{
    public class MasheryException : WebException
    {
        //Constants
        internal const string ERROR_CODE_HEADER = "X-Mashery-Error-Code";
        internal const string ERROR_DETAIL_HEADER = "X-Error-Detail-Header";

        //Public Variables
        public readonly string Url;
        public readonly HttpStatusCode StatusCode;
        public readonly string ErrorCode;
        public readonly string ErrorDetail;
        public readonly WebException WebException;
        public readonly DateTime CreatedAt;

        //Common 
        public bool ExceededPerSecondRateLimit
        {
            get
            {
                return StatusCode == HttpStatusCode.Forbidden && 
                    ErrorCode == "ERR_403_DEVELOPER_OVER_QPS";
            }
        }

        public bool InvalidApiKey
        {
            get
            {
                return StatusCode == HttpStatusCode.Forbidden && 
                    ErrorCode == "ERR_403_DEVELOPER_INACTIVE";
            }
        }

        public bool IsNotFound
        {
            get
            {
                return StatusCode == HttpStatusCode.NotFound;
            }
        }

        public bool IsApiUnavailable
        {
            get
            {
                return StatusCode == HttpStatusCode.ServiceUnavailable ||
                       StatusCode == HttpStatusCode.GatewayTimeout;
            }
        }

        //Constructors
        public MasheryException(string url, HttpStatusCode statusCode, string errorCode, 
            string errorDetail, WebException webException)
            : base(string.Format("{0} Web Request Failed", url))
        {
            Url = url;
            StatusCode = statusCode;
            ErrorCode = errorCode;
            ErrorDetail = errorDetail;
            WebException = webException;
            CreatedAt = DateTime.Now;
        }

        //Public Methods
        public override string ToString()
        {
            string createdAtLn = string.Format("CreatedAt: {0}", CreatedAt);
            string urlLn = string.Format("URL: {0}", Url);
            string statusCodeLn = string.Format("Status Code: {0} ({1})", StatusCode, (int) StatusCode);
            string errorCodeLn = string.Format("Error Code: {0}", ErrorCode);
            string errorDetailLn = string.Format("Error Detail: {0}", ErrorDetail);

            return string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}",
                createdAtLn, urlLn, statusCodeLn, errorCodeLn, errorDetailLn);
        }

        //Static methods
        internal static bool IsMashery(WebException e)
        {
            if (e == null || 
                !(e.Response is HttpWebResponse) || // For the status code
                e.Response.Headers == null) // Must have some headers
            {
                return false;
            }

            HttpWebResponse response = (HttpWebResponse) e.Response;

            // Must be either a 404, 503, 504 or have the Mashable error headers
            return response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.ServiceUnavailable ||
                response.StatusCode == HttpStatusCode.GatewayTimeout ||
                (response.Headers.AllKeys.Contains(ERROR_CODE_HEADER) &&
                   response.Headers.AllKeys.Contains(ERROR_DETAIL_HEADER));
        }
    }
}
