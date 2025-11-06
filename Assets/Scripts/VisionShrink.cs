using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VisionShrink : MonoBehaviour
{
    [SerializeField] private Light2D playerLight;
    [SerializeField] private float duration = 10f;
    [SerializeField] private float endRadius = 1.0f;

    private float startRadius;
    private float timer = 0f;

    private void Start()
    {
        startRadius = playerLight.pointLightOuterRadius;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);
        playerLight.pointLightOuterRadius = Mathf.Lerp(startRadius, endRadius, t);
    }

    public void RestoreVision(float restoreAmount)
    {
        float currentRadius = playerLight.pointLightOuterRadius;
        float targetRadius = Mathf.Min(currentRadius + restoreAmount, startRadius);

        playerLight.pointLightOuterRadius = targetRadius;
        timer = Mathf.Clamp(timer - (restoreAmount / (startRadius - endRadius)) * duration, 0f, duration);
    }
}
