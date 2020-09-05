using System;
using System.Linq;
using System.Web.UI.WebControls;
using CSValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Web.UI;

namespace UnitTests
{
    [TestClass]
    public class TestValidators
    { 
        [TestMethod]
        public void Validator_Validates()
        { 
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "123.123.123.123";
            rtb.ID = "IPAddress";

            Validator validator = new Validator(rtb);
            var validated = validator.Validate("IPADDRESS"); 
            Assert.IsTrue(validated); 
        }
        [TestMethod]
        public void Validator_Validates_Any()
        {
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "CVE-1234-123456";
            rtb.ID = "CVE"; 
            Validator validator = new Validator(rtb);
            var validated = validator.ValidateAny("IPADDRESS,CVE"); 
            Assert.IsTrue(validated);
        }
        [TestMethod]
        public void Validator_Sends_Validation_Message()
        {
            string message = null;
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "~~CVE-1234-12345";
            rtb.ID = "CommonVulunerabilityEnumCode"; 
            Validator validator = new Validator(rtb); 
            
            if (!validator.ValidateAny("IPADDRESS,CVE,PHONE"))
            {
                message = validator.ErrorMessage;
            } 
            Assert.IsNotNull(message);
        }
    }
}
