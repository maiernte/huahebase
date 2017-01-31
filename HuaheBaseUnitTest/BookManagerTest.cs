using System;
using HuaheData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuaheBaseUnitTest
{
    [TestClass]
    public class BookManagerTest
    {
        [TestMethod]
        public void BaseTest()
        {
            BookManager bk = new BookManager(@"C:\Users\f040371\Desktop\books\test.gua");
            string[] res = bk.Read();
            Assert.IsTrue(true);

            res = bk.Create();
            int i = 0;
        }
    }
}
