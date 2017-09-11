using Microsoft.Practices.Unity;
using rainbow.Models;
using rainbow.Models.Sys;
using rainbow.Web.Core;
using rianbow.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rainbow.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Dependency]
        public IHomeBLL homeBLL { get; set; }
        public ActionResult Index()
        {
            //AccountModel account = new AccountModel();
            //account.Id = "admin";
            //account.TrueName = "admin";
            //Session["Account"] = account;
            LogHandler.WriteServiceLog(this.GetUserName(), "进入主页面", "成功", "主页面", "主页面");
            AccountModel account = this.user;
            if (account == null)
            {
                Response.Redirect("/Account/Index");
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetTree(string id)
        {
            if (Session["Account"] != null)
            {
                AccountModel account = (AccountModel)Session["Account"];
                List<SysModule> menus = homeBLL.GetMenuByPersonId(account.Id,id);
                var jsonData = (
                        from m in menus
                        select new
                        {
                            id = m.Id,
                            text = m.Name,
                            value = m.Url,
                            showcheck = false,
                            complete = false,
                            isexpand = false,
                            checkstate = false,
                            hasChildren = m.IsLast ? false : true,
                            Icon = m.Iconic
                        }
                    ).ToArray();
                LogHandler.WriteServiceLog(this.GetUserName(), "显示左侧菜单栏信息", "成功", "获取树形菜单信息", "主页面");
                return Json(jsonData, JsonRequestBehavior.AllowGet);

            }
            else
            {
                LogHandler.WriteServiceLog(this.GetUserName(), "显示左侧菜单栏信息，用户没有登录", "失败", "获取树形菜单信息", "主页面");
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}