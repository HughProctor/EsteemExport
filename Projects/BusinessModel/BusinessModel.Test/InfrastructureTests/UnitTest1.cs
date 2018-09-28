using System;
using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessModel.Test.InfrastructureTests
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void SendBasicEmail()
        {
            Email.Send();
        }
    }
}
