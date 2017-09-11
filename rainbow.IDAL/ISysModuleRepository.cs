using rainbow.Models;
using rainbow.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.IDAL
{
     public interface ISysModuleRepository
    {
         List<SysModuleModel> GetList(string parentId);
         List<SysModule> GetModuleBySystem(string parentId);
         int Create(SysModule entity);
         int Delete(string id);
         int Edit(SysModule entity);
         SysModule GetById(string id);
         bool IsExist(string id);

         void InsertSysRight();

         bool HasNext(string id);

         void ClearRightOperation();
    }
}
