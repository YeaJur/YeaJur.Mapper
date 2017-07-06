using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YeaJur.Mapper.Tests.Models;

namespace YeaJur.Mapper.Tests
{
    [TestClass()]
    public class MapperTests
    {
        [TestMethod()]
        public void YeaJurMapperToJsonTest()
        {
            var model = new List<Order> {GetOrder(), GetOrder(), GetOrder()};
            var json = model.ToJson();
        }

        [TestMethod()]
        public void NewtonsoftJsonToJsonTest()
        {
            var model = new List<Order> {GetOrder(), GetOrder(), GetOrder()};
            var json = JsonConvert.SerializeObject(model);
        }

        [TestMethod()]
        public void NewtonsoftJsonToModelTest()
        {
            var json = GetJson();
            var model = JsonConvert.DeserializeObject<List<Order>>(json);
        }

        [TestMethod()]
        public void YeaJurMapperToModelTest()
        {
            var json = GetJson();
            var model = json.ToModel<List<Order>>();
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

        /// <summary>
        /// 同类型深拷贝
        /// </summary>
        [TestMethod()]
        public void YeaJurMapperSameTypeTest()
        {
            var list = new List<Order> {GetOrder(), GetOrder(), GetOrder()};
            var model = list.Map();
        }

        /// <summary>
        /// 同类型深拷贝
        /// </summary>
        [TestMethod()]
        public void AutoMapperSameTypeTest()
        {
            var list = new List<Order> {GetOrder(), GetOrder(), GetOrder()};

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<List<Order>, List<Order>>();
            });
            var model = AutoMapper.Mapper.Map<List<Order>>(list);
        }

        /// <summary>
        /// 不同类型拷贝
        /// </summary>
        [TestMethod()]
        public void YeaJurMapperDifferentTypeTest()
        {
            var model = new Seller
            {
                Name = "YeaJur.Mapper",
                Remark = "YeaJur.Mapper.Seller"
            };
            Supplier model2 = model.Map<Supplier>();
            string json = model2.ToJson();
            // Supplier model3 = model.Map<Seller, Supplier>();
            //验证是否是深拷贝
            //model2.Name = "YeaJur.Mapper.Supplier";
            //model2.Other = "YeaJur.Mapper.Supplier";
        }

        /// <summary>
        /// 不同类型拷贝
        /// </summary>
        [TestMethod()]
        public void AutoMapperDifferentTypeTest()
        {
            var model = new Seller
            {
                Name = "YeaJur.Mapper",
                Remark = "YeaJur.Mapper.Seller"
            };
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Seller, Supplier>();
            });
            Supplier model2 = AutoMapper.Mapper.Map<Seller, Supplier>(model);
            //验证是否是深拷贝
            //model2.Name = "YeaJur.Mapper.Supplier";
            //model2.Other = "YeaJur.Mapper.Supplier";
        }

        [TestMethod()]
        public void YeaJurMapperCustomFieldsTest()
        {

            var model = new Seller
            {
                Name = "YeaJur.Mapper.Seller",
                Remark = "YeaJur.Mapper.Seller"
            };
            var dic = new Dictionary<string, string>
            {
                {nameof(Seller.Remark), nameof(Supplier.Other)}
            };
            // Supplier model2 = model.Map< Supplier>(dic);
            Supplier model2 = model.Map<Seller, Supplier>(dic);
            ////验证是否是深拷贝
            //model2.Name = "YeaJur.Mapper.Supplier";
            //model2.Other = "YeaJur.Mapper.Supplier";
        }

        [TestMethod()]
        public void AutoMapperCustomFieldsTest()
        {

            var model = new Seller
            {
                Name = "YeaJur.Mapper.Seller",
                Remark = "YeaJur.Mapper.Seller"
            };
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Seller, Supplier>();
                cfg.ReplaceMemberName(nameof(Seller.Remark), nameof(Supplier.Other));
            });
            Supplier model2 = AutoMapper.Mapper.Map<Seller, Supplier>(model);
            //验证是否是深拷贝
            //model2.Name = "YeaJur.Mapper.Supplier";
            //model2.Other = "YeaJur.Mapper.Supplier";
        }

        [TestMethod()]
        public void CloneBaseTest()
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
        public void DateTimeTest()
        {
            var model = new { Name = "YeaJur", Date1 = DateTime.Parse("1918-09-09") ,Date2=DateTime.Parse("2017-07-06")};
            var json = model.ToJson("yyyy-MM-dd");
            var str = json;
        }

        private Order GetOrder()
        {
            var model = new Order
            {
                SellerInfo =
                {
                    Remark = "YeaJur.Mapper",
                    Name = "YeaJur.Mapper"
                },
                Products = new List<Product>()
                {
                    new Product
                    {
                        Name = "YeaJur.Mapper",
                        Price = (decimal) 12.32,
                        Count = 32,
                        SupplierInfo = new Supplier
                        {
                            Name = "YeaJur.Mapper"
                        }
                    },
                    new Product
                    {
                        Name = "YeaJur.Mapper",
                        Price = (decimal) 12.32,
                        Count = 32,
                        SupplierInfo = new Supplier
                        {
                            Name = "YeaJur.Mapper"
                        }
                    },
                    new Product
                    {
                        Name = "YeaJur.Mapper",
                        Price = (decimal) 12.32,
                        Count = 32,
                        SupplierInfo = new Supplier
                        {
                            Name = "YeaJur.Mapper"
                        }
                    }
                }
            };
            return model;
        }

        private string GetJson()
        {
            var json =
                "[{\"Products\":[{\"Id\":\"4309df0e-5fe5-4496-9436-a5e7dbdeb83d\",\"Name\":\"YeaJur.Mapper\",\"Count\":32,\"CreateTime\":\"2017-01-19\",\"Price\":12.32,\"SupplierInfo\":{\"Id\":\"3bf22a1e-c328-451c-b93e-2c08fbe21da3\",\"Name\":\"YeaJur.Mapper\",\"Other\":null}},{\"Id\":\"a67767b5-5420-4cbf-8464-7a1ac2f3a2d2\",\"Name\":\"YeaJur.Mapper\",\"Count\":32,\"CreateTime\":\"2017-01-19 \",\"Price\":12.32,\"SupplierInfo\":{\"Id\":\"665b2694-0fa5-4105-9f67-e0896cede32d\",\"Name\":\"YeaJur.Mapper\",\"Other\":null}},{\"Id\":\"6ab85148-6269-466b-a09d-5356a58b6567\",\"Name\":\"YeaJur.Mapper\",\"Count\":32,\"CreateTime\":\"2017-01-19 \",\"Price\":12.32,\"SupplierInfo\":{\"Id\":\"a9b29491-0393-4a80-874b-110908718b0c\",\"Name\":\"YeaJur.Mapper\",\"Other\":null}}],\"SellerInfo\":{\"Id\":\"fa298f36-6dab-4448-8ac5-451e103ea830\",\"Name\":\"YeaJur.Mapper\",\"Remark\":\"YeaJur.Mapper\"}},{\"Products\":[{\"Id\":\"d28d7a2f-7cea-4052-bd0f-0e556e940c32\",\"Name\":\"YeaJur.Mapper\",\"Count\":32,\"CreateTime\":\"2017-01-19 \",\"Price\":12.32,\"SupplierInfo\":{\"Id\":\"9bd979f2-d9cd-4dfa-ab04-14d2d9436507\",\"Name\":\"YeaJur.Mapper\",\"Other\":null}},{\"Id\":\"8b0be369-8236-415f-98b6-1531e42b764a\",\"Name\":\"YeaJur.Mapper\",\"Count\":32,\"CreateTime\":\"2017-01-19 \",\"Price\":12.32,\"SupplierInfo\":{\"Id\":\"dd0433b9-c6f9-47be-9b79-725b0b6ee17c\",\"Name\":\"YeaJur.Mapper\",\"Other\":null}},{\"Id\":\"d9a50d84-5430-46e2-82b8-9455fe40a564\",\"Name\":\"YeaJur.Mapper\",\"Count\":32,\"CreateTime\":\"2017-01-19 \",\"Price\":12.32,\"SupplierInfo\":{\"Id\":\"6323c131-9f5d-4a32-8c75-d09fdc4e14a0\",\"Name\":\"YeaJur.Mapper\",\"Other\":null}}],\"SellerInfo\":{\"Id\":\"04c20a66-7863-4dc1-b5e8-44d84d4129bb\",\"Name\":\"YeaJur.Mapper\",\"Remark\":\"YeaJur.Mapper\"}},{\"Products\":[{\"Id\":\"b5a11c81-7bea-4f82-9507-fd4ad67edaee\",\"Name\":\"YeaJur.Mapper\",\"Count\":32,\"CreateTime\":\"2017-01-19 \",\"Price\":12.32,\"SupplierInfo\":{\"Id\":\"f428379e-00ef-4e9f-9ee8-13fc8e4d1bd4\",\"Name\":\"YeaJur.Mapper\",\"Other\":null}},{\"Id\":\"5ec5fe0b-f45d-4463-8026-36e2207116fe\",\"Name\":\"YeaJur.Mapper\",\"Count\":32,\"CreateTime\":\"2017-01-19 \",\"Price\":12.32,\"SupplierInfo\":{\"Id\":\"0f9fce73-09d7-4dad-a64a-0d2bd0dc870f\",\"Name\":\"YeaJur.Mapper\",\"Other\":null}},{\"Id\":\"b00d8a8a-e898-4528-9970-1ea6a4528327\",\"Name\":\"YeaJur.Mapper\",\"Count\":32,\"CreateTime\":\"2017-01-19 \",\"Price\":12.32,\"SupplierInfo\":{\"Id\":\"778f446f-1725-499e-bbb7-fba25c205836\",\"Name\":\"YeaJur.Mapper\",\"Other\":null}}],\"SellerInfo\":{\"Id\":\"b7181dfa-26eb-4860-b9e3-5a1386ade097\",\"Name\":\"YeaJur.Mapper\",\"Remark\":\"YeaJur.Mapper\"}}]";
            return json;
        }
    }
}