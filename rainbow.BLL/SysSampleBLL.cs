using Microsoft.Practices.Unity;
using rainbow.BLL.ExceptionHand;
using rainbow.Common.PageHelp;
using rainbow.Common.Utils;
using rainbow.DAL;
using rainbow.IDAL;
using rainbow.Models;
using rainbow.Models.Sys;
using rianbow.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.BLL
{
    public class SysSampleBLL : ISysSampleBLL
    {
        [Dependency]
        public ISysSampleRepository Rep { get; set; }

        public List<SysSampleModel> GetList(int page, int rows, string sort, string order, ref int total)
        {
            List< SysSampleModel> queryData = Rep.GetList(page,rows,sort,order,ref total);
            return queryData;
        }

        public bool Create(ref ValidationErrors errors,SysSampleModel model)
        {
            try
            {
                SysSample entity = Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add("主键重复");
                    return false;
                }
                entity = new SysSample();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.Note = model.Note;
                entity.Age = model.Age;
                entity.Bir = model.Bir;
                entity.Photo = model.Photo;
                entity.CreateTime = model.CreateTime;
                if (Rep.Create(entity) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add("插入失败");
                    return false;
                }
            }
            catch (Exception e)
            {
                errors.Add(e.Message);
                ExceptionHander.WriteException(e);
                return false;
            }
        }

        public bool Delete(ref ValidationErrors errors, string id)
        {
            try
            {
                if (Rep.Delete(id))
                {
                    return true;
                }
                else
                {
                    errors.Add("删除失败");
                    return false;
                }
            }
            catch (Exception e)
            {
                errors.Add(e.Message);
                ExceptionHander.WriteException(e);
                return false;
            }
        }

        public bool Edit(ref ValidationErrors errors,SysSampleModel model)
        {
            try
            {
                SysSample entity = Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add("该条数据不存在");
                    return false;
                }
                entity.Name = model.Name;
                entity.Note = model.Note;
                entity.Age = model.Age;
                entity.Bir = model.Bir;
                entity.Photo = model.Photo;
                if (Rep.Edit(entity) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add("修改失败");
                    return false;
                }
            }
            catch (Exception e)
            {
                errors.Add(e.Message);
                ExceptionHander.WriteException(e);
                return false;
            }
        }

        public bool IsExists(string id)
        {
            if (Rep.IsExist(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SysSampleModel GetById(string id)
        {
            SysSample entity = Rep.GetById(id);
            if (entity != null){
                SysSampleModel model = new SysSampleModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.Age = entity.Age;
                model.Bir = entity.Bir;
                model.Photo = entity.Photo;
                model.Note = entity.Note;
                model.CreateTime = entity.CreateTime;
                return model;
            }
            else
                return null;
        }

        public bool IsExist(string id)
        {
            return Rep.IsExist(id);
        }


        public List<SysSampleModel> GetListByQuery(ref GridPager pager, string queryStr)
        {
            List<SysSampleModel> list = Rep.GetListByQuery(ref pager, queryStr);
            if (list != null && list.Count > 0)
                return list;
            else
                return null;
        }
    }
}
