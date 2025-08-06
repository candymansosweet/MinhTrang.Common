namespace Common.Services.TokenBlacklist
{
    public class InMemoryTokenBlacklist : ITokenBlacklist
    {
        private readonly HashSet<string> _revokedTokens = new();

        public void Revoke(string jti)
        {
            _revokedTokens.Add(jti);
        }

        public bool IsRevoked(string jti)
        {
            return _revokedTokens.Contains(jti);
        }
    }

}
