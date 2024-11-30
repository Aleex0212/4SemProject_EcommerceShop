using Microsoft.AspNetCore.DataProtection;

namespace EcommerceShop.UmbracoFrontEnd
{
  public class SessionStore
  {
    private readonly IDataProtector _protector;
    private string? _protectedToken; // variable to save the token

    public SessionStore(IDataProtectionProvider provider)
    {
      _protector = provider.CreateProtector("SessionTokenProtector");
    }

    internal void SetSessionToken(string token)
    {
      if (string.IsNullOrWhiteSpace(token))
        throw new ArgumentException("Token cannot be null or empty.", nameof(token));

      // Encrypt and save
      _protectedToken = _protector.Protect($"bearer {token}");
    }

    public string GetSessionToken()
    {
      if (string.IsNullOrEmpty(_protectedToken))
        return string.Empty;

      // Decrypt and return
      return _protector.Unprotect(_protectedToken);
    }

    internal void ClearSessionToken()
    {
      _protectedToken = null;
    }
  }
}
