using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using CSValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Web.UI;

namespace CSTests
{
    [TestClass]
    public class TestCSValidators
    {
        [TestMethod]
        public void Validator_Returns_Error_Message_Caption()
        {
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "##########";
            rtb.ID = "txtPhoneNumber";
            Validator validation = new Validator(rtb); 
            bool isValid = validation.IpAddress().IsValid;
            string message = validation.ErrorMessage;
            Assert.IsTrue(message.Contains("Phone Number"));
        }
        [TestMethod]
        public void Validator_Validates_REQUIRE()
        {
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "";
            rtb.ID = "IPAddress";
            Validator validation = new Validator(rtb); 
            Assert.IsTrue(validation.Require().IsValid);
        }
        [TestMethod]
        public void Validator_Validates_SingleCode()
        {
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "123";
            rtb.ID = "IPAddress";
            Validator validation = new Validator(rtb); 
            Assert.IsTrue(validation.EqualsAny(new string[] { "22", "123" }).IsValid);
        }
        [TestMethod]
        public void Validator_Validates_MultiCodes()
        {
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "123.123.123.123";
            rtb.ID = "IPAddress";
            Validator validation = new Validator(rtb); 
            validation.Phone().IpAddress().ValidateAny();
            Assert.IsTrue(validation.IsValid);
        } 
    }
}
