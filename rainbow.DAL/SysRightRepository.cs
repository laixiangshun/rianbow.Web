using rainbow.IDAL;
using rainbow.Models;
using rainbow.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.DAL
{
    public class SysRightRepository : ISysRightRepository,IDisposable
    {
        public List<permModel> GetPermission(string accountid, string controller)
        {
            using (DBContainer db = new DBContainer()) {
                //此方法调用存储过程会报错，找不到存储过程
                //List<permModel> rights = (from r in db.P_Sys_GetRightOperate(accountid, controller)
                //                          select new permModel
                //                          {
                //                              KeyCode = r.KeyCode,
                //                              IsValid = r.IsValid
                //                          }).ToList();

                //原始SQL的方式调用存储过程
                List<permModel> rights = (from r in db.Database.SqlQuery<P_Sys_GetRightOperate_Result>("dbo.P_Sys_GetRightOperate @p0,@p1", accountid, controller)
                                          select new permModel
                                        {
                                            KeyCode = r.KeyCode,
                                            IsValid = r.IsValid
                                        }).ToList();
                return rights;
            }
        }

        public void Dispose()
        {
           // throw new NotImplementedException();
        }


        public int UpdateRight(SysRightOperateModel model)
        {
            //转换
            SysRightOperate rightOperate = new SysRightOperate();
            rightOperate.Id = model.Id;
            rightOperate.RightId = model.RightId;
            rightOperate.KeyCode = model.KeyCode;
            rightOperate.IsValid = model.IsValid;
            //判断rightOperate是否存在，如果存在就更新rightOperate,否则就添加一条
            using (DBContainer db = new DBContainer())
            {
                SysRightOperate right = db.SysRightOperate.Where(a => a.Id == rightOperate.Id).FirstOrDefault();
                if (right != null)
                {
                    right.IsValid = rightOperate.IsValid;
                }
                else
                {
                    db.SysRightOperate.Add(rightOperate);
                }
                if (db.SaveChanges() > 0)
                {
                    //更新角色-模块的有效标志RightFlag
                    var sysRight = (from r in db.SysRight
                                    where r.Id == rightOperate.RightId
                                    select r).First();
                    db.Database.ExecuteSqlCommand("dbo.P_Sys_UpdateSysRightRightFlag @moduleId,@roleId", sysRight.ModuleId, sysRight.RoleId);
                    return 1;
                }
            }
            return 0;
        }
        //按选择的角色及模块加载模块的权限项
        public List<P_Sys_GetRightByRoleAndModule_Result> GetRightByRoleAndModule(string roleId, string moduleId)
        {
            List<P_Sys_GetRightByRoleAndModule_Result> result = null;
            using (DBContainer db = new DBContainer())
            {
                result = db.Database.SqlQuery<P_Sys_GetRightByRoleAndModule_Result>("dbo.P_Sys_GetRightByRoleAndModule @p0,@p1", roleId, moduleId).ToList();
            }
            return result;
        }
    }
}
