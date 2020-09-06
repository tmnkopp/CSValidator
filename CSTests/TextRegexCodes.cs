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
using System.Text.RegularExpressions;

namespace UnitTests
{ 
    [TestClass]
    public class TextRegexCodes
    {
        [TestMethod]
        public void MaskReplacesRegex()
        {
            string mask = "*";
            string replaceRegex = @"\d";
            string expected = "12.345.***.***"; 
            string masked = "";
            string target = "12.345.123.123";
            Match match = Regex.Match(target, @"^\d{1,3}\.\d{1,3}\.(\d{1,3}\.\d{1,3})$");
            int startPos = 0;
            if (match.Success) 
                startPos = match.Groups[1].Index;
            masked = target.Substring(0, startPos); 
            for (int pos = startPos; pos < target.Length; pos++)
            {
                string chr = target.Substring(pos, 1);
                match = Regex.Match(chr, replaceRegex);
                if (match.Success)
                    masked += "*";
                else
                    masked += chr;  
            }
            
            Assert.AreEqual(expected, masked);
        }
        [TestMethod]
        public void  RegexValidatesEitherOr()
        {
            string mask = "*";
            string replaceRegex = @"\d|-|n";
            string expected = "123n****";
            string masked = "";
            string target = "123n4567";
            Match match = Regex.Match(target, @"^\d{3}n(\d{4})$");
            int startPos = 0;
            if (match.Success)
                startPos = match.Groups[1].Index;
            masked = target.Substring(0, startPos);
            for (int pos = startPos; pos < target.Length; pos++)
            {
                string chr = target.Substring(pos, 1);
                match = Regex.Match(chr, replaceRegex);
                if (match.Success)
                    masked += mask;
                else
                    masked += chr;
            }

            Assert.AreEqual(expected, masked);
        }
    } 
}
