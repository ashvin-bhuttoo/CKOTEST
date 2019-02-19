using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Models;

namespace ClientLibrary_TEST
{
    [TestClass]
    public class OrdersTest
    {
        [TestMethod]
        //Add Order & Fetch
        public void ClientLibTest001() 
        {
            Task.Run(async () =>
            {
                Orders myLib = new Orders();
                Assert.IsTrue(await myLib.Clear());
                Assert.IsTrue(await myLib.Add(new OrderItem() { productid = 1, quantity = 2, unitprice = 2.33 }));
                List<OrderItem> orderItems = await myLib.GetAll();
                Assert.AreEqual(orderItems.Count, 1);
                Assert.IsTrue(orderItems.Any(itm => itm.productid == 1 && itm.quantity == 2 && itm.unitprice == 2.33));
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        //Add Order more than once to test Qty increase
        public void ClientLibTest002() 
        {
            Task.Run(async () =>
            {
                Orders myLib = new Orders();
                Assert.IsTrue(await myLib.Clear()); //need to clear Orders before starting test to prevent previous test residues
                Assert.IsTrue(await myLib.Add(new OrderItem() { productid = 1, quantity = 2, unitprice = 2.33 }));
                Assert.IsTrue(await myLib.Add(new OrderItem() { productid = 1, quantity = 2, unitprice = 2.33 }));
                List<OrderItem> orderItems = await myLib.GetAll();
                Assert.IsTrue(orderItems.Any(itm => itm.productid == 1 && itm.quantity == 4 && itm.unitprice == 2.33));
            }).GetAwaiter().GetResult();
        }

    }
}
