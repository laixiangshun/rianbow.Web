using rainbow.IDAL;
using rainbow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.DAL
{
    public class AccountRepository : IAccountRepository,IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public SysUser Login(string username, string pwd)
        {
            using (DBContainer db = new DBContainer())
            {
                SysUser user = db.SysUser.SingleOrDefault(a => a.UserName == username && a.Password == pwd);
                return user;
            }
        }
    }
}
