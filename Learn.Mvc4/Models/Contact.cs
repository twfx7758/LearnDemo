using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Mvc4.Models
{
    public class Contact
    {
        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("电话号码")]
        public string PhoneNo { get; set; }
        [DisplayName("邮箱地址")]
        public string Email { get; set; }
    }
}
