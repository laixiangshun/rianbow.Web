using rainbow.Common.Utils;
using rainbow.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace rainbow.BLL.ExceptionHand
{
    /// <summary>
    /// 异常通用类
    /// </summary>
    public class ExceptionHander
    {
        public static void WriteException(Exception ex)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    SysException model = new SysException
                    {
                        Id = ResultHelper.NewId,
                        HelpLink = ex.HelpLink,
                        Message = ex.Message,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace,
                        TargetSite = ex.TargetSite.ToString(),
                        Data = ex.Data.ToString(),
                        CreateTime = ResultHelper.NowTime
                    };
                    db.SysException.Add(model);
                    db.SaveChanges();
                }
            }catch(Exception ep){
                try
                {
                    string path = "~/exceptionLog.txt";
                    string txtpath = HttpContext.Current.Server.MapPath(path);
                    using (StreamWriter sw = new StreamWriter(txtpath, true, Encoding.Default))
                    {
                        sw.WriteLine((ex.Message + "|" + ex.StackTrace + "|" + ep.Message + "|" + DateTime.Now.ToString()).ToString());
                        sw.Dispose();
                        sw.Close();
                    }
                    return;
                }
                catch
                {
                    return;
                }
               
            }
        }
    }
}
