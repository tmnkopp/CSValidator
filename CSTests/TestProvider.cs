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
            ValidationDefinitionProvider p = new ValidationDefinitionProvider();
            Assert.IsNotNull(p.GetItems()); 
        } 
    }
}
