using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer mixer;
    public string parameterName;  

    public void SetVolume(float sliderValue)
    {
        mixer.SetFloat(parameterName, Mathf.Log10(sliderValue) * 20);
    }
}
