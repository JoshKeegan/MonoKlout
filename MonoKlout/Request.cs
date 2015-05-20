using System;

namespace MonoKlout
{
    /*
     * The purpose of this class is to generate the appropriate request for the user's need.
     */
    internal static class Request
    {
        // Base request Urls
        private const string BASE_REQUEST_URL = "https://api.klout.com/v2/user.json/";
        private const string INITIAL_REQUEST_URL = "https://api.klout.com/v2/identity.json/twitter?screenName=";
        private const string API_KEY_URL = "?key=";

        // Extension Urls
        internal const string SCORE_REQUEST_EXTENSION = "/score";
        internal const string INFLUENCE_REQUEST_EXTENSION = "/influence";
        internal const string USER_TOPICS_REQUEST_EXTENSION = "/topics";

        internal static string GenerateIdentityRequest(string apiKey, string twitterUsername)
        {
            #if DEBUG
                Console.WriteLine("Inside GenerateIdentityRequest().");
                Console.WriteLine("Generating Identity Request for {0}.", twitterUsername);
            #endif

            string request = INITIAL_REQUEST_URL + twitterUsername + "&key=" + apiKey;

            return request;
        }

        internal static string GenerateRequest(string apiKey, string kloutId, string requestExtension)
        {
            #if DEBUG
                Console.WriteLine("Inside GenerateRequest().");
                Console.WriteLine("Generating Request for {0}.", kloutId);
                Console.WriteLine("Request Extension: {0}", requestExtension);
            #endif

            string request = BASE_REQUEST_URL + kloutId + requestExtension + API_KEY_URL + apiKey;

            return request;
        }
    }
}
