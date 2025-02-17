using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegistrationManager : MonoBehaviour
{
    [Header("UI Elements")]
    public InputField UsernameInput; 
    public InputField PasswordInput; 
    public Button RegisterButton; 
    public Text FeedbackText; 

    [Header("Server URL")]
    public string registerUrl = "http://localhost/unity_project/register_user.php";

    private void Start()
    {
        RegisterButton.onClick.AddListener(RegisterUser);
    }

    private void RegisterUser()
    {
        // On vérifie que les champs ne sont pas vides
        if (string.IsNullOrEmpty(UsernameInput.text) || string.IsNullOrEmpty(PasswordInput.text))
        {
            FeedbackText.text = "Veuillez remplir tous les champs.";
            return;
        }

        // Hachage du mot de passe
        string hashedPassword = HashPassword(PasswordInput.text);

        // Envoi des données au serveur
        StartCoroutine(SendRegistrationData(UsernameInput.text, hashedPassword));
    }

    private string HashPassword(string password)
    {
        // Hacher le mot de passe avec SHA256
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2")); // Convertir en hexadécimal
            }
            return builder.ToString();
        }
    }

    IEnumerator SendRegistrationData(string username, string hashedPassword)
    {
        // Préparer les données pour l'envoi
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", hashedPassword);

        using (UnityWebRequest request = UnityWebRequest.Post(registerUrl, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                SceneManager.LoadScene("Login"); 
            }
            else
            {
                FeedbackText.text = "Erreur lors de l'inscription: " + request.error;
            }
        }
    }
}
