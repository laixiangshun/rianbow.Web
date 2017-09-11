using Microsoft.Practices.Unity;
using rainbow.IDAL;
using rianbow.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.BLL
{
    public class HomeBLL:IHomeBLL
    {
        [Dependency]
        public IHomeRepository homeRepository { get; set; }
        public List<Models.SysModule> GetMenuByPersonId(string personId,string moduleId)
        {
            return homeRepository.GetMenuByPersonId(personId,moduleId);
        }
    }
}
