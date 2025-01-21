using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class DataGridManager : MonoBehaviour
{
    public Text statsText;  
    public Text statusText; 
    public string statsUrl = "http://localhost/unity_project/get_game_stats.php";
    
    void Start()
    {
        // Récupérer les statistiques de l'utilisateur actuellement connecté
        StartCoroutine(GetGameStats(UserSession.CurrentUserId)); 
    }

    // Public method to update stats display
    public void UpdateStatsDisplay()
    {
        StartCoroutine(GetGameStats(UserSession.CurrentUserId));
    }

    // Coroutine pour récupérer les statistiques depuis le serveur
    IEnumerator GetGameStats(int userId)
    {
        // Vérification simple que l'ID utilisateur est valide
        if (userId <= 0)
        {
            Debug.LogError("ID utilisateur invalide.");
            statusText.text = "ID utilisateur invalide!";
            yield break;
        }

        // Créer une requête pour envoyer l'ID utilisateur
        WWWForm form = new WWWForm();
        form.AddField("user_id", userId);

        // Faire une requête POST pour récupérer les statistiques
        using (UnityWebRequest www = UnityWebRequest.Post(statsUrl, form))
        {
            yield return www.SendWebRequest();

            // Vérifier si la requête a réussi
            if (www.result == UnityWebRequest.Result.Success)
            {
                string response = www.downloadHandler.text;

                // Vérifier si la réponse est vide
                if (string.IsNullOrEmpty(response))
                {
                    Debug.LogError("Réponse vide du serveur.");
                    statusText.text = "Aucune donnée trouvée.";
                    yield break;
                }

                // Traiter la réponse JSON
                try
                {
                    // Désérialiser la réponse JSON dans un objet wrapper
                    StatsResponseWrapper wrapper = JsonUtility.FromJson<StatsResponseWrapper>("{\"stats\":" + response + "}");

                    // Vérifier si des statistiques ont été retournées
                    if (wrapper.stats.Length > 0)
                    {
                        statsText.text = "Statistiques des dernières parties :\n\n";

                        foreach (var stat in wrapper.stats)
                        {
                            statsText.text += $"- Date : {stat.gameDate}\n";
                            statsText.text += $"  Score : {stat.score}\n";
                            statsText.text += $"  Niveau : {stat.level}\n";
                            statsText.text += $"  Temps joué : {FormatTime(stat.timePlayed)}\n\n";
                        }
                    }
                    else
                    {
                        statusText.text = "Aucune statistique disponible.";
                    }
                }
                catch (System.Exception ex)
                {
                    //Debug.LogError("Error parsing JSON: " + ex.Message);
                   // statusText.text = "Error displaying stats.";
                }
            }
            else
            {
                // En cas d'échec de la requête
                Debug.LogError("La demande a échoué: " + www.error);
                statusText.text = "Erreur lors de la récupération des statistiques.";
            }
        }
    }

    // Fonction pour formater le temps en heures:minutes:secondes
    private string FormatTime(int seconds)
    {
        int hours = seconds / 3600;
        int minutes = (seconds % 3600) / 60;
        int secs = seconds % 60;
        return $"{hours:D2}:{minutes:D2}:{secs:D2}";
    }
}

// Classe pour représenter les statistiques du joueur
[System.Serializable]
public class StatsResponse
{
    public string gameDate; // Date de la partie
    public int score;
    public int level;
    
    [SerializeField] 
    private string time_played; // Temps joué en string (reçu du JSON)

    public int timePlayed
    {
        get => int.Parse(time_played); // Convertir en int
    }
}


[System.Serializable]
public class StatsResponseWrapper
{
    public StatsResponse[] stats;
}
