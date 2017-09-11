using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace rainbow.Common.Utils
{
    /// <summary>
    /// 对结果处理的通用类
    /// </summary>
     public class ResultHelper
    {
         /// <summary>
        /// 创建一个全球唯一标识的32位ID
         /// </summary>
         public static string NewId {
             get
             {
                 string id = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                 string guid = Guid.NewGuid().ToString().Replace("-", "");
                 id += guid.Substring(0, 10);
                 return id;
             }
         }

         public static string NewTimeId
         {
             get { 
                 string id = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                return id;
             }
         }

         public static DateTime NowTime
         {
             get
             {
                 return DateTime.Now;
             }
         }

         /// <summary>
         /// 去除字符串中的html标记
         /// </summary>
         /// <param name="htmlstring"></param>
         /// <returns></returns>
         public static string NoHtml(string htmlstring)
         {
             htmlstring = Regex.Replace(htmlstring, @"<script[^>]*?>.*?</script>","", RegexOptions.IgnoreCase);//去掉脚本
             //去电html
             htmlstring = Regex.Replace(htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"-->", "", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"&hellip;", "", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"&mdash;", "", RegexOptions.IgnoreCase);
             htmlstring = Regex.Replace(htmlstring, @"&ldquo;", "", RegexOptions.IgnoreCase);
             htmlstring.Replace("<", "");
             htmlstring = Regex.Replace(htmlstring, @"&rdquo;", "", RegexOptions.IgnoreCase);
             htmlstring.Replace(">", "");
             htmlstring.Replace("\r\n", "");
             htmlstring = HttpContext.Current.Server.HtmlEncode(htmlstring).Trim();
             return htmlstring;
         }

         /// <summary>
         /// 截取指定长度的字符串
         /// </summary>
         /// <param name="value"></param>
         /// <param name="length"></param>
         /// <returns></returns>
         public static string SubValue(string value, int length)
         {
             if (value.Length >length)
             {
                 value = value.Substring(0, length);
                 value = value + "...";
                 return NoHtml(value);
             }
             else
             {
                 return NoHtml(value);
             }
         }

         /// <summary>
         /// 将字符串还原的时候
         /// </summary>
         /// <param name="inputstring"></param>
         /// <returns></returns>
         public static string InputString(string inputstring)
         {
             if (inputstring != null && inputstring != string.Empty)
             {
                 inputstring = inputstring.Trim();
                 inputstring = inputstring.Replace("<br>", "\n");
                 inputstring = inputstring.Replace("&", "&amp");
                 inputstring = inputstring.Replace("'", "''");
                 inputstring = inputstring.Replace("<", "&lt");
                 inputstring = inputstring.Replace(">", "&gt");
                 inputstring = inputstring.Replace("chr(60)", "&lt");
                 inputstring = inputstring.Replace("chr(37)", "&gt");
                 inputstring = inputstring.Replace("\"", "&quot");
                 inputstring = inputstring.Replace(";", ";");
                 return inputstring;
             }
             else
             {
                 return "";
             }
         }

         /// <summary>
         /// 将字符串的html转换的时候
         /// </summary>
         /// <param name="outputString"></param>
         /// <returns></returns>
         public static string OutputText(string outputString)
         {
             if (outputString != null && outputString != string.Empty)
             {
                 outputString = outputString.Trim();
                 outputString = outputString.Replace("&amp", "&");
                 outputString = outputString.Replace("''", "'");
                 outputString = outputString.Replace("&lt", "<");
                 outputString = outputString.Replace("&gt", ">");
                 outputString = outputString.Replace("&lt", "chr(60)");
                 outputString = outputString.Replace("&gt", "chr(37)");
                 outputString = outputString.Replace("&quot", "\"");
                 outputString = outputString.Replace(";", ";");
                 outputString = outputString.Replace("\n", "<br>");
                 return outputString;
             }
             else
             {
                 return "";
             }
         }

         /// <summary>
         /// 格式化文本，防止sql注入
         /// </summary>
         /// <param name="html"></param>
         /// <returns></returns>
         public static string Formatstr(string html)
         {
             Regex regex1 = new Regex(@"<script[\s\S]+</script *>",RegexOptions.IgnoreCase);
             Regex regex2=new Regex(@" href *= *[\s\S]*script *:",RegexOptions.IgnoreCase);
             Regex regex3 = new Regex(@" on[\s\S]*=", RegexOptions.IgnoreCase);
             Regex regex4 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
             Regex regex5 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
             Regex regex6 = new Regex(@"select", RegexOptions.IgnoreCase);
             Regex regex7 = new Regex(@"update", RegexOptions.IgnoreCase);
             Regex regex8 = new Regex(@"delete", RegexOptions.IgnoreCase);
             Regex regex9 = new Regex(@"insert", RegexOptions.IgnoreCase);
             html = regex1.Replace(html, ""); //过滤<script></script>标记
             html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
             html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
             html = regex4.Replace(html, ""); //过滤iframe
             html = regex5.Replace(html, "");//过滤frameset
             html = regex6.Replace(html, "s_elect");
             html = regex7.Replace(html, "u_pudate");
             html = regex8.Replace(html, "d_elete");
             html = regex9.Replace(html, "i_insert");
             html = html.Replace("'", "’");
             html = html.Replace("&nbsp;", " ");
             return html;
         }

         /// <summary>
         /// 检查sql语句合法性
         /// </summary>
         /// <param name="sql"></param>
         /// <param name="msg"></param>
         /// <returns></returns>
         public static bool ValidateSQL(string sql, ref string msg)
         {
             if (sql.ToLower().IndexOf("delete") > 0)
             {
                 msg = "sql参数中含有非法语句delete";
                 return false;
             }
             if (sql.ToLower().IndexOf("update") > 0)
             {
                 msg = "SQL参数中含有非法语句UPDATE";
                 return false;
             }
             if (sql.ToLower().IndexOf("insert") > 0)
             {
                 msg = "SQL参数中含有非法语句INSERT";
                 return false;
             }
             return true;
         }

         /// <summary>
         /// 将日期转换为字符串
         /// </summary>
         /// <param name="dt"></param>
         /// <returns></returns>
         public static string DateTimeConvertString(DateTime? dt)
         {
             if (dt == null)
             {
                 return "";
             }
             else
             {
                 return Convert.ToDateTime(dt.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
             }
         }
         /// <summary>
         /// 将字符串转换为日期格式
         /// </summary>
         /// <param name="str"></param>
         /// <returns></returns>
         public static DateTime? StringConvertDatetime(string str)
         {
             if (str == null)
             {
                 return null;
             }
             else
             {
                 try
                 {
                     return Convert.ToDateTime(str);
                 }
                 catch
                 {
                     return null;
                 }
             }
         }

         /// <summary>
         /// 获取IP
         /// </summary>
         /// <returns></returns>
         public static string GetUserIp()
         {
             if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
             {
                 return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
             }
             else
             {
                 return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
             }
         }
    }
}
