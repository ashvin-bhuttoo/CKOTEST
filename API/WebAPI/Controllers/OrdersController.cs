using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.SessionState;
using Microsoft.Ajax.Utilities;
using Microsoft.ApplicationInsights;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class OrdersController : ApiController
    {
        //Holding Cart Items in memory using HttpContext.Current.Session, which is able to differentiate between different browsers on single pc
        private List<OrderItem> orderItems
        {
            get {
                if(HttpContext.Current.Session["orderItems"] == null)
                    HttpContext.Current.Session["orderItems"] = new List<OrderItem>();

                return HttpContext.Current.Session["orderItems"] as List<OrderItem>; 
            }
            set{
                HttpContext.Current.Session["orderItems"] = value;
            }
        }
        
        // GET: api/Orders
        /// <summary>
        /// Retrieve all items from Order
        /// </summary>
        public List<OrderItem> Get()
        {
            return orderItems;
        }

        // GET: api/Orders/5
        /// <summary>
        /// Retrieve specific item from Order
        /// </summary>
        /// <param name="id">product id</param>
        public OrderItem Get(ulong id)
        {
            return orderItems.FirstOrDefault(itm => itm.productid == id);
        }

        // POST: api/Orders/5
        /* *
        * 
        * Required Request Header: Content-Type application/json
        * Sample Request Content:
        *   {
        *       "productid": 1,
        *       "quantity": 1,
        *       "unitprice": 1.99
        *   }
        *   
        * */
        /// <summary>
        /// Update specific item in Order using product id provided (overwrites existing item)
        /// </summary>
        /// <param name="id">product id</param>
        public HttpResponseMessage Post(ulong id, [FromBody]OrderItem item)
        {
            if (ModelState.IsValid && item != null && item?.productid != 0)
            {
                if (orderItems.All(c => c.productid != id))
                {
                    orderItems.Add(item);
                }
                else
                {
                    orderItems.Remove(orderItems.FirstOrDefault(c => c.productid == id));
                    orderItems.Add(item);
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/Orders
        /* *
         * 
         * Required Request Header: Content-Type application/json
         * Sample Request Content:
         *   {
	     *       "productid": 1,
	     *       "quantity": 1,
	     *       "unitprice": 1.99
         *   }
         *   
         * */
        /// <summary>
        /// Add an item to Order (increments quantity if existing productid added)
        /// </summary>
        public HttpResponseMessage Put([FromBody]OrderItem item) 
        {
            if (ModelState.IsValid && item != null && item?.productid != 0)
            {
                if (orderItems.All(c => c.productid != item.productid))
                {
                    orderItems.Add(item);
                }
                else
                {
                    int last_qty = orderItems.FirstOrDefault(c => c.productid == item.productid).quantity;
                    orderItems.Remove(orderItems.FirstOrDefault(c => c.productid == item.productid));
                    item.quantity = last_qty + item.quantity;
                    orderItems.Add(item);
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/Orders/5
        /// <summary>
        /// Delete specific item from Order
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns></returns>
        public HttpResponseMessage Delete(ulong id)
        {
            if (orderItems.Any(c => c.productid == id))
            {
                orderItems.Remove(orderItems.FirstOrDefault(c => c.productid == id));
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        // DELETE: api/Orders
        /// <summary>
        /// Clear all items from Order
        /// </summary>
        public HttpResponseMessage Delete()
        {
            orderItems.Clear();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        #region Codes for unit test project only
        private static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://localhost:1947/", "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                new HttpStaticObjectsCollection(), 10, true,
                HttpCookieMode.AutoDetect,
                SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.Instance,
                    null, CallingConventions.Standard,
                    new[] { typeof(HttpSessionStateContainer) },
                    null)
                .Invoke(new object[] { sessionContainer });

            return httpContext;
        }

        public void setFakeHTTPContext()
        {
            HttpContext.Current = FakeHttpContext();
        }
        #endregion

    }
}
