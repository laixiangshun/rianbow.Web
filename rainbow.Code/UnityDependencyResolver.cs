
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;


namespace rainbow.Code
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private const string HttpContextKey = "perRequestContainer";

        private readonly IUnityContainer container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }


        public object GetService(Type serviceType)
        {
            if (typeof(IController).IsAssignableFrom(serviceType))
            {
                return ChildContainer.Resolve(serviceType);
            }
            return IsRegistered(serviceType) ? ChildContainer.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (IsRegistered(serviceType))
            {
                yield return ChildContainer.Resolve(serviceType);
            }
            foreach (var service in ChildContainer.ResolveAll(serviceType))
            {
                yield return service;
            }
        }

        protected IUnityContainer ChildContainer
        {
            get
            {
                var childContainer = HttpContext.Current.Items[HttpContextKey] as IUnityContainer;
                if (childContainer == null)
                {
                    HttpContext.Current.Items[HttpContextKey] = childContainer = container.CreateChildContainer();
                }
                return childContainer;
            }
        }

        protected bool IsRegistered(Type typeToCheck)
        {
            var isRegistered = true;
            //只对接口
            if (typeToCheck.IsInterface || typeToCheck.IsAbstract)
            {
                isRegistered = ChildContainer.IsRegistered(typeToCheck);
                if (!isRegistered && typeToCheck.IsGenericType)
                {
                    var openGenericType = typeToCheck.GetGenericTypeDefinition();//返回泛型类型
                    isRegistered = ChildContainer.IsRegistered(openGenericType);
                }
            }
            return isRegistered;
        }

        public static void DisposeOfChildContainer()
        {
            var childContainer = HttpContext.Current.Items[HttpContextKey] as IUnityContainer;
            if (childContainer != null)
            {
                childContainer.Dispose();//释放当前的IOC容器
            }
        }
    }
}
