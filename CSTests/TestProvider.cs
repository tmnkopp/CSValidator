using System;
using CSValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Web.UI;

namespace UnitTests
{
    [TestClass]
    public class TestProvider
    {
        [TestMethod]
        public void ProviderProvides()
        {
            ValidationProvider p = new ValidationProvider();
            Assert.IsNotNull(p.GetValidationExpressions()); 
        }
        [TestMethod]
        public void ValidateAnyProvides()
        {
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "123.123.123.123";
            rtb.ID = "IPAddress";

            Validator validator = new Validator(rtb);
            bool isvalid = validator.ValidateAny("IPADDRESS,PHONE");
            Assert.IsTrue(isvalid);
        }

    }
}
