using rainbow.IDAL;
using rainbow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.DAL
{
    public class HomeRepository : IHomeRepository,IDisposable
    {
        public List<Models.SysModule> GetMenuByPersonId(string personId,string moduleId)
        {
            using (DBContainer db = new DBContainer()) 
            {
                //原来的方法
                //var menus = 
                //    (
                //         from m in db.SysModule
                //         where m.ParentId == moduleId 
                //         where m.Id != "0"
                //         select m
                //     ).OrderBy(a => a.Sort).ToList();

                //增加了模块的权限后
                //var menus = (
                //        from m in db.SysModule
                //        join rl in db.SysRight on m.Id equals rl.ModuleId
                //        join sr in db.SysRole on rl.RoleId equals sr.Id
                //        join srsu in db.SysRoleSysUser on sr.Id equals srsu.SysRoleId
                //        join u in db.SysUser on srsu.SysUserId equals u.Id
                //        where m.ParentId == moduleId
                //        where m.Id != "0"
                //        where rl.Rightflag == true
                //        where u.Id == personId
                //        select m
                //    ).Distinct().OrderBy(a => a.Sort).ToList();

                //试试另一种方式：
                var menus =
               (
                   from m in db.SysModule
                   join rl in db.SysRight on m.Id equals rl.ModuleId
                   join r in
                       (from r in db.SysRole
                        from u in db.SysUser
                        where u.Id == personId
                        select r) on rl.RoleId equals r.Id
                   where rl.Rightflag == true
                   where m.ParentId == moduleId
                   where m.Id != "0"
                   select m
                         ).Distinct().OrderBy(a => a.Sort).ToList();
                return menus;
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
