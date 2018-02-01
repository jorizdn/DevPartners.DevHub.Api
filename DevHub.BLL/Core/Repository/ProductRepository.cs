using DevHub.BLL.Core.Interface;
using DevHub.BLL.Methods;
using DevHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevHub.BLL.Core.Repository
{
    public class ProductRepository : IProductInterface
    {
        private readonly QueryMethod _query;

        public ProductRepository(QueryMethod query)
        {
            _query = query;
        }

        public ProductReturnModel CreateUpdate(ProductModel model)
        {
            var product = _query.CreateUpdateProduct(model);

            return product;
        }


    }
}
