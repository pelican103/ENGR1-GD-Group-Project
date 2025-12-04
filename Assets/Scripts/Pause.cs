using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenu; 
    public AudioSource bgmAudio; 

    private bool isPaused = false;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        pauseMenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0 : 1; 
        if (isPaused)
            bgmAudio.Pause();
        else
            bgmAudio.UnPause();
    
    }
}
