using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;

public class FMODRainZoneTrigger : MonoBehaviour
{
    public StudioEventEmitter rainEmitter;
    private int insideLluviaZones = 0;

    [Header("Transición")]
    public float lluviaTransitionDuration = 1f; // ⏱ Duración en segundos
    public GameObject ParticleSystem;
    private Coroutine parameterTransitionCoroutine;
    private Coroutine fadeOutCoroutine;

    private void Start()
    {
        if (rainEmitter == null)
        {
            Debug.LogError("No se asignó el StudioEventEmitter.");
            return;
        }

        if (!rainEmitter.IsPlaying())
        {
            rainEmitter.Play();
        }

        rainEmitter.SetParameter("LLUVIA Domo", 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lluvia"))
        {
            insideLluviaZones++;
            UpdateRainParameter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lluvia"))
        {
            insideLluviaZones = Mathf.Max(0, insideLluviaZones - 1);
            UpdateRainParameter();
        }
    }

    private void UpdateRainParameter()
    {
        float targetValue = insideLluviaZones > 0 ? 1f : 0f;

        if (parameterTransitionCoroutine != null)
        {
            StopCoroutine(parameterTransitionCoroutine);
        }

        parameterTransitionCoroutine = StartCoroutine(TransitionRainParameter(targetValue, lluviaTransitionDuration));
    }

    private IEnumerator TransitionRainParameter(float target, float duration)
    {
        float currentValue = 0f;
        rainEmitter.EventInstance.getParameterByName("LLUVIA Domo", out currentValue);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float value = Mathf.Lerp(currentValue, target, elapsed / duration);
            rainEmitter.SetParameter("LLUVIA Domo", value);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rainEmitter.SetParameter("LLUVIA Domo", target);
    }

    

    // 🔊 Llamar esta función públicamente para hacer fade-out del volumen
    public void FadeOutAndStopRain(float duration = 2f)
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
        }
        fadeOutCoroutine = StartCoroutine(FadeOutCoroutine(duration));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        if (rainEmitter == null || !rainEmitter.IsPlaying())
            yield break;

        EventInstance instance = rainEmitter.EventInstance;

        float currentVolume = 1f;
        float timeElapsed = 0f;

        instance.getVolume(out currentVolume);

        while (timeElapsed < duration)
        {
            float newVolume = Mathf.Lerp(currentVolume, 0f, timeElapsed / duration);
            instance.setVolume(newVolume);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        instance.setVolume(0f);
        rainEmitter.Stop();
        ParticleSystem.SetActive(false);
    }
}
