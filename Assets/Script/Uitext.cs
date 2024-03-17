using System.Collections;
using TMPro;
using UnityEngine;

public class PhasingText : MonoBehaviour
{
    public TMP_Text interactHint;
    public float fadeInDelay = 0.5f; // Delay in seconds before fading in
    public float fadeInDuration = 1.5f; // Duration in seconds for fading in
    public float duration = 3f; // Duration in seconds before the text phases out
    public float fadeOutDuration = 1f; // Duration in seconds for fading out

    private void Start()
    {
        // Check if TMP_Text component is assigned
        if (interactHint == null)
        {
            Debug.LogError("No TextMeshPro Text component assigned to PhasingText script.");
            enabled = false; // Disable the script if TMP_Text component is not assigned
            return;
        }

        // Hide the text at the beginning
        Color initialColor = interactHint.color;
        initialColor.a = 0f;
        interactHint.color = initialColor;

        StartCoroutine(StartPhasing()); // Start the phasing process
    }

    IEnumerator StartPhasing()
    {
        yield return new WaitForSeconds(fadeInDelay); // Wait before starting the fade-in
        StartCoroutine(FadeIn());
        StartCoroutine(PhaseOut());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color startColor = interactHint.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque

        while (elapsedTime < fadeInDuration)
        {
            // Interpolate color to fade in
            interactHint.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the text becomes fully opaque at the end
        interactHint.color = targetColor;
    }

    IEnumerator PhaseOut()
    {
        yield return new WaitForSeconds(duration - fadeOutDuration); // Wait before starting the fade-out
        float elapsedTime = 0f;
        Color startColor = interactHint.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Fade to transparent

        while (elapsedTime < fadeOutDuration)
        {
            // Interpolate color to fade out
            interactHint.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the text becomes fully transparent at the end
        interactHint.color = targetColor;

        // Optionally, you can disable or hide the text object after phasing out
        gameObject.SetActive(false);
    }
}
