using Classes;

namespace finance.Helper;

public interface IJwtProvider
{
    Task<string> GenerateAsync(Benutzer user);

}