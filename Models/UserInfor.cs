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
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public RoleValue.ROLE Role { get; set; }
        public Guid StaffId { get; set; }
        public string StaffName { get; set; }
    }
}
