using rainbow.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace rainbow.Web
{
    public static class ExtendMvcHtml
    {
        /// <summary>
        /// 权限按钮
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="icon"></param>
        /// <param name="text"></param>
        /// <param name="perm"></param>
        /// <param name="keycode"></param>
        /// <param name="hr"></param>
        /// <returns></returns>
        public static MvcHtmlString ToolButton(this HtmlHelper helper, string id, string icon, string text, List<permModel> perm, string keycode, bool hr)
        {
            StringBuilder sb = new StringBuilder();
            if (perm.Where(a => a.KeyCode == keycode).Count() > 0)
            {
                sb.AppendFormat("<a id=\"{0}\" style=\"float:left;\" class=\"l-btn l-btn-plain\">", id);
                sb.AppendFormat("<span class=\"l-btn-left\"><span class=\"l-btn-text {0}\" style=\"padding-left:20px;display:initial;\">", icon);
                sb.AppendFormat("{0}</span></span></a>", text);
                if (hr)
                {
                    sb.Append("<div clss=\"datagrid-btn-separator\"></div>");
                }
                return new MvcHtmlString(sb.ToString());
            }
            else
            {
                sb.Append("<a style=\"float:left;\" class=\"l-btn l-btn-plain\" onclick=\"NoRight();\">");
                sb.AppendFormat("<span class=\"l-btn-left\"><span class=\"l-btn-text {0}\" style=\"padding-left:20px;display:initial;\">", icon);
                sb.AppendFormat("{0}</span></span></a>", text);
                if (hr)
                {
                    sb.Append("<div class=\"datagrid-btn-separator\"></div>");
                }
                return new MvcHtmlString(sb.ToString());
            }
        }

        /// <summary>
        /// 普通按钮
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="icon"></param>
        /// <param name="text"></param>
        /// <param name="hr"></param>
        /// <returns></returns>
        public static MvcHtmlString ToolButton(this HtmlHelper helper, string id, string icon, string text, bool hr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<a id=\"{0}\" style=\"float:left;\" class=\"l-btn l-btn-plain\">", id);
            sb.AppendFormat("<span class=\"l-btn-left\"><span class=\"l-btn-text {0}\" style=\"padding-left:20px;display:initial;\">", icon);
            sb.AppendFormat("{0}</span></span></a>", text);
            if (hr)
            {
                sb.Append("<div class=\"datagrid-btn-separator\"></div>");
            }
            return new MvcHtmlString(sb.ToString());
        }
    }
}