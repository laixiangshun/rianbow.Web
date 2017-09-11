using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.Models.Sys
{
    /// <summary>
    /// 权限的视图类
    /// </summary>
     public class permModel
    {
        public string KeyCode { get; set; }//操作码
        public bool IsValid { get; set; }//是否验证
    }
}
