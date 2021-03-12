namespace Kaizen.Core.Security
{
    public interface ITokenGenerator
    {
        string GenerateToken(string id, string username, string role);
    }
}
