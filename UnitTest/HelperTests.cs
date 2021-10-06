using Microsoft.VisualStudio.TestTools.UnitTesting;
using F1Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tools.Tests
{
    [TestClass()]
    public class HelperTests
    {
        [TestMethod()]
        public void CheckUpdateTest()
        {
            Helper.CheckUpdate();
        }
    }
}