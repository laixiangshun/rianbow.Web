using rainbow.Common.PageHelp;
using rainbow.Models;
using rainbow.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.IDAL
{
    public interface ISysModuleOperateRepository
    {
        List<SysModuleOperateModel> GetList(ref GridPager pager, string mid);
        int Create(SysModuleOperate entity);
        int Delete(string id);
        SysModuleOperate GetById(string id);
        bool IsExist(string id);
    }
}
