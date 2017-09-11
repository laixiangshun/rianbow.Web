using rainbow.IDAL;
using rainbow.Models;
using rainbow.Models.Sys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.DAL
{
    public class SysModuleRepository : ISysModuleRepository,IDisposable
    {
        public List<SysModuleModel> GetList(string parentId)
        {
            using (DBContainer db = new DBContainer())
            {
                IQueryable<SysModule> queryData = db.SysModule.AsQueryable().Where(a=>a.ParentId==parentId && a.Id!="0").OrderBy(a=>a.Sort);

                List<SysModuleModel> list = (from r in queryData
                                             select new SysModuleModel {
                                                 Id = r.Id,
                                                 Name = r.Name,
                                                 EnglishName = r.EnglishName,
                                                 ParentId = r.ParentId,
                                                 Url = r.Url,
                                                 Iconic = r.Iconic,
                                                 Sort = r.Sort,
                                                 Remark = r.Remark,
                                                 Enable = r.Enable,
                                                 CreatePerson = r.CreatePerson,
                                                 CreateTime = r.CreateTime,
                                                 IsLast = r.IsLast
                                             }).ToList();
                return list;
            }
           
        }

        public List<Models.SysModule> GetModuleBySystem(string parentId)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysModule.Where(a => a.ParentId == parentId && a.Id!="0").ToList();
            }
        }

        public int Create(Models.SysModule entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.SysModule.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysModule entity = db.SysModule.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {
                    //删除SysRight表数据
                    var sr = db.SysRight.AsQueryable().Where(a => a.ModuleId == id);
                    if (sr.Count() > 0) { 
                        foreach (var o in sr)
                        {
                            var sro = db.SysRightOperate.AsQueryable().Where(a => a.RightId == o.Id);
                            if (sro.Count() > 0)
                            {
                                foreach (var o2 in sro)
                                {
                                    db.SysRightOperate.Remove(o2);
                                }
                            }
                            db.SysRight.Remove(o);
                        }
                    }
                    var smo = db.SysModuleOperate.AsQueryable().Where(a => a.ModuleId == id);
                    if (smo.Count() > 0)
                    {
                        foreach (var o3 in smo)
                        {
                            db.SysModuleOperate.Remove(o3);
                        }
                    }
                    db.SysModule.Remove(entity);
                }
               return db.SaveChanges();
            }
        }

        public int Edit(Models.SysModule entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.SysModule.Attach(entity);
                db.Entry<SysModule>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public Models.SysModule GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysModule.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysModule entity = db.SysModule.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                    return true;
                else
                    return false;
            }
        }

        public void Dispose()
        {
           // throw new NotImplementedException();
        }


        public void InsertSysRight()
        {
            using (DBContainer db = new DBContainer())
            {
                //db.P_Sys_InsertSysRight();
                db.Database.ExecuteSqlCommand("dbo.P_Sys_InsertSysRight");
            }
        }


        public bool HasNext(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysModule.AsQueryable().Where(a => a.ParentId == id).Count() > 0;
            }
        }


        public void ClearRightOperation()
        {
            using (DBContainer db = new DBContainer())
            {
                //db.P_Sys_ClearUnusedRightOperate();
                db.Database.ExecuteSqlCommand("dbo.P_Sys_ClearUnusedRightOperate");
            }
        }
    }
}
