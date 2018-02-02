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
    public class InventoryRepository : DataManager, IInventoryInterface
    {
        private readonly QueryMethod _query;
        private readonly IOptions<AppSettingModel> _options;

        public InventoryRepository(QueryMethod query, IOptions<AppSettingModel> options)
        {
            _query = query;
            _options = options;
        }

        public InventoryReturnModel CreateUpdate(InventoryModel model, string username)
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

        public spInventoryModel GetById(int id)
        {
            using (var con = GetDbConnection(_options.Value.DefaultConnection))
            {
                var result = con.Query<spInventoryModel>("DevHub_AddProductsByID_Get",
                    new
                    {
                        iIntRecId = id
                    },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                return result;
            }
        }

        public IEnumerable<spInventoryModel> Get(DateTime dateFrom, DateTime dateTo)
        {
            var from = new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day).ToString("yyyy-MM-dd");
            var to = new DateTime(dateTo.Year, dateTo.Month, dateTo.Day).ToString("yyyy-MM-dd");

            using (var con = GetDbConnection(_options.Value.DefaultConnection))
            {
                var result = con.Query<spInventoryModel>("DevHub_AddProducts_Get",
                    new
                    {
                        iDtFrom = from,
                        iDtTo = to
                    },
                    commandType: CommandType.StoredProcedure).ToList();

                return result;
            }
        }


    }
}
