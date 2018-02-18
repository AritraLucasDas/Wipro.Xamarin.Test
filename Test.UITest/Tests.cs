using System;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Test.UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void HaveElemenst()
        {
            Thread.Sleep(5000);
            int res = app.Query(q => q.Id("List").Child()).Length;
            Assert.IsTrue(res>0 );
        }
        [Test]
        public void NoElemenst()
        {
            Thread.Sleep(5000);
            int res = app.Query(q => q.Id("List").Child()).Length;
            Assert.IsTrue(res == 0);
        }
    }
}

