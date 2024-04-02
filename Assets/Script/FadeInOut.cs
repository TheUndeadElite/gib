using System.Collections;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasgroup;
    public float fadeDuration = 1.0f;
    public float fadeInDelay = 0.0f;
    public float fadeOutDelay = 0.0f;
    public bool fadein = false;
    public bool fadeout = false;

    // Call this method to start the fade-in effect with a delay
    public void StartFadeIn()
    {
        StartCoroutine(FadeInWithDelay());
    }

    // Coroutine for the fade-in effect with delay
    private IEnumerator FadeInWithDelay()
    {
        fadein = true;
        yield return new WaitForSeconds(fadeInDelay);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasgroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasgroup.alpha = 1f; // Ensure the canvas is fully visible at the end
        fadein = false;
    }

    // Call this method to start the fade-out effect with a delay
    public void StartFadeOut()
    {
        StartCoroutine(FadeOutWithDelay());
    }

    // Coroutine for the fade-out effect with delay
    private IEnumerator FadeOutWithDelay()
    {
        fadeout = true;
        yield return new WaitForSeconds(fadeOutDelay);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasgroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasgroup.alpha = 0f; // Ensure the canvas is fully transparent at the end
        fadeout = false;
    }
}
