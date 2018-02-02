using Dapper;
using DevHub.BLL.Core.Interface;
using DevHub.BLL.Helpers;
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
    public class StaticRepository : DataManager, IStaticInterface
    {
        private readonly IOptions<AppSettingModel> _options;

        public StaticRepository(IOptions<AppSettingModel> options)
        {
            _options = options;
        }

        public IEnumerable<InvProductCategories> GetCategories()
        {
            using (var con = GetDbConnection(_options.Value.DefaultConnection))
            {
                return con.Query<InvProductCategories>("DevHub_ProductCategories_List", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public IEnumerable<InvUnitOfMeasure> GetUnitOfMeasure()
        {
            using (var con = GetDbConnection(_options.Value.DefaultConnection))
            {
                return con.Query<InvUnitOfMeasure>("DevHub_ProductUOM_List", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
        }


    }
}
