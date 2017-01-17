using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using YeaJur.Mapper.Tests.Models;

namespace YeaJur.Mapper.Tests
{
    [TestClass()]
    public class MapperTests
    {
        [TestMethod()]
        public void ToJsonTest()
        {
            var model = GetOrder();

            var json = model.ToJson();
        }

        [TestMethod()]
        public void ToJsonTest1()
        {

            object model = new
            {
                Name = "YeaJur.Mapper",
                Id = Guid.NewGuid(),
                Orders = new[]
                {
                    new
                    {
                        Name = "YeaJur.Mapper",
                        Id = Guid.NewGuid(),
                    },
                    new
                    {
                        Name = "YeaJur.Mapper",
                        Id = Guid.NewGuid(),
                    }
                }
            };

            var json = model.ToJson();
        }

        [TestMethod()]
        public void ToModelTest()
        {
            var model = GetOrder();
            var json = model.ToJson();
            var model2 = json.ToModel<Order>();
        }

        [TestMethod()]
        public void MapTest()
        {
            var model = GetOrder();
            Order model2 = model.Map();
        }

        [TestMethod()]
        public void MapTest1()
        {
            var model = new Seller
            {
                Name = "YeaJur.Mapper",
                Remark = "YeaJur.Mapper.Seller"
            };
            Supplier model2 = model.Map<Seller, Supplier>();
            //验证是否是深拷贝
            model2.Name = "YeaJur.Mapper.Supplier";
            model2.Other = "YeaJur.Mapper.Supplier";
        }

        [TestMethod()]
        public void MapTest2()
        {

            var model = new Seller
            {
                Name = "YeaJur.Mapper.Seller",
                Remark = "YeaJur.Mapper.Seller"
            };
            var dic = new Dictionary<string, string> {{nameof(Seller.Remark), nameof(Supplier.Other)}};
            Supplier model2 = model.Map<Seller, Supplier>(dic);
            //验证是否是深拷贝
            model2.Name = "YeaJur.Mapper.Supplier";
            model2.Other = "YeaJur.Mapper.Supplier";
        }

        [TestMethod()]
        public void CloneBaseTest()
        {

            var model = new Seller
            {
                Name = "YeaJur.Mapper.Seller",
                Remark = "YeaJur.Mapper.Seller"
            };
            var dic = new Dictionary<string, string> { { nameof(Seller.Remark), nameof(Supplier.Other) } };
            Supplier model2 = model.Map<Seller, Supplier>(dic);
            //验证是否是深拷贝
            model2.Name = "YeaJur.Mapper.Supplier";
            model2.Other = "YeaJur.Mapper.Supplier";
        }

        private Order GetOrder()
        {
            var model = new Order
            {
                SellerInfo =
                {
                    Remark = "YeaJur.Mapper",
                    Name = "YeaJur.Mapper"
                }
            };
            model.Products.Add(new Product
            {
                Name = "YeaJur.Mapper",
                Price = (decimal)12.32,
                Count = 32,
                SupplierInfo = new Supplier
                {
                    Name = "YeaJur.Mapper"
                }
            });
            return model;
        }
    }
}