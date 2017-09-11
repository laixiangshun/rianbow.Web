using Microsoft.Practices.Unity;
using rainbow.BLL.ExceptionHand;
using rainbow.Common.PageHelp;
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
    public class SysExceptionBLL : ISysExceptionBLL,IDisposable
    {
        [Dependency]
        public ISysExceptionRepository exceptionRepository { get; set; }

        public List<SysException> GetList(ref GridPager pager,string queryStr)
        {
            return exceptionRepository.GetList(ref pager,queryStr);

        }

        public Models.SysException GetById(string id)
        {
            return exceptionRepository.GetById(id);
        }

        public int Create(ref ValidationErrors errors,Models.SysException entity)
        {
            try
            {
                return exceptionRepository.Create(entity);
            }
            catch (Exception e)
            {
                errors.Add(e.Message);
                ExceptionHander.WriteException(e);
                return 0;
            }
           
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }


        public bool Delete(ref ValidationErrors errors,string id)
        {
            try
            {
                return exceptionRepository.Delete(id);
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
