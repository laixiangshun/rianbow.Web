using Microsoft.Practices.Unity;
using rainbow.BLL.ExceptionHand;
using rainbow.Common.Utils;
using rainbow.IDAL;
using rainbow.Models;
using rianbow.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.BLL
{
    public class SysLogBLL : ISysLogBLL
    {
        [Dependency]
        public ISysLogRepository logRepository { get; set; }
        public List<SysLog> GetList(ref Common.PageHelp.GridPager pager, string queryStr)
        {
            List<SysLog> list = logRepository.GetList(ref pager, queryStr);
            if (list != null && list.Count > 0)
                return list;
            else
                return null;
        }

        public Models.SysLog GetById(string id)
        {
            return logRepository.GetById(id);
        }


        public bool Delete(ref ValidationErrors errors,string id)
        {
            try
            {
                return logRepository.Delete(new string[] { id });
            }
            catch (Exception e)
            {
                errors.Add(e.Message);
                ExceptionHander.WriteException(e);
                return false;
            }
           
        }


        public bool Create(ref ValidationErrors errors,SysLog entity)
        {
            try
            {
                if (logRepository.Create(entity) == 1)
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
    }
}
