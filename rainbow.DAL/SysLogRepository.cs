using rainbow.Common.PageHelp;
using rainbow.IDAL;
using rainbow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.DAL
{
    public class SysLogRepository : ISysLogRepository,IDisposable
    {
        public int Create(Models.SysLog entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.SysLog.Add(entity);
                return db.SaveChanges();
            }
        }

        public bool Delete(string[] deleteCollection)
        {
            using (DBContainer db = new DBContainer())
            {
                IQueryable<SysLog> collection = from s in db.SysLog
                                                where deleteCollection.Contains(s.Id)
                                                select s;
                foreach (var item in collection)
                {
                    db.Set<SysLog>().Remove(item);
                }
                return db.SaveChanges()>0;
            }
        }

        public List<Models.SysLog> GetList(ref GridPager pager,string queryStr)
        {
            using (DBContainer db = new DBContainer())
            {
                List<SysLog> list = null;
                IQueryable<SysLog> q = db.SysLog.AsQueryable();
                if (!string.IsNullOrWhiteSpace(queryStr))
                {
                    q = q.Where(a => a.Message.Contains(queryStr) || a.Module.Contains(queryStr));
                    pager.totalRows = q.Count();
                }
                else
                {
                    pager.totalRows = q.Count();
                }
                if (pager.order == "desc")
                {
                    q = q.OrderByDescending(a => a.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows);
                }
                else
                {
                    q = q.OrderBy(a => a.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows);
                }
                list = q.ToList();
                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }

        public Models.SysLog GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysLog.SingleOrDefault(a => a.Id == id);
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
