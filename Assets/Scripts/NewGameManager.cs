using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class NewGameManager : MonoBehaviour
{
    public Button newGameButton;
    public Text feedbackText; // Pour afficher les messages de statut
    private string newGameUrl = "http://localhost/unity_project/add_game.php";
    public DataGridManager dataGridManager;
    void Start()
    {
        newGameButton.onClick.AddListener(CreateNewGame);
    }

    public void CreateNewGame()
    {
        // Vérifier si un utilisateur est connecté
        if (UserSession.CurrentUserId == 0)
        {
            feedbackText.text = "Aucun utilisateur n'est connecté!";
            return;
        }

        StartCoroutine(AddGameToDatabase(UserSession.CurrentUserId));
    }

    IEnumerator AddGameToDatabase(int userId)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", userId);

        using (UnityWebRequest www = UnityWebRequest.Post(newGameUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                if (www.downloadHandler.text == "success")
                {
                    feedbackText.text = "Nouveau jeu créé avec succès!";
                    dataGridManager.UpdateStatsDisplay();
                }
                else
                {
                    feedbackText.text = "Erreur: " + www.downloadHandler.text;
                }
            }
            else
            {
                feedbackText.text = "Erreur de connexion au serveur.";
            }
        }
    }
}
