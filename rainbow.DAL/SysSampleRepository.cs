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
    public class SysSampleRepository : ISysSampleRepository,IDisposable
    {
        public List<SysSampleModel> GetList(int page, int rows, string sort, string order, ref int total)
        {
            using (DBContainer db = new DBContainer())
            {
                //List<SysSampleModel> list = (from r in db.SysSample
                //                             select new SysSampleModel
                //                             {
                //                                 Id = r.Id,
                //                                 Name = r.Name,
                //                                 Age = r.Age,
                //                                 Bir = r.Bir,
                //                                 Photo = r.Photo,
                //                                 Note = r.Note,
                //                                 CreateTime = r.CreateTime
                //                             }).ToList();
                IQueryable<SysSample> querydata = from r in db.SysSample
                                                  select r;
                if (order == "desc")
                {
                    switch (sort)
                    {
                        case "Id":
                            querydata = querydata.OrderByDescending(r => r.Id);
                            break;
                        case "Name":
                            querydata = querydata.OrderByDescending(r => r.Name);
                            break;
                        default:
                            querydata = querydata.OrderByDescending(r => r.CreateTime);
                            break;
                    }
                }
                else
                {
                    switch (sort)
                    {
                        case "Id":
                            querydata = querydata.OrderBy(c => c.Id);
                            break;
                        case "Name":
                            querydata = querydata.OrderBy(c => c.Name);
                            break;
                        default:
                            querydata = querydata.OrderBy(c => c.CreateTime);
                            break;
                    }
                }
                total = querydata.Count();
                if (total > 0)
                {
                    if (page <= 1)
                    {
                        querydata = querydata.Take(rows);
                    }
                    else
                    {
                        querydata = querydata.Skip((page - 1) * rows).Take(rows);
                    }
                }
                List<SysSampleModel> list = (from r in querydata
                                             select new SysSampleModel
                                             {
                                                 Id = r.Id,
                                                 Name = r.Name,
                                                 Age = r.Age,
                                                 Bir = r.Bir,
                                                 Photo = r.Photo,
                                                 Note = r.Note,
                                                 CreateTime = r.CreateTime
                                             }).ToList();
                return list;
            }
        }

        public int Create(SysSample entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.Set<SysSample>().Add(entity);
                return db.SaveChanges();
            }
        }

        public bool Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysSample entity = db.SysSample.SingleOrDefault(a => a.Id == id);
                db.Set<SysSample>().Remove(entity);
                return db.SaveChanges()>0;
            }
        }

        public int Edit(SysSample entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.Set<SysSample>().Attach(entity);
                db.Entry<SysSample>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public SysSample GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysSample.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysSample entity = db.SysSample.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public void Dispose()
        {

        }

        /// <summary>
        /// 排序，分页，条件查询通过Expression和反射精简代码
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public List<SysSampleModel> GetListByQuery(ref GridPager pager,string queryStr)
        {
            using (DBContainer db = new DBContainer()) 
            {
                IQueryable<SysSample> querydata = from r in db.SysSample
                                                  select r;
                //if (pager.order == "desc")
                //{
                //    switch (pager.sort)
                //    {
                //        case "Id":
                //            querydata = querydata.OrderByDescending(c => c.Id);
                //            break;
                //        case "Name":
                //            querydata = querydata.OrderByDescending(c => c.Name);
                //            break;
                //        default:
                //            querydata = querydata.OrderByDescending(c => c.CreateTime);
                //            break;
                //    }
                //}
                //else
                //{
                //    switch (pager.sort)
                //    {
                //        case "Id":
                //            querydata = querydata.OrderBy(c => c.Id);
                //            break;
                //        case "Name":
                //            querydata = querydata.OrderBy(c => c.Name);
                //            break;
                //        default:
                //            querydata = querydata.OrderBy(c => c.CreateTime);
                //            break;
                //    }
                //}
                if (queryStr != null)
                {
                    querydata=querydata.Where(c => c.Name.Contains(queryStr) || c.Note.Contains(queryStr));
                }
                pager.totalRows = querydata.Count();
                querydata=LinkHelper.SortingAndPaging(querydata, pager.sort, pager.order, pager.page, pager.rows);
                //if (pager.totalRows > 0)
                //{
                //    if (pager.page <= 1)
                //    {
                //        querydata = querydata.Take(pager.rows);
                //    }
                //    else
                //    {
                //        querydata = querydata.Skip((pager.page - 1) * pager.rows).Take(pager.rows);
                //    }
                //}
                List<SysSampleModel> list = (from r in querydata
                                             select new SysSampleModel
                                             {
                                                 Id = r.Id,
                                                 Note = r.Note,
                                                 Age = r.Age,
                                                 Bir = r.Bir,
                                                 Photo = r.Photo,
                                                 Name = r.Name,
                                                 CreateTime = r.CreateTime
                                             }).ToList();
                if(list!=null && list.Count>0){
                    return list;
                }else
                    return null;
            }
        }
    }
}
