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
    public class SysExceptionController : BaseController
    {
        [Dependency]
        public ISysExceptionBLL sysExceptionBLL { get; set; }
        //
        // GET: /SysException/
        public ActionResult Index()
        {
           // Convert.ToInt16("dddd");
            LogHandler.WriteServiceLog(user.TrueName, "进入系统异常页面", "成功", "主页面", "系统异常");
            return View();
        }

        [HttpPost]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<SysException> list = sysExceptionBLL.GetList(ref pager, queryStr);
            var json = new object();
            if (list != null && list.Count > 0)
            {
                json = new
                {
                    total = pager.totalRows,
                    rows = (from r in list
                            select new SysExceptionModel
                            {
                                Id = r.Id,
                                HelpLink = r.HelpLink,
                                Message = r.Message,
                                Source = r.Source,
                                StackTrace = r.StackTrace,
                                TargetSite = r.TargetSite,
                                Data = r.Data,
                                CreateTime = r.CreateTime
                            }).ToArray()
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
            LogHandler.WriteServiceLog(user.TrueName, "系统异常查询", "成功", "查询", "系统异常");
            return Json(json, "json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(string id)
        {
            SysException ex = sysExceptionBLL.GetById(id);
            SysExceptionModel exmodel = new SysExceptionModel
            {
                Id = ex.Id,
                HelpLink = ex.HelpLink,
                Message = ex.Message,
                Source = ex.Source,
                StackTrace = ex.StackTrace,
                TargetSite = ex.TargetSite,
                Data = ex.Data,
                CreateTime = ex.CreateTime
            };
            LogHandler.WriteServiceLog(user.TrueName, "系统异常详情", "成功", "详情", "系统异常");
            return View(exmodel);
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (sysExceptionBLL.Delete(ref errors,id))
                {
                    LogHandler.WriteServiceLog(user.TrueName, "删除id:"+id+"的系统异常", "成功", "删除", "系统异常");
                    return Json(new { success = true, message = "删除成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    LogHandler.WriteServiceLog(user.TrueName, "删除id:" + id + "系统异常,errorMessage:" + errors.Error, "失败", "删除", "系统异常");
                    return Json(new { success = false, message = errors.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                LogHandler.WriteServiceLog(user.TrueName, "删除id:" + id + "系统异常,errorMessage:参数Id为空", "失败", "删除", "系统异常");
                return Json(new { success = false, message = "参数有错" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 全局异常跳转方法
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            BaseException e = new BaseException();
            return View(e);
        }
	}
}