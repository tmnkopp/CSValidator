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
        public void DelegateValidator_Returns__ValidatorDefinitionCaption()
        { 
            Validator validation = new Validator("Testy McTestface");
            bool isValid = validation.MinLength(121).IsValid;
            string message = validation.ErrorMessage;
            Assert.IsTrue(message.Contains("121"));
        }
        [TestMethod]
        public void DoesNotEqualAny_Returns__List()
        {
            Validator validation = new Validator("Testy McTestface");
            validation.TargetCaption = "Testy McTestface";
            bool isValid = validation.DoesNotEqualAny(new string[] {"321","123" }).IsValid;
            string message = validation.ErrorMessage;
            Assert.IsTrue(message.Contains("321, 123"));
        }
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
            Assert.IsFalse(validation.Require().IsValid);
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
        [TestMethod]
        public void Validator_Validates_MultiCodesFalse()
        {
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "X";
            rtb.ID = "Nothing";
            Validator validation = new Validator(rtb);
            validation.Phone().IpAddress().MinLength(2).ValidateAny();
            Assert.IsFalse(validation.IsValid);
        }
    }
}
