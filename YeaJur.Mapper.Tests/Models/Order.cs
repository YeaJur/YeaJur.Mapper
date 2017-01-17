using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YeaJur.Mapper.Tests.Models
{
    public class Order
    {
        public List<Product> Products { get; set; }=new List<Product>();

        public Seller SellerInfo { get; set; }=new Seller();
    }
}
