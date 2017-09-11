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
     public interface ISysExceptionBLL
    {
         List<SysException> GetList(ref GridPager pager,string queryStr);

         SysException GetById(string id);

         int Create(ref ValidationErrors errors,SysException entity);

         bool Delete(ref ValidationErrors errors,string id);
    }
}
