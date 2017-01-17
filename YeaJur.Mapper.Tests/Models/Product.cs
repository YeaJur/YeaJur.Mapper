using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YeaJur.Mapper.Tests.Models
{
    public class Product
    {
        public Guid Id { get; set; }=Guid.NewGuid();

        public string Name { get; set; }

        public int Count { get; set; }

        public DateTime CreateTime { get; set; }=DateTime.Now;

        public decimal Price { get; set; }

        public Supplier SupplierInfo { get; set; }=new Supplier();
    }
}
