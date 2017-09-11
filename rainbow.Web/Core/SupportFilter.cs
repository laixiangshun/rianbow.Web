using rainbow.BLL;
using rainbow.DAL;
using rainbow.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace rainbow.Web.Core
{
    /// <summary>
    /// 自定义拦截器
    /// </summary>
    public class SupportFilter:ActionFilterAttribute
    {
        public string ActionName { get; set; }//ActionName是自定义Action的名称,与数据库操作码对应
        public string Area { get; set; }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        /// <summary>
        /// 在action方法之前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            RouteData routeData = routes.GetRouteData(filterContext.HttpContext);
            //取出区域的控制器Action，id
            string ctlName = filterContext.Controller.ToString();
            string[] routeInfo = ctlName.Split('.');
            string controller = null;
            string action = null;
            string id = null;
            int iAreas = Array.IndexOf(routeInfo, "Areas");
            if (iAreas > 0)
            {
                //取区域和控制器
                Area = routeInfo[iAreas + 1];
            }
            int ctlIndex = Array.IndexOf(routeInfo, "Controllers");
            ctlIndex++;
            controller = routeInfo[ctlIndex].Replace("Controller", "").ToLower();
            string url = HttpContext.Current.Request.Url.ToString().ToLower();
            string[] urlArray = url.Split('/');
            int urlCtlIndex = Array.IndexOf(urlArray, controller);
            urlCtlIndex++;
            if (urlArray.Count() > urlCtlIndex)
            {
                action = urlArray[urlCtlIndex];
            }
            urlCtlIndex++;
            if (urlArray.Count() > urlCtlIndex)
            {
                id = urlArray[urlCtlIndex];
            }
            action = string.IsNullOrEmpty(action) ? "Index" : action;
            int actionIndex = action.IndexOf("?", 0);
            if (actionIndex > 1)
            {
                action = action.Substring(0, actionIndex);
            }
            id = string.IsNullOrEmpty(id) ? "" : id;
            //URL路径
            string filePath = HttpContext.Current.Request.FilePath;
            AccountModel account = filterContext.HttpContext.Session["Account"] as AccountModel;
            if (ValiddatePermission(account, controller, action, filePath))
            {
                return;
            }
            else
            {
                filterContext.Result = new EmptyResult();
                return;
            }
            //base.OnActionExecuting(filterContext);
        }
        public bool ValiddatePermission(AccountModel account, string controller, string action, string filepath)
        {
            bool bResult = false;
            string actionName = string.IsNullOrEmpty(ActionName) ? action : ActionName;
            if (account != null)
            {
                List<permModel> perm = null;
                if (!string.IsNullOrEmpty(Area))
                {
                    controller = Area + "/" + controller;
                }
                perm = (List<permModel>)HttpContext.Current.Session[filepath];
                //if (perm == null || perm.Count<=0)
                //{
                    using(SysUserBLL userBLL = new SysUserBLL() { 
                        sysRightRepository = new SysRightRepository()
                    })
                    {

                        perm = userBLL.GetPermission(account.Id, controller);//获取当前用户的权限表
                        HttpContext.Current.Session[filepath] = perm;//获取的权限放入回话由Controller调用
                    }
               // }
                //当用户访问Index时只需权限>0就可以访问
                if (actionName.ToLower() == "index")
                {
                    if (perm.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        bResult = false;
                        HttpContext.Current.Response.Write("你没有操作权限，请联系管理员");
                    }
                }
                //查询当前Action是否具有操作权限，大于0表示有，否则没有
                int count = perm.Where(a => a.KeyCode.ToLower() == actionName.ToLower()).Count();
                if (count > 0)
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
                    HttpContext.Current.Response.Write("你没有操作权限，请联系管理员");
                }
            }
            return bResult;
        }
    }
}