using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace Owin.Web.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            new object().ShouldNotBeNull();
        }
    }
}
