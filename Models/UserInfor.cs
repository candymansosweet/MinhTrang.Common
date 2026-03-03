using Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class UserInfor
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public RoleValue.ROLE Role { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
