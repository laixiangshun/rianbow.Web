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
    public interface ISysRoleRepository
    {
        List<SysRoleModel> GetList(ref GridPager pager, string queryStr);

        int Create(SysRole entity);
        int Delete(string id);
        int Edit(SysRole entity);
        SysRole GetById(string id);
        bool IsExist(string id);
    }
}
