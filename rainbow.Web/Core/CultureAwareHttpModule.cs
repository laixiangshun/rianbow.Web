using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Routing;

namespace rainbow.Web
{
    public class CultureAwareHttpModule:IHttpModule
    {
        private CultureInfo currentCulture;
        private CultureInfo currentUICulture;
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += SetCurrentCulture;
            context.EndRequest += RecoverCulture;
        }
        private void SetCurrentCulture(object sender, EventArgs args)
        {
            currentCulture = Thread.CurrentThread.CurrentCulture;
            currentUICulture = Thread.CurrentThread.CurrentUICulture;
            HttpContextBase contentWrapper = new HttpContextWrapper(HttpContext.Current);
            RouteData routeData = RouteTable.Routes.GetRouteData(contentWrapper);
            if (routeData == null)
            {
                return;
            }
            object culture;
            if (routeData.Values.TryGetValue("lang", out culture))
            {
                try
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(culture.ToString());
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture.ToString());
                }
                catch { }
            }
        }

        private void RecoverCulture(object sender, EventArgs args)
        {
            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentUICulture;
        }
    }
}