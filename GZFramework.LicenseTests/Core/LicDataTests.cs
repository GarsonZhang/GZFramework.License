using Microsoft.VisualStudio.TestTools.UnitTesting;
using GZFramework.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GZFramework.License.Core;

namespace GZFramework.License.Core
{
    [TestClass()]
    public class LicDataTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            LicData data = new LicData();
            data.ExtraContent = "123";
            string str = ConvertObject.ConvertObjectToXML(typeof(LicData), data);
            object d2 = ConvertObject.ConvertXMLToObject(typeof(LicData), str);
            Assert.AreEqual("", "");
        }
    }
}