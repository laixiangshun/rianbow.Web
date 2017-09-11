using Microsoft.Practices.Unity;
using rainbow.BLL.ExceptionHand;
using rainbow.Common;
using rainbow.Common.Utils;
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
    public class SysModuleBLL : ISysModuleBLL
    {
        [Dependency]
        public ISysModuleRepository moduleRepository { get; set; }
        public List<SysModuleModel> GetList(string parentId)
        {
            return moduleRepository.GetList(parentId);
        }

        public List<SysModule> GetModuleBySystem(string parentId)
        {
            return moduleRepository.GetModuleBySystem(parentId);
        }

        public bool Create(ref Common.Utils.ValidationErrors errors, SysModuleModel model)
        {
            try
            {
                SysModule entity = moduleRepository.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new SysModule();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.EnglishName = model.EnglishName;
                entity.ParentId = model.ParentId;
                entity.Url = model.Url;
                entity.Iconic = model.Iconic;
                entity.Sort = model.Sort;
                entity.Remark = model.Remark;
                entity.Enable = model.Enable;
                entity.CreatePerson = model.CreatePerson;
                entity.CreateTime = model.CreateTime;
                entity.IsLast = model.IsLast;
                if (moduleRepository.Create(entity) == 1)
                {
                    moduleRepository.InsertSysRight();//分配给角色-存储过程
                    return true;
                }
                else
                {
                    errors.Add(Suggestion.InsertFail);
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
                //检查是否有下级
                if (moduleRepository.HasNext(id))
                {
                    errors.Add("有下属关联，请先删除下属");
                    return false;
                }
               int num= moduleRepository.Delete(id);
                if (num > 0)
                {
                    //清理无用的项
                    moduleRepository.ClearRightOperation();
                    return true;
                }
                else
                {
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

        public bool Edit(ref Common.Utils.ValidationErrors errors, Models.Sys.SysModuleModel model)
        {
            try
            {
                SysModule entity = moduleRepository.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.Name = model.Name;
                entity.EnglishName = model.EnglishName;
                entity.ParentId = model.ParentId;
                entity.Url = model.Url;
                entity.Iconic = model.Iconic;
                entity.Sort = model.Sort;
                entity.Remark = model.Remark;
                entity.Enable = model.Enable;
                entity.IsLast = model.IsLast;
                if (moduleRepository.Edit(entity) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add(Suggestion.EditFail);
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

        public Models.Sys.SysModuleModel GetById(string id)
        {
            if (IsExist(id))
            {
                SysModule entity = moduleRepository.GetById(id);
                SysModuleModel model = new SysModuleModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.EnglishName = entity.EnglishName;
                model.ParentId = entity.ParentId;
                model.Url = entity.Url;
                model.Iconic = entity.Iconic;
                model.Sort = entity.Sort;
                model.Remark = entity.Remark;
                model.Enable = entity.Enable;
                model.CreatePerson = entity.CreatePerson;
                model.CreateTime = entity.CreateTime;
                model.IsLast = entity.IsLast;
                return model;
            }
            else
            {
                return null;
            }
        }

        public bool IsExist(string id)
        {
            return moduleRepository.IsExist(id);
        }
    }
}
