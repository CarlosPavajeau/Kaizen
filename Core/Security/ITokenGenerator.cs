namespace Kaizen.Core.Security
{
    public interface ITokenGenerator
    {
        string GenerateToken(string username, string role);
    }
}
