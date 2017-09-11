using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.Models.Sys
{
    /// <summary>
    /// 由EF生成的实体模型是拥有事务状态的，我们需要为SysSample的类再次定义个模型，SysSampleModel，这个模型我们可以加属性，序列化、脱离事物
    /// </summary>
    public class SysSampleModel
    {
        [Display(Name="ID")]
        public string Id { get; set; }

        [Display(Name="名称")]
        public string Name { get; set; }

        [Display(Name="年龄")]
        [Range(0,150)]
        public int? Age { get; set; }

        [Display(Name="生日")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString="{:yyyy-MM-dd HH:mm:ss}",ApplyFormatInEditMode=true)]
        public DateTime? Bir { get; set; }

        [Display(Name="简介")]
        public string Note { get; set; }

        [Display(Name="创建时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd HH:mm:ss}",ApplyFormatInEditMode=true)]
        public DateTime? CreateTime { get; set; }

        [Display(Name="照片")]
        public string Photo { get; set; }
    }
}
