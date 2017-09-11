using rainbow.Common.PageHelp;
using rainbow.Common.Utils;
using rainbow.Models;
using rainbow.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rianbow.IBLL
{
     public interface ISysSampleBLL
    {
         /// <summary>
         /// 条件查询和分页
         /// </summary>
         /// <param name="page"></param>
         /// <param name="rows"></param>
         /// <param name="sort"></param>
         /// <param name="order"></param>
         /// <param name="total"></param>
         /// <returns></returns>
         List<SysSampleModel> GetList(int page,int rows,string sort,string order,ref int total);

         bool Create(ref ValidationErrors errors , SysSampleModel entity);

         bool Delete(ref ValidationErrors errors,string id);

         bool Edit(ref ValidationErrors errors, SysSampleModel entity);

         bool IsExists(string id);


         SysSampleModel GetById(string id);

         bool IsExist(string id);
         /// <summary>
         /// 封装的条件查询
         /// </summary>
         /// <param name="pager"></param>
         /// <returns></returns>
         List<SysSampleModel> GetListByQuery(ref GridPager pager, string queryStr);
    }
}
