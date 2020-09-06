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
            ValidationTypeProvider p = new ValidationTypeProvider();
            Assert.IsNotNull(p.GetItems()); 
        } 
    }
}
