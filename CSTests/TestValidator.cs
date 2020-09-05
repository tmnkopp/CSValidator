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
        public void CSValidator_Validates_REQUIRE()
        {
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "";
            rtb.ID = "IPAddress";
            ValidationBuilder validation = new ValidationBuilder(rtb);
            validation.AddValidation("REQUIRE");
            Assert.IsTrue(validation.IsValid);
        }
        [TestMethod]
        public void CSValidator_Validates_SingleCode()
        {
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "123.123.123.123";
            rtb.ID = "IPAddress";
            ValidationBuilder validation = new ValidationBuilder(rtb);
            validation.AddValidation("IPADDRESS");  
            Assert.IsTrue(validation.IsValid);
        }
        [TestMethod]
        public void CSValidator_Validates_MultiCodes()
        {
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "123.123.123.123";
            rtb.ID = "IPAddress";
            ValidationBuilder validation = new ValidationBuilder(rtb);
            validation.AddValidation("PHONE");
            validation.AddValidation("IPADDRESS");
            validation.ValidateAny();
            Assert.IsTrue(validation.IsValid);
        }
        [TestMethod]
        public void CSValidator_Validates_MinMax()
        {
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "123.123.123.123";
            rtb.ID = "IPAddress";
            ValidationBuilder validation = new ValidationBuilder(rtb)
                .MaxLength(999)
                .MinLength(100)
                .ValidateAny();
            bool isValid = validation.IsValid;
            string message = validation.ErrorMessage;
            Assert.IsNotNull(message);
        } 

    }
}
