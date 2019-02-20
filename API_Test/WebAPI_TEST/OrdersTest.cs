using System;
using System.Linq;
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
        //Add Order & Fetch single OrderItem 
        public void WebApiTest001()
        {
            var Orders = new WebAPI.Controllers.OrdersController();
            Orders.setFakeHTTPContext(); //a fake http context is set up for test purposes
            Assert.IsTrue(Orders.Delete().IsSuccessStatusCode);

            //PUT product with id 1 & Qty 2
            OrderItem item001 = new OrderItem() {productid = 1, quantity = 2/*, unitprice = 2.3*/};
            Assert.AreEqual(Orders.Put(item001).StatusCode, HttpStatusCode.OK);

            //GET & assert Orders content for product ID 1
            Assert.AreEqual(Orders.Get(1).quantity, 2);
        }

        [TestMethod]
        //Add Order more than once, fetch List<OrderItem> to test Qty increase
        public void WebApiTest002()
        {
            var Orders = new WebAPI.Controllers.OrdersController();
            Orders.setFakeHTTPContext(); //a fake http context is set up for test purposes
            Assert.IsTrue(Orders.Delete().IsSuccessStatusCode);

            //PUT product with id 1 & Qty 2
            OrderItem item001 = new OrderItem() { productid = 1, quantity = 2/*, unitprice = 2.3*/ };
            Assert.AreEqual(Orders.Put(item001).StatusCode, HttpStatusCode.OK);

            //PUT product with id 1 & Qty 3
            OrderItem item002 = new OrderItem() { productid = 1, quantity = 3/*, unitprice = 2.3*/ };
            Assert.AreEqual(Orders.Put(item002).StatusCode, HttpStatusCode.OK);

            //GET & assert Orders content for product ID 1
            Assert.AreEqual(Orders.Get().FirstOrDefault(itm => itm.productid == 1).quantity, 5);
        }

        [TestMethod]
        //Add Order more than once to test Qty increase & Overwrite with POST
        public void WebApiTest003() 
        {
            var Orders = new WebAPI.Controllers.OrdersController();
            Orders.setFakeHTTPContext(); //a fake http context is set up for test purposes
            Assert.IsTrue(Orders.Delete().IsSuccessStatusCode);

            //PUT product with id 1 & Qty 2
            OrderItem item001 = new OrderItem() { productid = 1, quantity = 2/*, unitprice = 2.3*/ };
            Assert.AreEqual(Orders.Put(item001).StatusCode, HttpStatusCode.OK);

            //PUT product with id 1 & Qty 3
            OrderItem item002 = new OrderItem() { productid = 1, quantity = 3/*, unitprice = 2.3*/ };
            Assert.AreEqual(Orders.Put(item002).StatusCode, HttpStatusCode.OK);

            //POST product with id 1 & Qty 1
            OrderItem item003 = new OrderItem() { productid = 1, quantity = 1/*, unitprice = 2.3*/ };
            Assert.AreEqual(Orders.Post(1, item003).StatusCode, HttpStatusCode.OK);

            //GET & assert Orders content for product ID 1
            Assert.AreEqual(Orders.Get(1).quantity, 1);
        }

        [TestMethod]
        //Add multiple Orders with different product id, fetch multiple, test POST() and PUT() when using multiple OrderItems
        public void WebApiTest004()
        {
            var Orders = new WebAPI.Controllers.OrdersController();
            Orders.setFakeHTTPContext(); //a fake http context is set up for test purposes
            Assert.IsTrue(Orders.Delete().IsSuccessStatusCode);
            
            Assert.AreEqual(Orders.Put(new OrderItem() { productid = 1, quantity = 2/*, unitprice = 2.3*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Put(new OrderItem() { productid = 9, quantity = 1/*, unitprice = 1.99*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Put(new OrderItem() { productid = 55, quantity = 4/*, unitprice = 0.22*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Post(1, new OrderItem() { productid = 1, quantity = 1/*, unitprice = 2.33*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Put(new OrderItem() { productid = 55, quantity = 1/*, unitprice = 0.22*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Get().Count, 3);
            Assert.IsTrue(Orders.Get().Any(itm => itm.productid == 1 && itm.quantity == 1));
            Assert.IsTrue(Orders.Get().Any(itm => itm.productid == 9 && itm.quantity == 1));
            Assert.IsTrue(Orders.Get().Any(itm => itm.productid == 55 && itm.quantity == 5));
        }

        [TestMethod]
        //Test Remove Single Item From Order
        public void WebApiTest005()
        {
            var Orders = new WebAPI.Controllers.OrdersController();
            Orders.setFakeHTTPContext(); //a fake http context is set up for test purposes
            Assert.IsTrue(Orders.Delete().IsSuccessStatusCode);

            Assert.AreEqual(Orders.Put(new OrderItem() { productid = 1, quantity = 2/*, unitprice = 2.3*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Put(new OrderItem() { productid = 9, quantity = 1/*, unitprice = 1.99*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Put(new OrderItem() { productid = 55, quantity = 4/*, unitprice = 0.22*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Post(1, new OrderItem() { productid = 1, quantity = 1/*, unitprice = 2.33*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Put(new OrderItem() { productid = 55, quantity = 1/*, unitprice = 0.22*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Get().Count, 3);
            Assert.AreEqual(Orders.Delete(9).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Get().Count, 2);
            Assert.IsFalse(Orders.Get().Exists(itm => itm.productid == 9));
        }


        [TestMethod]
        //Test Clear all Orders
        public void WebApiTest006()
        {
            var Orders = new WebAPI.Controllers.OrdersController();
            Orders.setFakeHTTPContext(); //a fake http context is set up for test purposes
            Assert.IsTrue(Orders.Delete().IsSuccessStatusCode);

            Assert.AreEqual(Orders.Put(new OrderItem() { productid = 1, quantity = 2/*, unitprice = 2.3*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Put(new OrderItem() { productid = 9, quantity = 1/*, unitprice = 1.99*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Put(new OrderItem() { productid = 55, quantity = 4/*, unitprice = 0.22*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Post(1, new OrderItem() { productid = 1, quantity = 1/*, unitprice = 2.33*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Put(new OrderItem() { productid = 55, quantity = 1/*, unitprice = 0.22*/ }).StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Get().Count, 3);
            Assert.AreEqual(Orders.Delete().StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(Orders.Get().Count, 0);
        }
    }
}
