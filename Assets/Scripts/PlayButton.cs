using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayButton : MonoBehaviour
{
    public void Play()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
