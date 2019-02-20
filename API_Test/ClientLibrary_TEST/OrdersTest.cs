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
        //Add Order & Fetch single OrderItem using GetOrderItem(..)
        public void ClientLibTest001() 
        {
            Task.Run(async () =>
            {
                Orders myOrders = new Orders();
                Assert.IsTrue(await myOrders.Clear());
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 1, quantity = 2/*, unitprice = 2.33 */}));
                OrderItem orderItems = await myOrders.GetOrderItem(1);
                Assert.IsTrue(orderItems.productid == 1 && orderItems.quantity == 2/* && orderItems.unitprice == 2.33*/);
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        //Add Order more than once, fetch List<OrderItem> using GetAll(..) to test Qty increase
        public void ClientLibTest002() 
        {
            Task.Run(async () =>
            {
                Orders myOrders = new Orders();
                Assert.IsTrue(await myOrders.Clear()); //need to clear Orders before starting test to prevent previous test residues
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 1, quantity = 2/*, unitprice = 2.33 */}));
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 1, quantity = 3/*, unitprice = 2.33 */}));
                List<OrderItem> orderItems = await myOrders.GetAll();
                Assert.IsTrue(orderItems.Any(itm => itm.productid == 1 && itm.quantity == 5 /*&& itm.unitprice == 2.33*/));
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        //Add Order more than once to test Qty increase & Overwrite with Update()
        public void ClientLibTest003()
        {
            Task.Run(async () =>
            {
                Orders myOrders = new Orders();
                Assert.IsTrue(await myOrders.Clear()); //need to clear Orders before starting test to prevent previous test residues
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 1, quantity = 2/*, unitprice = 2.33 */}));
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 1, quantity = 3/*, unitprice = 2.33 */}));
                Assert.IsTrue(await myOrders.Update(1, new OrderItem() { productid = 1, quantity = 1/*, unitprice = 2.33 */}));
                List<OrderItem> orderItems = await myOrders.GetAll();
                Assert.IsTrue(orderItems.Any(itm => itm.productid == 1 && itm.quantity == 1/* && itm.unitprice == 2.33*/));
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        //Add multiple Orders with different product id, fetch multiple, test Update() and Add() when using multiple OrderItems
        public void ClientLibTest004()
        {
            Task.Run(async () =>
            {
                Orders myOrders = new Orders();
                Assert.IsTrue(await myOrders.Clear()); //need to clear Orders before starting test to prevent previous test residues
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 1, quantity = 2/*, unitprice = 2.33 */}));
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 9, quantity = 1/*, unitprice = 1.99 */}));
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 55, quantity = 4/*, unitprice = 0.22 */}));
                Assert.IsTrue(await myOrders.Update(1, new OrderItem() { productid = 1, quantity = 1/*, unitprice = 2.33 */}));
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 55, quantity = 1/*, unitprice = 0.22 */}));
                List<OrderItem> orderItems = await myOrders.GetAll();
                Assert.AreEqual(orderItems.Count, 3);
                Assert.IsTrue(orderItems.Any(itm => itm.productid == 1 && itm.quantity == 1));
                Assert.IsTrue(orderItems.Any(itm => itm.productid == 9 && itm.quantity == 1));
                Assert.IsTrue(orderItems.Any(itm => itm.productid == 55 && itm.quantity == 5));
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        //Test Remove Single Item From Order
        public void ClientLibTest005()
        {
            Task.Run(async () =>
            {
                Orders myOrders = new Orders();
                Assert.IsTrue(await myOrders.Clear()); //need to clear Orders before starting test to prevent previous test residues
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 1, quantity = 2/*, unitprice = 2.33 */}));
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 9, quantity = 1/*, unitprice = 1.99 */}));
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 55, quantity = 4/*, unitprice = 0.22 */}));
                Assert.IsTrue(await myOrders.Update(1, new OrderItem() { productid = 1, quantity = 1/*, unitprice = 2.33 */}));
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 55, quantity = 1/*, unitprice = 0.22 */}));
                List<OrderItem> orderItems = await myOrders.GetAll();
                Assert.AreEqual(orderItems.Count, 3);
                Assert.IsTrue(await myOrders.Remove(9));
                orderItems = await myOrders.GetAll();
                Assert.AreEqual(orderItems.Count, 2);
                Assert.IsFalse(orderItems.Exists(itm => itm.productid == 9));
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        //Test Remove Single Item From Order
        public void ClientLibTest006()
        {
            Task.Run(async () =>
            {
                Orders myOrders = new Orders();
                Assert.IsTrue(await myOrders.Clear()); //need to clear Orders before starting test to prevent previous test residues
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 1, quantity = 2/*, unitprice = 2.33 */}));
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 9, quantity = 1/*, unitprice = 1.99 */}));
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 55, quantity = 4/*, unitprice = 0.22 */}));
                Assert.IsTrue(await myOrders.Update(1, new OrderItem() { productid = 1, quantity = 1/*, unitprice = 2.33 */}));
                Assert.IsTrue(await myOrders.Add(new OrderItem() { productid = 55, quantity = 1/*, unitprice = 0.22 */}));
                List<OrderItem> orderItems = await myOrders.GetAll();
                Assert.AreEqual(orderItems.Count, 3);
                Assert.IsTrue(await myOrders.Clear());
                orderItems = await myOrders.GetAll();
                Assert.AreEqual(orderItems.Count, 0);
            }).GetAwaiter().GetResult();
        }
    }
}
