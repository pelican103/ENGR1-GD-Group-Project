using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayButton : MonoBehaviour
{
    [SerializeField] public string sceneName;
    
    
    public void Play()
    {
        if (sceneName != null && sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
            return;
        }

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
