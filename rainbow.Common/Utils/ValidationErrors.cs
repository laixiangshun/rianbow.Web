using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.Common.Utils
{
    public class ValidationError
    {
        public ValidationError() { }
        public string ErrorMessage { get; set; }
    }
    /// <summary>
    /// 通用的异常的集合类
    /// </summary>
    public class ValidationErrors : List<ValidationError>
    {
        /// <summary>
        /// 添加错误
        /// </summary>
        /// <param name="errorMessage"></param>
        public void Add(string errorMessage)
        {
            base.Add(new ValidationError { ErrorMessage = errorMessage });
        }

        /// <summary>
        /// 获取错误集合
        /// </summary>
        public string Error
        {
            get
            {
                string error = "";
                this.All(a =>
                {
                    error += a.ErrorMessage;
                    return true;
                });
                return error;
            }
        }
    }
}
