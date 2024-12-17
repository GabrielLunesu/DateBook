using ui.DTOs;

namespace ui.Helpers;

public static class TokenManager
{
    private const string TokenKey = "auth_token";
    private const string UserIdKey = "user_id";

    public static async Task SetAuthToken(string token)
    {
        await SecureStorage.SetAsync(TokenKey, token);
    }

    public static async Task<string> GetAuthToken()
    {
        return await SecureStorage.GetAsync(TokenKey);
    }

    public static async Task SetUserId(string userId)
    {
        await SecureStorage.SetAsync(UserIdKey, userId);
    }

    public static async Task<string> GetUserId()
    {
        return await SecureStorage.GetAsync(UserIdKey);
    }

    public static void ClearAll()
    {
        SecureStorage.Remove(TokenKey);
        SecureStorage.Remove(UserIdKey);
    }
}