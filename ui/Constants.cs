using System;

namespace ui;

public class Constants
{
    public const string BaseApiUrl = "http://localhost:5144/api";
    public const string RegisterEndpoint = $"{BaseApiUrl}/account/register";
    public const string LoginEndpoint = $"{BaseApiUrl}/account/login";
    
    // Quiz endpoints
    public const string QuizzesEndpoint = $"{BaseApiUrl}/Quizzes";
    public const string QuizByIdEndpoint = $"{BaseApiUrl}/Quizzes/{{id}}";
}
