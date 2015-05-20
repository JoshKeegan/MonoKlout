using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using MonoKlout;
using MonoKlout.Exceptions;

namespace UnitTests
{
    [TestFixture]
    public class KloutApiTests
    {
        //Constants
        private const string KLOUT_API_KEY_FILE_PATH = "../../KLOUT_API_KEY";

        //Variables
        private static string kloutApiKey;

        [SetUp]
        public void SetUp()
        {
            kloutApiKey = File.ReadAllText(KLOUT_API_KEY_FILE_PATH).Trim();
        }

        [Test]
        public void TestGetKloutIdentity()
        {
            KloutApi kloutApi = new KloutApi(kloutApiKey);
            const string expected = "42502726249709279";

            KloutIdentityResponse response = kloutApi.GetKloutIdentity("kevin");

            Assert.AreEqual(expected, response.id);
        }

        [Test]
        public void TestGetKloutIdentityDoesntExist()
        {
            KloutApi kloutApi = new KloutApi(kloutApiKey);

            KloutIdentityResponse response = kloutApi.GetKloutIdentity("kshkahashfkljah123");

            Assert.IsNull(response);
        }

        [Test]
        public void TestInvalidApiKey()
        {
            KloutApi kloutApi = new KloutApi("blah");

            KloutIdentityResponse response = kloutApi.GetKloutIdentity("kevin");

            //Response should be null
            Assert.IsNull(response);

            //Exception has been logged
            WebException e = ExceptionHandler.GetLastException();
            Assert.NotNull(e);

            //Should be a Mashery Exception
            MasheryException me = e as MasheryException;
            Assert.NotNull(me);

            Assert.AreEqual("ERR_403_DEVELOPER_INACTIVE", me.ErrorCode);
        }
    }
}
