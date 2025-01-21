using UnityEngine;

public static class UserSession
{
    public static int CurrentUserId; 
    public static string CurrentUsername; // pas utilisé au final
    
    public static void Logout()
    {
        CurrentUserId = 0;
        CurrentUsername = null;
    }
}
