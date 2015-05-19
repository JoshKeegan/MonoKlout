using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoKlout
{
    public class KloutAPI
    {
        public static string APIKey { get; set; }
        public string KloutId { get; set; }
        public string TwitterUsername { get; set; }

        // Note: The Twitter username must be applied before using MonoKlout.
        public KloutAPI(string apiKey)
        {
            APIKey = apiKey;
        }

        public KloutAPI(string apiKey, string twitterUsername)
        {
            APIKey = apiKey;
            TwitterUsername = twitterUsername;
        }

        public KloutIdentityResponse GetKloutIdentity()
        {
            if (TwitterUsername == string.Empty)
                throw new Exception("Error: Must set Twitter username before receiving a Klout Id.");

            string request = Request.GenerateIdentityRequest(TwitterUsername);
            KloutIdentityResponse identity = Response.MakeIdentityRequest(request);

            // Store our KloutId
            if (identity != null)
            {
                KloutId = identity.id;
            }

            return identity;
        }

        public KloutScoreResponse GetKloutScore()
        {
            if (KloutId == string.Empty)
                throw new Exception("Error: Must get the user's Klout Id using GetKloutIdentity() before "
                                    + "calling further methods.");

            string request = Request.GenerateRequest(KloutId, Request.scoreRequestExtension);
            KloutScoreResponse score = Response.MakeRequest<KloutScoreResponse>(request);

            return score;
        }

        public List<KloutUserTopicsResponse> GetUserTopics()
        {
            if (KloutId == string.Empty)
                throw new Exception("Error: Must get the user's Klout Id using GetKloutIdentity() before "
                                    + "calling further methods.");

            string request = Request.GenerateRequest(KloutId, Request.userTopicsRequestExtension);
            List<KloutUserTopicsResponse> userTopics = Response.MakeRequest<List<KloutUserTopicsResponse>>(request);

            return userTopics;
        }

        public KloutInfluenceResponse GetInfluence()
        {
            if (KloutId == string.Empty)
                throw new Exception("Error: Must get the user's Klout Id using GetKloutIdentity() before "
                                    + "calling further methods.");

            string request = Request.GenerateRequest(KloutId, Request.influenceRequestExtension);
            KloutInfluenceResponse influence = Response.MakeRequest<KloutInfluenceResponse>(request);

            return influence;
        }
    }
}

