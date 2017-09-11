using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.Common.JSON
{
    /// <summary>
    /// 系统Json结果通用类
    /// </summary>
    public class JsonHandler
    {
        public static JsonMessage CreateJsonMessage(bool success, string message, string value)
        {
            JsonMessage json=new JsonMessage{
                success=success,
                message=message,
                value=value
            };
            return json;
        }
        public static JsonMessage CreateJsonMessage(bool success, string messge)
        {
            JsonMessage json = new JsonMessage
            {
                success = success,
                message = messge
            };
            return json;
        }
    }

    public class JsonMessage
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string value { get; set; }
    }
}
