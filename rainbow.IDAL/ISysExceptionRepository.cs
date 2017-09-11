using rainbow.Common.PageHelp;
using rainbow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.IDAL
{
    public interface ISysExceptionRepository
    {
        int Create(SysException entity);
        List<SysException> GetList(ref GridPager pager, string queryStr);

        SysException GetById(string id);

        bool Delete(string id);
    }
}
