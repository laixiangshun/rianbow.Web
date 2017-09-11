using Microsoft.Practices.Unity;
using rainbow.Common.PageHelp;
using rainbow.Models;
using rainbow.Models.Sys;
using rainbow.Web.Core;
using rianbow.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace rainbow.Web.Controllers
{
    public class SysLogController : BaseController
    {
        [Dependency]
        public ISysLogBLL sysLogBLL { get; set; }
        //
        // GET: /SysLog/
        public ActionResult Index()
        {
            LogHandler.WriteServiceLog(user.TrueName, "进入日志页面", "成功", "主页面", "系统日志");
            return View();
        }

        [HttpPost]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<SysLog> list = sysLogBLL.GetList(ref pager, queryStr);
            var json = new object();
            if (list != null && list.Count >0)
            {
                json = new
                {
                    total = pager.totalRows,
                    rows = (
                        from r in list
                        select new SysLogModel
                        {
                            Id = r.Id,
                            Operator = r.Operator,
                            Message = r.Message,
                            Result = r.Result,
                            Type = r.Type,
                            Module = r.Module,
                            CreateTime = r.CreateTime
                        }
                    ).ToArray()
                };
            }
            else
            {
                json = new
                {
                    total = 0,
                    rows = 0
                };
            }
            LogHandler.WriteServiceLog(user.TrueName, "日志数据查询", "成功", "查询", "系统日志");
            return Json(json, "json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Details(string id)
        {
            SysLog log = sysLogBLL.GetById(id);
            SysLogModel model = new SysLogModel
            {
                Id = log.Id,
                Operator = log.Operator,
                Message = log.Message,
                Result = log.Result,
                Type = log.Type,
                Module = log.Module,
                CreateTime = log.CreateTime
            };
            LogHandler.WriteServiceLog(user.TrueName, "查看日志详情", "成功", "详情", "系统日志");
            return View(model);
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (sysLogBLL.Delete(ref errors,id))
                {
                    LogHandler.WriteServiceLog(user.TrueName, "进行id:"+id+"的日志删除", "成功", "删除", "系统日志");
                    return Json(new { success = true, message = "删除成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    LogHandler.WriteServiceLog(user.TrueName, "进行id:" + id + "的日志删除,errorMessage:"+errors.Error, "失败", "删除", "系统日志");
                    return Json(new { success = false, message = errors.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                LogHandler.WriteServiceLog(user.TrueName, "进行id:" + id + "的日志删除,errorMessage:参数id为空", "失败", "删除", "系统日志");
                return Json(new { success = false, message = "参数出错" }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}