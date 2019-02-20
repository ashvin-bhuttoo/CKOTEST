using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary
{

    public class Orders
    {
        static HttpClient client = new HttpClient() {BaseAddress = new Uri("http://localhost:1947")};

        //default ctor
        public Orders() 
        {}

        //ctor with baseURL override
        public Orders(string baseUrl)
        {
            client.BaseAddress = new Uri(baseUrl);
        }

        public async Task<List<OrderItem>> GetAll() //GET
        {
            HttpResponseMessage response = await client.GetAsync($"api/Orders");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<List<OrderItem>>();
        }

        public async Task<OrderItem> GetOrderItem(int productid) //GET
        {
            HttpResponseMessage response = await client.GetAsync($"api/Orders/{productid}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<OrderItem>();
        }

        public async Task<bool> Add(OrderItem orderItem) //PUT
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Orders", orderItem);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(ulong productid, OrderItem orderItem) //POST
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"api/Orders/{productid}", orderItem);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Remove(int productid) //DELETE
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/Orders/{productid}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Clear() //DELETE
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/Orders");
            return response.IsSuccessStatusCode;
        }

    }


    public class OrderItem
    {
        public ulong productid { get; set; }

        public int quantity { get; set; }
        //public double unitprice { get; set; }
    }
}
