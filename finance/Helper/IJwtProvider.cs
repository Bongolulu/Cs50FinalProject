using finance.Models;

namespace finance.Helper;

public interface IJwtProvider
{
    Task<string> GenerateAsync(Benutzer user);

}