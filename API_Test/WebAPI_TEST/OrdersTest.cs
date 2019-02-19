using System;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI;
using WebAPI.Models;

namespace WebAPI_TEST
{
    [TestClass]
    public class OrdersTest
    {
        [TestMethod]
        public void WebApiTest001() //Add Order & Fetch
        {
            var Orders = new WebAPI.Controllers.OrdersController();
            Orders.setFakeHTTPContext(); //a fake http context is set up for test purposes
            Assert.IsTrue(Orders.Delete().IsSuccessStatusCode);

            //PUT product with id 1 & Qty 2
            OrderItem item001 = new OrderItem() {productid = 1, quantity = 2, unitprice = 2.3};
            Assert.AreEqual(Orders.Put(item001).StatusCode, HttpStatusCode.OK);

            //GET & assert Orders content for product ID 1
            Assert.AreEqual(Orders.Get(1).quantity, 2);
        }

        [TestMethod]
        public void WebApiTest002() //Add Order more than once to test Qty increase
        {
            var Orders = new WebAPI.Controllers.OrdersController();
            Orders.setFakeHTTPContext(); //a fake http context is set up for test purposes
            Assert.IsTrue(Orders.Delete().IsSuccessStatusCode);

            //PUT product with id 1 & Qty 2
            OrderItem item001 = new OrderItem() { productid = 1, quantity = 2, unitprice = 2.3 };
            Assert.AreEqual(Orders.Put(item001).StatusCode, HttpStatusCode.OK);

            //PUT product with id 1 & Qty 1
            OrderItem item002 = new OrderItem() { productid = 1, quantity = 1, unitprice = 2.3 };
            Assert.AreEqual(Orders.Put(item002).StatusCode, HttpStatusCode.OK);

            //GET & assert Orders content for product ID 1
            Assert.AreEqual(Orders.Get(1).quantity, 3);
        }
    }
}
