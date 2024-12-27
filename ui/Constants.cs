using System;

namespace ui;

public class Constants
{
    public const string BaseApiUrl = "http://localhost:5144/api";
    public const string RegisterEndpoint = $"{BaseApiUrl}/account/register";
    public const string LoginEndpoint = $"{BaseApiUrl}/account/login";
    
    // Quiz endpoints
    public const string QuizEndpoint = $"{BaseApiUrl}/Quiz";
    public const string QuizByIdEndpoint = $"{BaseApiUrl}/Quiz/{{id}}";
    public const string QuizResponseEndpoint = $"{BaseApiUrl}/QuizResponse";
    public const string UserResponsesEndpoint = $"{BaseApiUrl}/QuizResponse/user/{{0}}";
}
