using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace rainbow.Common.JSON
{
    /// <summary>
    /// 自定义Json视图——处理DateTime类型的数据
    /// </summary>
    public class CustomJsonResult:JsonResult
    {
        /// <summary>
        /// 格式化字符串-指定时间日期的格式
        /// </summary>
        public string FormatStr
        {
            get;
            set;
        }

        /// <summary>
        /// 重新执行视图
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            if (this.Data != null)
            {
                string jsonString=JsonConvert.SerializeObject(this.Data);//json序列化
                string p = @"\\/Date\((\d+)\)\\/";
                MatchEvaluator matchEvalutor = new MatchEvaluator(this.ConvertJsonDateToDateString);//委托
                Regex reg = new Regex(p);
                jsonString = reg.Replace(jsonString, matchEvalutor);
                response.Write(jsonString);
            }
           // base.ExecuteResult(context);
        }
        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278)转为字符串
        /// </summary>
        /// <param name="m">正则匹配</param>
        /// <returns>格式化后的字符串</returns>
        private string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString(FormatStr);
            return result;
        }
    }
}
