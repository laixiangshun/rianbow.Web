using rainbow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.IDAL
{
    /// <summary>
    /// 用户登录接口
    /// </summary>
     public interface IAccountRepository
    {
         SysUser Login(string username, string pwd);
    }
}
