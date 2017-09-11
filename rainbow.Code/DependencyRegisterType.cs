using Microsoft.Practices.Unity;
using rainbow.BLL;
using rainbow.DAL;
using rainbow.IDAL;
using rianbow.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.Code
{
    /// <summary>
    /// 系统依赖注入
    /// </summary>
    public class DependencyRegisterType
    {
        public static void Container_Sys(ref UnityContainer container)
        {
            container.RegisterType<ISysSampleRepository, SysSampleRepository>();
            container.RegisterType<ISysSampleBLL, SysSampleBLL>();

            container.RegisterType<IHomeRepository, HomeRepository>();
            container.RegisterType<IHomeBLL, HomeBLL>();

            container.RegisterType<ISysLogBLL, SysLogBLL>();
            container.RegisterType<ISysLogRepository, SysLogRepository>();

            container.RegisterType<ISysExceptionBLL, SysExceptionBLL>();
            container.RegisterType<ISysExceptionRepository, SysExceptionRepository>();

            container.RegisterType<IAccountBLL, AccountBLL>();
            container.RegisterType<IAccountRepository, AccountRepository>();

            container.RegisterType<ISysUserBLL, SysUserBLL>();
            container.RegisterType<ISysRightRepository, SysRightRepository>();

            container.RegisterType<ISysModuleRepository, SysModuleRepository>();
            container.RegisterType<ISysModuleBLL, SysModuleBLL>();

            container.RegisterType<ISysModuleOperateBLL, SysModuleOperateBLL>();
            container.RegisterType<ISysModuleOperateRepository, SysModuleOperateRepository>();

            container.RegisterType<ISysRoleBLL, SysRoleBLL>();
            container.RegisterType<ISysRoleRepository, SysRoleRepository>();
        }
    }
}
