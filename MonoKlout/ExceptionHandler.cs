using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using MonoKlout.Exceptions;

namespace MonoKlout
{
    public class ExceptionHandler
    {
        [ThreadStatic]
        private static ExceptionHandler currentThreadExceptionHandler;
        internal static ExceptionHandler CurrentThreadExceptionHander
        {
            get
            {
                if (currentThreadExceptionHandler == null)
                {
                    currentThreadExceptionHandler = new ExceptionHandler();
                }
                return currentThreadExceptionHandler;
            }
        }

        //Private Variables
        private readonly List<WebException> exceptions = new List<WebException>();

        internal static void Add(string url, WebException e)
        {
            //If already a mashery exception, or cannot be made into one
            if (e is MasheryException || !MasheryException.IsMashery(e))
            {
                CurrentThreadExceptionHander.exceptions.Add(e);
            }
            else //Otherwise this can be made into a MasheryException
            {
                HttpWebResponse response = (HttpWebResponse) e.Response;
                HttpStatusCode statusCode = response.StatusCode;
                string errorCode = response.Headers[MasheryException.ERROR_CODE_HEADER];
                string errorDetail = response.Headers[MasheryException.ERROR_DETAIL_HEADER];

                MasheryException me = new MasheryException(url, statusCode, errorCode, errorDetail, e);
                Add(url, me);
            }
        }

        public static void ClearExceptions()
        {
            CurrentThreadExceptionHander.exceptions.Clear();
        }

        public static WebException GetLastException()
        {
            if (CurrentThreadExceptionHander.exceptions.Any())
            {
                return CurrentThreadExceptionHander.exceptions[CurrentThreadExceptionHander.exceptions.Count - 1];
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<WebException> GetExceptions()
        {
            return CurrentThreadExceptionHander.exceptions;
        }
    }
}
