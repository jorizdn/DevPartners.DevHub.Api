using DevHub.DAL.Entities;
using DevHub.DAL.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using DevHub.BLL.Helpers;
using static DevHub.BLL.Helpers.HttpResponses;

namespace DevHub.BLL.Methods
{
    public class QueryMethod : DataManager
    {
        private readonly DevHubContext _context;
        private readonly IOptions<AppSettingModel> _options;

        public QueryMethod(DevHubContext context, IOptions<AppSettingModel> options)
        {
            _context = context;
            _options = options;
        }

        public ClientMaster GetClientByEmail(string email)
        {
            try
            {
                return _context.ClientMaster.Where(a => a.Email == email).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public InventoryReturnModel CreateUpdateInventory(InventoryModel model,  string username)
        {
            using (var con = GetDbConnection(_options.Value.DefaultConnection))
            {
                var result = con.Query<int?>("DevHub_AddProducts_Set",
                            new
                            {
                                iIntRecId = model.InventoryId,
                                iIntProduct = model.ProductId,
                                iDecQuantity = model.Quantity,
                                UserName = username
                            },
                            commandType: CommandType.StoredProcedure).FirstOrDefault() ?? 0;


                if (result > 0)
                {
                    model.Username = username;
                    return new InventoryReturnModel() { Inventory = model, Action = result };
                }
                else
                {
                    return null;
                }
                
            }
        }

        public ProductReturnModel CreateUpdateProduct(ProductModel model)
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


    }
}
