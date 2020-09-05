using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Web.UI;

namespace UnitTests
{ 
    [TestClass]
    public class TestExtentions
    {
        [TestMethod]
        public void TestToProper()
        {
            string target = "rcbIPAddress";
            string expected = "IP Address"; 
            string actual = target.ToProperCase();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestCAPSToProper()
        {
            string target = "rcbCVENUM";
            string expected = "CVENUM";
            string actual = target.ToProperCase();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestPropValGetter()
        { 
            RadTextBox rtb = new RadTextBox();
            rtb.Text = "IP Address";
            rtb.ID = "id"; 
            string actual = rtb.GetProperty("Text");
            Assert.IsNotNull(actual);
        }

    }
   

}
