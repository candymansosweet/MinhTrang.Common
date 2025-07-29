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
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public List<string> Permissions { get; set; } = new List<string>();
        public string SecretString { get; set; }

        public ClaimDto()
        {
        }
        public ClaimDto(
            string secretString, 
            string accountId, 
            string accountName, 
            List<string> permissions)
        {
            AccountId = accountId;
            AccountName = accountName;
            Permissions = permissions;
            SecretString = secretString;
        }
    }
}
