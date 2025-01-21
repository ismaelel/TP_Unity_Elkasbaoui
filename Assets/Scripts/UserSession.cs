using UnityEngine;

public static class UserSession
{
    public static int CurrentUserId; 
    public static string CurrentUsername;
    
    public static void Logout()
    {
        CurrentUserId = 0;
        CurrentUsername = null;
    }
}
