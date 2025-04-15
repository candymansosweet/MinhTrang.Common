using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.JwtTokenService
{
    public interface IJwtTokenService
    {
        public string GenerateToken<T>(T primeKey, string secretString);
        public Guid? ValidateToken(string? token, string secretString);
    }
}
