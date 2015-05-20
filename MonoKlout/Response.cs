using System;
using System.IO;
using System.Net;

using Newtonsoft.Json;

namespace MonoKlout
{
    internal static class Response
    {
        internal static T MakeRequest<T>(string requestUrl)
        {
            #if DEBUG
                Console.WriteLine("Inside MakeRequest().");
                Console.WriteLine("Request: {0}", requestUrl);
            #endif
            
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new WebException(string.Format("Server error {0} : {1}",
                                            response.StatusCode,
                                            response.StatusDescription));
                    }

                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string json = reader.ReadToEnd();

                    T responseObj = JsonConvert.DeserializeObject<T>(json);
                    
                    return responseObj;
                }
            }
            catch (WebException e)
            {
                ExceptionHandler.Add(requestUrl, e);

                return default(T);
            }
        }
    }
}

