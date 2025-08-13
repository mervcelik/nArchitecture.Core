using System.IdentityModel.Tokens.Jwt;

namespace Core.Security.Extensions;

public static class JwtExtensions
{
    public static DateTime? GetExpiryDate(this string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return null;

        var handler = new JwtSecurityTokenHandler();

        JwtSecurityToken jwt;
        try
        {
            jwt = handler.ReadJwtToken(token);
        }
        catch
        {
            // Geçersiz token
            return null;
        }

        var expClaim = jwt.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
        if (expClaim != null && long.TryParse(expClaim, out var expSeconds))
        {
            return DateTimeOffset.FromUnixTimeSeconds(expSeconds).UtcDateTime;
        }

        return null;
    }

    public static bool IsExpired(this string token, int skewSeconds = 0)
    {
        var expiry = token.GetExpiryDate();
        if (expiry == null)
            return true; // geçersiz token expired kabul edilir

        return DateTime.UtcNow >= expiry.Value.AddSeconds(-skewSeconds);
    }
}