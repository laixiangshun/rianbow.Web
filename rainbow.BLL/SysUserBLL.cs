using Microsoft.Practices.Unity;
using rainbow.BLL.ExceptionHand;
using rainbow.Common;
using rainbow.Common.Utils;
using rainbow.IDAL;
using rainbow.Models.Sys;
using rianbow.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.BLL
{
    public class SysUserBLL : ISysUserBLL,IDisposable
    {
        [Dependency]
        public ISysRightRepository sysRightRepository { get; set; }
        public List<permModel> GetPermission(string accountid, string controller)
        {
            return sysRightRepository
                .GetPermission(accountid, controller);
        }


        public void Dispose()
        {
           // throw new NotImplementedException();
        }


        public int UpdateRight(ref ValidationErrors errors,SysRightOperateModel model)
        {
            try
            {
                int num=sysRightRepository.UpdateRight(model);
                if ( num== 1)
                {
                    return num;
                }
                else
                {
                    errors.Add("更新权限失败");
                    return 0;
                }
            }
            catch (Exception e)
            {
                errors.Add(e.Message);
                ExceptionHander.WriteException(e);
                return 0;
            }
        }

        public List<Models.P_Sys_GetRightByRoleAndModule_Result> GetRightByRoleAndModule(string roleId, string moduleId)
        {
            return sysRightRepository.GetRightByRoleAndModule(roleId, moduleId);
        }
    }
}
