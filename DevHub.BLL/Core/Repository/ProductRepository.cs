using AutoMapper;
using Dapper;
using DevHub.BLL.Core.Interface;
using DevHub.BLL.Helpers;
using DevHub.BLL.Methods;
using DevHub.DAL.Entities;
using DevHub.DAL.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DevHub.BLL.Core.Repository
{
    public class ProductRepository : DataManager, IProductInterface
    {
        private readonly QueryMethod _query;
        private readonly IOptions<AppSettingModel> _options;
        private readonly IMapper _mapper;

        public ProductRepository(QueryMethod query, IOptions<AppSettingModel> options, IMapper mapper)
        {
            _query = query;
            _options = options;
            _mapper = mapper;
        }

        public ProductReturnModel CreateUpdate(ProductModel model)
        {
            using (var con = GetDbConnection(_options.Value.DefaultConnection))
            {
                var result = con.Query<int?>("DevHub_ProductDesc_Set",
                    new
                    {
                        iIntProdId = model.ProductId,
                        iIntCategoryID = model.CategoryId,
                        iStrProductDescription = model.Description,
                        iStrProductName = model.Name,
                        iDecSRP = model.Price,
                        iIntUom_Id = model.UnitMeasure
                    },
                    commandType: CommandType.StoredProcedure).FirstOrDefault() ?? 0;

                if (result > 0)
                {
                    return new ProductReturnModel() { Product = model, Action = result };
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<spProductModel> Get()
        {
            var result = new List<spProductModel>();
            var products = new List<ProductModel>();
            using (var con = GetDbConnection(_options.Value.DefaultConnection))
            {
                    result = con.Query<spProductModel>("DevHub_ProductDescList_Get",
                    new
                    {
                        @iStrSearch = ""
                    },
                    commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        public spProductModel GetById(int id)
        {
            using (var con = GetDbConnection(_options.Value.DefaultConnection))
            {
                var result = con.Query<spProductModel>("DevHub_ProductDescByID_Get",
                    new
                    {
                        @iIntProductID = id
                    },
                    commandType : CommandType.StoredProcedure).FirstOrDefault();

                return result;
            }
        }

    }
}
