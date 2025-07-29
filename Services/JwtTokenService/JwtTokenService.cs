using Common.Constants;
using Common.Dtos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Common.Constants.RoleValue;

namespace Common.Services.JwtTokenService
{
    public class JwtTokenService : IJwtTokenService
    {
        // HEADER.PAYLOAD.SIGNATURE
        // HEADER: chứa thông tin về thuật toán mã hóa và loại token.
        // PAYLOAD: chứa các thông tin (claims) về người dùng và các dữ liệu khác.
        // SIGNATURE: được tạo bằng cách mã hóa HEADER và PAYLOAD với một khóa bí mật (secret key) sử dụng thuật toán đã chỉ định trong HEADER.
        // => ghép HEADER và PAYLOAD với nhau, sau đó mã hóa chúng bằng thuật toán đã chỉ định trong HEADER và khóa bí mật để tạo ra phần SIGNATURE.
        // nếu kẻ tấn công có được token, họ có thể đọc được các thông tin trong PAYLOAD nhưng không thể thay đổi chúng mà không làm cho phần SIGNATURE không hợp lệ.
        // trừ khi kẻ tấn công biết được khóa bí mật, họ có thể tạo ra một token giả mạo với các thông tin tùy ý.
        public string GenerateToken(ClaimDto claimDto)
        {
            // Tạo khóa bảo mật đối xứng từ chuỗi bí mật.
            // Dù chuỗi bí mật bị lộ, nếu không biết đúng thuật toán và cấu trúc ký,
            // kẻ xấu vẫn không dễ giả mạo token hợp lệ (nhưng bảo mật sẽ giảm đáng kể nếu key bị lộ).
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claimDto.SecretString));

            // Tạo thông tin ký JWT sử dụng khóa bảo mật và thuật toán HMAC-SHA256.
            // Thông tin này sẽ được dùng để ký token nhằm đảm bảo tính toàn vẹn và xác thực.
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(Claims.AccountId, claimDto.AccountId), // user ID
                new Claim(Claims.AccountName, claimDto.AccountName), // username
                new Claim(Claims.Permissions, string.Join(",", claimDto)), // vai trò
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(
                new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(180),
                    SigningCredentials = signingCredentials,
                    TokenType = "Bearer" // loại token là Bearer
                }
            );
            return tokenHandler.WriteToken(token);
        }

        public ClaimDto? ValidateToken(string? token, string secretString)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            ClaimDto? claimDto = null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretString);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // bỏ qua kiểm tra issuer nếu không cần
                    ValidateAudience = false, // bỏ qua kiểm tra audience nếu không cần
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // không cho phép lệch thời gian
                };
                // Giải mã và xác thực token
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Kiểm tra thuật toán có đúng không (phòng token giả mạo)
                if (validatedToken is JwtSecurityToken jwtToken &&
                    jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    claimDto = new ClaimDto();
                    claimDto.AccountId = principal.FindFirst(Claims.AccountId)?.Value ?? "";
                    claimDto.AccountName = principal.FindFirst(Claims.AccountName)?.Value ?? "";
                    claimDto.Permissions = principal.FindFirst(Claims.Permissions)?.Value?.Split(',').ToList() ?? new List<string>();
                }
                return claimDto;
            }
            catch
            {
                return null;
            }
        }
    }
}
