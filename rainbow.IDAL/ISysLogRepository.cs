using rainbow.Common.PageHelp;
using rainbow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.IDAL
{
     public interface ISysLogRepository
    {
        int Create(SysLog entity);
        bool Delete(string[] deleteCollection);
        List<SysLog> GetList(ref GridPager pager,string queryStr);
        SysLog GetById(string id);
    }
}
