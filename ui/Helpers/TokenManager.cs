using ui.DTOs;

namespace ui.Helpers;

public static class TokenManager
{
   
   
        public static async Task SetAuthToken(string token)
        {
            await SecureStorage.SetAsync("authToken", token);
        }

        public static void SetUserProperties(UserDTO userDTO)
        {
            Preferences.Set("user_name",userDTO.Name);
            Preferences.Set("birth_date",userDTO.BirthDate);

        }

        public static async Task<string> GetAuthTokenAsync()
        {
            return await SecureStorage.GetAsync("authToken")?? string.Empty;
        }

        public static string GetUserName()
        {
            return Preferences.Get("user_name", string.Empty);
        }

        public static async Task ClearAllDataAsync() 
        {
            SecureStorage.Remove("authToken");
            Preferences.Remove("user_name");
            Preferences.Clear();
        }
   
}