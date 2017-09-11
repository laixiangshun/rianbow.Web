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
    public class SysExceptionRepository : ISysExceptionRepository,IDisposable
    {
        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        public int Create(SysException entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.SysException.Add(entity);
                return db.SaveChanges();
            }
        }

        public List<SysException> GetList(ref GridPager pager, string queryStr)
        {
            using (DBContainer db = new DBContainer())
            {
                List<SysException> list = null;
                IQueryable<SysException> q = db.SysException.AsQueryable();
                if (!string.IsNullOrEmpty(queryStr))
                {
                    q = q.Where(c => c.Message.Contains(queryStr));
                    pager.totalRows = q.Count();
                }
                else
                {
                    pager.totalRows = q.Count();
                }
                if (pager.order == "desc")
                {
                    q = q.OrderByDescending(c => c.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows);
                }
                else
                {
                    q = q.OrderBy(c => c.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows);
                }
                list = q.ToList();
                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }

        public SysException GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysException.SingleOrDefault(s => s.Id == id);
            }
        }


        public bool Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysException ex = db.SysException.SingleOrDefault(a => a.Id == id);
                db.Set<SysException>().Remove(ex);
                return db.SaveChanges() > 0;
            }
        }
    }
}
