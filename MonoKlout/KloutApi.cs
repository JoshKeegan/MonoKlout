using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoKlout
{
    public class KloutApi
    {
        //Private variables
        private readonly string apiKey;

        //Constructor
        public KloutApi(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public KloutIdentityResponse GetKloutIdentity(string twitterUsername)
        {
            if (twitterUsername == null)
            {
                throw new ArgumentNullException("twitterUsername");
            }
            if (twitterUsername.Trim() == "")
            {
                throw new ArgumentException("Twitter Username cannot be whitespace", "twitterUsername");
            }

            string request = Request.GenerateIdentityRequest(twitterUsername);
            KloutIdentityResponse identity = Response.MakeRequest<KloutIdentityResponse>(request, apiKey);

            return identity;
        }

        public KloutScoreResponse GetKloutScore(string kloutId)
        {
            checkKloutId(kloutId);

            string request = Request.GenerateRequest(kloutId, Request.SCORE_REQUEST_EXTENSION);
            KloutScoreResponse score = Response.MakeRequest<KloutScoreResponse>(request, apiKey);

            return score;
        }

        public List<KloutUserTopicsResponse> GetUserTopics(string kloutId)
        {
            checkKloutId(kloutId);

            string request = Request.GenerateRequest(kloutId, Request.USER_TOPICS_REQUEST_EXTENSION);
            List<KloutUserTopicsResponse> userTopics = Response.MakeRequest<List<KloutUserTopicsResponse>>(request, apiKey);

            return userTopics;
        }

        public KloutInfluenceResponse GetInfluence(string kloutId)
        {
            checkKloutId(kloutId);

            string request = Request.GenerateRequest(kloutId, Request.INFLUENCE_REQUEST_EXTENSION);
            KloutInfluenceResponse influence = Response.MakeRequest<KloutInfluenceResponse>(request, apiKey);

            return influence;
        }

        //Private Methods
        private static void checkKloutId(string kloutId)
        {
            if (kloutId == null)
            {
                throw new ArgumentNullException("kloutId");
            }
            if (kloutId.Trim() == "")
            {
                throw new ArgumentException("Klout ID cannot be whitespace", "kloutId");
            }
        }
    }
}

