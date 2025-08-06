namespace Common.Services.TokenBlacklist
{
    public interface ITokenBlacklist
    {
        void Revoke(string jti);
        bool IsRevoked(string jti);
    }
}
