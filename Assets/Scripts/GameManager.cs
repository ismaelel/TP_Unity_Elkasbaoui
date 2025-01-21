using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("GameManager Start");
    }
    
    // Méthode pour changer de scène
    public void LoadScene(string sceneName)
    {
        //SceneManager.LoadScene(sceneName); 
    }
}
