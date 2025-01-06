using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DashOverlayController : MonoBehaviour
{
    [Header("UI Elements")]
    public Image dashOverlay; // Assign the UI Image here

    [Header("Effect Settings")]
    public float fadeDuration = 0.5f; // Time taken for fade-in/out
    public float dashEffectDuration = 1.0f; // Total effect duration

    private Coroutine fadeCoroutine;

    private void Start()
    {
        if (dashOverlay != null)
        {
            dashOverlay.enabled = false;
            dashOverlay.color = new Color(dashOverlay.color.r, dashOverlay.color.g, dashOverlay.color.b, 0);
        }
        else
        {
            Debug.LogError("DashOverlay Image is not assigned in the Inspector.");
        }
    }

    public void StartDashEffect()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(PlayDashOverlayEffect());
    }

    private IEnumerator PlayDashOverlayEffect()
    {
        // Enable Overlay and Fade In
        dashOverlay.enabled = true;
        float elapsedTime = 0f;
        Color overlayColor = dashOverlay.color;

        // Fade In
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            overlayColor.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            dashOverlay.color = overlayColor;
            yield return null;
        }

        // Hold Effect for Dash Duration
        yield return new WaitForSeconds(dashEffectDuration - (fadeDuration * 2));

        // Fade Out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            overlayColor.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            dashOverlay.color = overlayColor;
            yield return null;
        }

        dashOverlay.enabled = false;
    }
}
