using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using MonoKlout;
using MonoKlout.Exceptions;

using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class KloutApiTests
    {
        //Constants
        private readonly string KLOUT_API_KEY_FILE_PATH = Path.Combine(
            TestContext.CurrentContext.TestDirectory, "../../KLOUT_API_KEY");

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

            Assert.AreEqual(expected, response.Id);
        }

        [Test]
        public void TestGetKloutIdentityDoesntExist()
        {
            KloutApi kloutApi = new KloutApi(kloutApiKey);

            KloutIdentityResponse response = kloutApi.GetKloutIdentity("kshkahashfkljah123");

            //Response should be null
            Assert.IsNull(response);

            //Exception has been logged
            WebException e = ExceptionHandler.GetLastException();
            Assert.NotNull(e);

            //Should be a Mashery Exception
            MasheryException me = e as MasheryException;
            Assert.NotNull(me);

            Assert.IsTrue(me.IsNotFound);
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

            Assert.IsTrue(me.InvalidApiKey);
        }

        [Test]
        public void TestMasheryExceptionUrlDoesntIncludeApiKey()
        {
            KloutApi kloutApi = new KloutApi(kloutApiKey);

            KloutIdentityResponse response = kloutApi.GetKloutIdentity("kshkahashfkljah123");

            //Response should be null
            Assert.IsNull(response);

            //Exception has been logged
            WebException e = ExceptionHandler.GetLastException();
            Assert.NotNull(e);

            //Should be a Mashery Exception
            MasheryException me = e as MasheryException;
            Assert.NotNull(me);

            Assert.IsTrue(me.IsNotFound);

            Assert.IsFalse(me.Url.Contains(kloutApiKey));
        }
    }
}
