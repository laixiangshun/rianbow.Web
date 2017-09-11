using rainbow.Common.LinkHelp;
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
    public class SysModuleOperateRepository : ISysModuleOperateRepository,IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<SysModuleOperateModel> GetList(ref Common.PageHelp.GridPager pager, string mid)
        {
            using (DBContainer db = new DBContainer())
            {
                IQueryable<SysModuleOperate> list = db.SysModuleOperate.AsQueryable().Where(a=>a.ModuleId==mid);
                pager.totalRows = list.Count();
                list = LinkHelper.SortingAndPaging(list, pager.sort, pager.order, pager.page, pager.rows);
                List<SysModuleOperateModel> query = (from r in list
                                                     select new SysModuleOperateModel
                                                     {
                                                         Id = r.Id,
                                                         Name = r.Name,
                                                         KeyCode = r.KeyCode,
                                                         ModuleId = r.ModuleId,
                                                         IsValid = r.IsValid,
                                                         Sort = r.Sort
                                                     }).ToList();
                return query;
            }
        }

        public int Create(SysModuleOperate entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.SysModuleOperate.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysModuleOperate entity = db.SysModuleOperate.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {
                    db.SysModuleOperate.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public Models.SysModuleOperate GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysModuleOperate.SingleOrDefault(s => s.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysModuleOperate entity = db.SysModuleOperate.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                    return true;
                else
                    return false;
            }
        }
    }
}
