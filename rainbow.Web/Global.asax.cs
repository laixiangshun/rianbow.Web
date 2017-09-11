using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using rainbow.Code;
using Microsoft.Practices.Unity;
using rainbow.BLL.ExceptionHand;
using System.Web.UI;
namespace rainbow.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleTable.EnableOptimizations = true;//启用压缩
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //注入IOC-Unity实现依赖注入
            var container = new UnityContainer();
            DependencyRegisterType.Container_Sys(ref container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

        }

        /// <summary>
        /// 全局异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="?"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender,EventArgs e){
            string s = HttpContext.Current.Request.Url.ToString();
            HttpServerUtility server = HttpContext.Current.Server;
            if (server.GetLastError()!=null)
            {
                Exception ex= server.GetLastError();
                ExceptionHander.WriteException(ex); // 此处进行异常记录，可以记录到数据库或文本，也可以使用其他日志记录组件。
                Application["LastError"] = ex;
                int statusCode = HttpContext.Current.Response.StatusCode;
                string exceptionOprator = "/SysException/Error";
                try
                {
                    if (!string.IsNullOrEmpty(exceptionOprator))
                    {
                        exceptionOprator = new Control().ResolveUrl(exceptionOprator);
                        string url = string.Format("{0}?ErrorUrl={1}", exceptionOprator, server.UrlEncode(s));
                        string script = string.Format("<script language='javascript' type='text/javascript'>window.top.location='{0}';</script>", url);
                        Response.Write(script);
                        Response.End();
                    }
                }
                catch { }
            }
        }
    }
}
