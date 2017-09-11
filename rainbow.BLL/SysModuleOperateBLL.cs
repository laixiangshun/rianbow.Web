using Microsoft.Practices.Unity;
using rainbow.BLL.ExceptionHand;
using rainbow.Common;
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
    public class SysModuleOperateBLL : ISysModuleOperateBLL
    {
        [Dependency]
        public ISysModuleOperateRepository moduleOperationRepository { get; set; }

        public List<Models.Sys.SysModuleOperateModel> GetList(ref Common.PageHelp.GridPager pager, string queryStr)
        {
            return moduleOperationRepository.GetList(ref pager, queryStr);
        }

        public bool Create(ref Common.Utils.ValidationErrors errors, Models.Sys.SysModuleOperateModel model)
        {
            try
            {
                SysModuleOperate entity = moduleOperationRepository.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new SysModuleOperate();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.KeyCode = model.KeyCode;
                entity.ModuleId = model.ModuleId;
                entity.IsValid = model.IsValid;
                entity.Sort = model.Sort;
                if (moduleOperationRepository.Create(entity) == 1)
                {
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

        public bool Delete(ref Common.Utils.ValidationErrors errors, string id)
        {
            try
            {
                if (moduleOperationRepository.Delete(id) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add(Suggestion.DeleteFail);
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

        public SysModuleOperateModel GetById(string id)
        {
            if (IsExist(id))
            {
                SysModuleOperate entity = moduleOperationRepository.GetById(id);
                SysModuleOperateModel model = new SysModuleOperateModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.KeyCode = entity.KeyCode;
                model.ModuleId = entity.ModuleId;
                model.IsValid = entity.IsValid;
                model.Sort = entity.Sort;
                return model;
            }
            else
            {
                return null;
            }
        }

        public bool IsExist(string id)
        {
            return moduleOperationRepository.IsExist(id);
        }
    }
}
