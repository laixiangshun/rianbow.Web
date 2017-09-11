using rainbow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rianbow.IBLL
{
    public interface IHomeBLL
    {
        List<SysModule> GetMenuByPersonId(string personId,string moduleId);
    }
}
