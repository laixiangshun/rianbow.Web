using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.Models.Sys
{
     public class SysUserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string TrueName { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<bool> State { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreatePerson { get; set; }
    }
}
