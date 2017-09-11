using rainbow.Common.Utils;
using rainbow.Models.Sys;
using rainbow.Web.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rainbow.Web.Controllers
{
    /// <summary>
    /// 我们自己的Controller在继承BaseController即可
    /// 为方便自己个性化的需要又定义了两个MyJosn的方法
    /// </summary>
    public class BaseController : Controller
    {
       public ValidationErrors errors = new ValidationErrors();

       public AccountModel user
       {
           get
           {
               return (AccountModel)Session["Account"];
           }
       }
        /// <summary>
        /// 返回自定义的JsonResult
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {

            return new CustomJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                FormatStr = "yyyy-MM-dd HH:mm:ss"
            };
        }

        /// <summary>
        /// 返回指定日期格式的JsonResult
        /// </summary>
        /// <param name="data"></param>
        /// <param name="behavior"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        protected JsonResult MyJson(object data, JsonRequestBehavior behavior, string format)
        {
            return new CustomJsonResult
            {
                Data = data,
                JsonRequestBehavior = behavior,
                FormatStr = format
            };
        }
        /// <summary>
        /// 返回指定的JsonResult
        /// </summary>
        /// <param name="data"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        protected JsonResult MyJson(object data, string format)
        {
            return new CustomJsonResult
            {
                Data = data,
                FormatStr = format
            };
        }

        /// <summary>
        /// 获取当前登录用户的ID
        /// </summary>
        /// <returns></returns>
        public string GetUserId()
        {
            if (Session["Account"] != null)
            {
                AccountModel info = (AccountModel)Session["Account"];
                return info.Id;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取当前登录用户的姓名
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            if (Session["Account"] != null)
            {
                AccountModel info = (AccountModel)Session["Account"];
                return info.TrueName;
            }
            else
            {
                return "未知用户";
            }
        }
        /// <summary>
        /// 获取当前登录用户的信息
        /// </summary>
        /// <returns></returns>
        public AccountModel GetAccount()
        {
            if (Session["Account"] != null)
            {
                AccountModel info = (AccountModel)Session["Account"];
                return info;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 检查SQL语句合法性
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ValidateSQL(string sql, ref string msg)
        {
            if (sql.ToLower().IndexOf("delete") > 0)
            {
                msg = "查询参数中含有非法语句DELETE";
                return false;
            }
            if (sql.ToLower().IndexOf("update") > 0)
            {
                msg = "查询参数中含有非法语句UPDATE";
                return false;
            }

            if (sql.ToLower().IndexOf("insert") > 0)
            {
                msg = "查询参数中含有非法语句INSERT";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取当前页或操作访问权限
        /// </summary>
        /// <returns></returns>
        public List<permModel> GetPermission()
        {
            string filepath = HttpContext.Request.FilePath;
            List<permModel> perm =(List<permModel>)Session[filepath];
            return perm;
        }
	}
}