using Microsoft.Practices.Unity;
using rainbow.Common.Utils;
using rainbow.DAL;
using rainbow.Models;
using rianbow.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rainbow.Web.Core
{
    public class LogHandler
    {
        //[Dependency]
        private static ISysLogBLL logBLL { get; set; }
        public LogHandler(ISysLogBLL logb)
        {
            logBLL = logb;
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="mes"></param>
        /// <param name="result"></param>
        /// <param name="type"></param>
        /// <param name="module"></param>
        public static void WriteServiceLog(string oper, string mes, string result, string type, string module)
        {
            SysLog entity = new SysLog
            {
                Id = ResultHelper.NewId,
                Operator = oper,
                Message = mes,
                Result = result,
                Type = type,
                Module = module,
                CreateTime = ResultHelper.NowTime
            };
            using (SysLogRepository log = new SysLogRepository())
            {
                log.Create(entity);
            }
           //logBLL.Create(entity);
        }
    }
}