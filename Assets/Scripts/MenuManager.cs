using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public void LoadLoginScene()
    {
        SceneManager.LoadScene("Login"); 
    }

    public void LoadCommentsScene()
    {
        SceneManager.LoadScene("Comms");
    }

    public void LoadStatsScene()
    {
        SceneManager.LoadScene("Stats");
    }
    public void LoadRegistrationScene()
    {
        SceneManager.LoadScene("RegistrationScene");
    }
    
    public void Logout()
    {
        // Réinitialiser les données de l'utilisateur
        UserSession.Logout();

        SceneManager.LoadScene("Login");
    }
}