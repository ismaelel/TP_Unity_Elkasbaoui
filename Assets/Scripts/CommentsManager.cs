using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CommentsManager : MonoBehaviour
{
    [Header("UI Elements")]
    public InputField InputComment; 
    public Button SubmitButton; 
    public Transform CommentsParent;
    public GameObject CommentPrefab; 
    public Text FeedbackText;

    [Header("Server URLs")]
    public string submitCommentURL = "http://localhost/unity_project/submit_comment.php";
    public string getCommentsURL = "http://localhost/unity_project/get_comments.php";

    private void Start()
    {
        SubmitButton.onClick.AddListener(SubmitComment);
        StartCoroutine(GetComments());
    }

    private void SubmitComment()
    {
        if (string.IsNullOrEmpty(InputComment.text))
        {
            FeedbackText.text = "Veuillez remplir le champ du commentaire.";
            return;
        }

        // Envoie le commentaire au serveur
        StartCoroutine(SendComment(InputComment.text));
    }

    IEnumerator SendComment(string comment)
    {
        WWWForm form = new WWWForm();
        form.AddField("comment", comment);

        using (UnityWebRequest request = UnityWebRequest.Post(submitCommentURL, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                FeedbackText.text = "Commentaire envoyé avec succès!";
                // Recharge les commentaires pour afficher le nouveau
                StartCoroutine(GetComments());
            }
            else
            {
                FeedbackText.text = "Erreur lors de l'envoi: " + request.error;
            }
        }
    }

    IEnumerator GetComments()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(getCommentsURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("Server response: " + jsonResponse);

                try
                {
                    CommentsResponse response = JsonUtility.FromJson<CommentsResponse>(jsonResponse);
                    DisplayComments(response.comments);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error parsing JSON: " + e.Message);
                    FeedbackText.text = "Erreur de chargement des commentaires.";
                }
            }
            else
            {
                FeedbackText.text = "Erreur lors du chargement: " + request.error;
            }
        }
    }

    private void DisplayComments(Comment[] comments)
    {
        // Supprime les anciens commentaires
        foreach (Transform child in CommentsParent)
        {
            Destroy(child.gameObject);
        }

        // Ajoute chaque commentaire comme un nouvel élément
        foreach (Comment comment in comments)
        {
            GameObject commentObject = Instantiate(CommentPrefab, CommentsParent);
            Text commentText = commentObject.GetComponentInChildren<Text>();
            commentText.text = comment.comment; // Affiche uniquement le commentaire, sans le username
        }
        
        // Force une mise à jour du layout
        LayoutRebuilder.ForceRebuildLayoutImmediate(CommentsParent.GetComponent<RectTransform>());
    }
}

[System.Serializable]
public class CommentsResponse
{
    public Comment[] comments;
}

[System.Serializable]
public class Comment
{
    public string comment;
}
