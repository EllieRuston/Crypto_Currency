using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Cripto_C.UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class UITests
    {
        IApp app;
        Platform platform;

        public UITests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void DoesButtonUpdateData()
        {
            app.Repl();
            // Arrange
            app.Screenshot("Original Data");
            // Act
            app.WaitForElement(c=> c.Marked("Refresh"));

            app.Tap(c => c.Marked("Refresh"));
            app.Screenshot("Updated Data");
            //Assert
           
        }

        [Test]
        public void AppRepl()
        {
            app.Repl();  
        }

        [Test]
        public void MainPageTest()
        {
            app.WaitForElement(x =>
            x.Marked("cList").Index(0));
            app.Tap(x => x.Marked("cList").Index(0));
        }

    }
}
