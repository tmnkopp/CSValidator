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
    public class TestValidationCodes
    {

        [TestMethod]
        public void Array_Resolves_ToSTR()
        { 
            string[] arr = "CODE1,CODE2,CODE3".Split(',');
            string actual = string.Join(",", arr);
            Assert.AreEqual("CODE1,CODE2,CODE3", actual);
        }
        [TestMethod]
        public void ValidationCodes_Split()
        {
            List<string> codes = new List<string>();
            string[] arr = "CODE1,CODE2,CODE3".Split(',');
            foreach (string item in arr)
            {
                codes.Add(item);
            }
            Assert.AreEqual(codes.Count, arr.Length);
        }
        [TestMethod]
        public void ValidationCodes_Found()
        {
            List<string> codes = new List<string>();
            string[] arr = "CODE1,CODE2,CODE3".Split(',');
            foreach (string item in arr)
            {
                codes.Add(item);
            }
            Assert.IsTrue(codes.Contains("CODE2"));
        }

    }
   

}
