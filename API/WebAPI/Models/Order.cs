using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class OrderItem
    {
        [Key]
        public ulong productid { get; set; }

        public int quantity { get; set; }
        //public double unitprice { get; set; }
    }
}