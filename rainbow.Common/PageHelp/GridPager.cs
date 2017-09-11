using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.Common.PageHelp
{
    public class GridPager
    {
        public int rows { get; set; }//每页大小
        public int page { get; set; }//当前页是第几页
        public string order { get; set; }//排序方式
        public string sort { get; set; }//排序列
        public int totalRows { get; set; }//总行数

        public int totalPages
        {
            get
            {
                return (int)Math.Ceiling((float)totalRows / (float)rows);//向上取整
            }
        }
    }
}
