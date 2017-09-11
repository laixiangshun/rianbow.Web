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
    public interface ISysUserBLL
    {
        List<permModel> GetPermission(string accountid, string controller);

        //更新SysRightOperate
        int UpdateRight(ref ValidationErrors errors,SysRightOperateModel model);

        //按选择的角色及模块加载模块的权限项
        List<P_Sys_GetRightByRoleAndModule_Result> GetRightByRoleAndModule(string roleId, string moduleId);
    }
}
