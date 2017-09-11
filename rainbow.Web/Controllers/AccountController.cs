using Microsoft.Practices.Unity;
using rainbow.Common.JSON;
using rainbow.Common.MD5;
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
    public class AccountController : BaseController
    {
        [Dependency]
        public IAccountBLL accountBLL { get; set; }
        //
        // GET: /Account/
        public ActionResult Index()
        {
            LogHandler.WriteServiceLog(this.GetUserName(), "进入登录界面", "成功", "登录页面", "登录页面");
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwssword"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Login(string username, string password, string code)
        {
            if (Session["code"] == null)
            {
                return Json(JsonHandler.CreateJsonMessage(false, "请重新获取验证码"), JsonRequestBehavior.AllowGet);
            }
            if (Session["code"].ToString().ToLower() != code.ToLower())
            {
                return Json(JsonHandler.CreateJsonMessage(false, "验证码错误"), JsonRequestBehavior.AllowGet);
            }
            SysUser user = accountBLL.Login(username, ValueConvert.MD5(password));
            if (user == null)
            {
                return Json(JsonHandler.CreateJsonMessage(false, "用户名或者密码错误"), JsonRequestBehavior.AllowGet);
            }
            else if (!Convert.ToBoolean(user.State))
            {
                return Json(JsonHandler.CreateJsonMessage(false, "账户被系统禁用"), JsonRequestBehavior.AllowGet);
            }
            AccountModel account = new AccountModel
            {
                Id = user.Id,
                TrueName = user.TrueName
            };
            Session["Account"] = account;
            LogHandler.WriteServiceLog(this.GetUserName(), "用户登录", "成功", "登录", "系统登录");
            return Json(JsonHandler.CreateJsonMessage(true, "登录成功"), JsonRequestBehavior.AllowGet);
        }
	}
}