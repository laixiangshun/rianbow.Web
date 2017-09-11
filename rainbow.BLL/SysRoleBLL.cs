using Microsoft.Practices.Unity;
using rainbow.BLL.ExceptionHand;
using rainbow.Common;
using rainbow.Common.PageHelp;
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
    public class SysRoleBLL : ISysRoleBLL
    {
        [Dependency]
        public ISysRoleRepository roleRepository { get; set; }
        [Dependency]
        public ISysModuleRepository moduleRepository { get; set; }
        public List<SysRoleModel> GetList(ref GridPager pager, string queryStr)
        {
            return roleRepository.GetList(ref pager, queryStr);
        }

        public bool Create(ref Common.Utils.ValidationErrors errors, Models.Sys.SysRoleModel model)
        {
            try
            {
                SysRole entity = roleRepository.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new SysRole();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.CreateTime = model.CreateTime;
                entity.CreatePerson = model.CreatePerson;
                if (roleRepository.Create(entity) == 1)
                {
                    //分配给角色
                    moduleRepository.InsertSysRight();
                    moduleRepository.ClearRightOperation();
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
                if (roleRepository.Delete(id) == 1)
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

        public bool Edit(ref Common.Utils.ValidationErrors errors, Models.Sys.SysRoleModel model)
        {
            try
            {
                SysRole entity = roleRepository.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.CreateTime = model.CreateTime;
                entity.CreatePerson = model.CreatePerson;
                if (roleRepository.Edit(entity)==1)
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

        public Models.Sys.SysRoleModel GetById(string id)
        {
            SysRole entity = roleRepository.GetById(id);
            if (entity == null)
                return null;
            else
            {
                SysRoleModel model = new SysRoleModel{
                   Id = entity.Id,
                   Name = entity.Name,
                   Description = entity.Description,
                   CreateTime = entity.CreateTime,
                   CreatePerson = entity.CreatePerson
                };
                return model;
            }
        }

        public bool IsExist(string id)
        {
            if (roleRepository.IsExist(id))
            {
                return true;
            }
            else
                return false;
        }
    }
}
