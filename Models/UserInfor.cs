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
        public string UserId { get; set; }
        public string Username { get; set; }
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
