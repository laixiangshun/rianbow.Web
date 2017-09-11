using rainbow.Common.LinkHelp;
using rainbow.Common.PageHelp;
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
    public class SysRoleRepository : ISysRoleRepository,IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<SysRoleModel> GetList(ref GridPager pager, string queryStr)
        {
            using(DBContainer db=new DBContainer()){
                 IQueryable<SysRole> querydata = null;
                if (!string.IsNullOrEmpty(queryStr))
                {
                    querydata = db.SysRole.AsQueryable().Where(a => a.Name.Contains(queryStr));
                }
                else
                {
                    querydata = db.SysRole.AsQueryable();
                }
                pager.totalRows = querydata.Count();
                querydata = LinkHelper.SortingAndPaging(querydata, pager.sort, pager.order, pager.page, pager.rows);
                List<SysRoleModel> list = (from r in querydata
                                           select new SysRoleModel
                                           {
                                               Id = r.Id,
                                               Name = r.Name,
                                               Description = r.Description,
                                               CreateTime = r.CreateTime,
                                               CreatePerson = r.CreatePerson,
                                               UserName = ""
                                           }).ToList();
                return list;
            }
        }

        public int Create(SysRole entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.SysRole.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysRole role = db.SysRole.SingleOrDefault(a => a.Id == id);
                if (role != null)
                {
                    db.SysRole.Remove(role);
                }
                return db.SaveChanges();
            }
        }

        public int Edit(Models.SysRole entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.SysRole.Attach(entity);
                db.Entry<SysRole>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public Models.SysRole GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysRole.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysRole role = db.SysRole.SingleOrDefault(a => a.Id == id);
                if (role != null)
                    return true;
                else
                    return false;
            }
        }
    }
}
