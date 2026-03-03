using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class ClaimDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Permissions { get; set; } = new List<string>();
        public string SecretString { get; set; }
        public string Jti { get; set; }

        public ClaimDto()
        {
        }
        public ClaimDto(
            string secretString, 
            string userId, 
            string userName, 
            List<string> permissions)
        {
            UserId = userId;
            UserName = userName;
            Permissions = permissions;
            SecretString = secretString;
        }
    }
}
