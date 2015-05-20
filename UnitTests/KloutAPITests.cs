﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using MonoKlout;

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
    }
}
