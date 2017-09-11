using rainbow.Common.PageHelp;
using rainbow.Common.Utils;
using rainbow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rianbow.IBLL
{
     public interface ISysLogBLL
    {
         List<SysLog> GetList(ref GridPager pager, string queryStr);

         SysLog GetById(string id);

         bool Delete(ref ValidationErrors errors,string id);

         bool Create(ref ValidationErrors errors,SysLog entity);
    }
}
