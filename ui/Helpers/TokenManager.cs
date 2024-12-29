using ui.DTOs;

namespace ui.Helpers;

public static class TokenManager
{
    private const string TokenKey = "auth_token";
    private const string UserIdKey = "userId";

    public static async Task SetAuthToken(string token)
    {
        await SecureStorage.SetAsync(TokenKey, token);
    }

    public static async Task<string> GetAuthToken()
    {
        return await SecureStorage.GetAsync(TokenKey);
    }

    // public static async Task<string> GetUserIdAndToken()

    public static async Task SetUserId(int userId)
    {
        await SecureStorage.SetAsync(UserIdKey, userId.ToString());
    }

    public static async Task<int?> GetUserId()
    {
        var userIdStr = await SecureStorage.GetAsync(UserIdKey);
        if (int.TryParse(userIdStr, out int userId))
        {
            return userId;
        }
        return null;
    }

    public static void ClearAll()
    {
        SecureStorage.Remove(TokenKey);
        SecureStorage.Remove(UserIdKey);
    }
}