using System.Collections;
using UnityEngine;

public class ZoomAnimation : MonoBehaviour
{
    public float animationDuration = 1.0f;
    public float targetScale = 1.5f;

    private Vector3 originalScale;

    private Coroutine currentAnimation;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void OnEnable()
    {
        // Al activar el objeto, inicia automáticamente la animación de acercamiento
        currentAnimation = StartCoroutine(ZoomIn());
    }

    public void StartZoomOutAnimation()
    {
        // Inicia manualmente la animación de alejamiento desde un controlador externo
        StartCoroutine(ZoomOut());
    }

    IEnumerator ZoomIn()
    {
        float startTime = Time.time;
        float endTime = startTime + animationDuration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / animationDuration;
            transform.localScale = Vector3.Lerp(originalScale, originalScale * targetScale, t);
            yield return null;
        }

        transform.localScale = originalScale * targetScale;
    }

    IEnumerator ZoomOut()
    {
        float startTime = Time.time;
        float endTime = startTime + animationDuration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / animationDuration;
            transform.localScale = Vector3.Lerp(originalScale * targetScale, originalScale, t);
            yield return null;
        }

        transform.localScale = originalScale;
        gameObject.SetActive(false); // Desactiva el objeto al final de la animación de alejamiento
    }
}
