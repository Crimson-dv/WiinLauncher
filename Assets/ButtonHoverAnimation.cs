using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(AudioSource))] // Aseg�rate de que el objeto tenga un componente AudioSource
public class ButtonHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float hoverScaleFactor = 1.1f; // Factor de escala cuando el mouse est� sobre el bot�n
    public float animationDuration = 0.2f; // Duraci�n de la animaci�n en segundos
    public AudioClip hoverSound; // Sonido al pasar el cursor sobre el bot�n
    public AudioClip clickSound; // Sonido al hacer clic en el bot�n

    private Vector3 originalScale;
    private AudioSource audioSource;

    void Start()
    {
        // Almacena la escala original del bot�n
        originalScale = transform.localScale;

        // Obt�n o agrega el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Asigna los sonidos al AudioSource
        audioSource.clip = hoverSound;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Inicia la animaci�n de agrandamiento cuando el mouse entra en el bot�n
        StartCoroutine(ScaleOverTime(transform.localScale, originalScale * hoverScaleFactor, animationDuration));

        // Reproduce el sonido al pasar el cursor sobre el bot�n
        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Inicia la animaci�n de retorno a la escala original cuando el mouse sale del bot�n
        StartCoroutine(ScaleOverTime(transform.localScale, originalScale, animationDuration));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Reproduce el sonido al hacer clic en el bot�n
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    IEnumerator ScaleOverTime(Vector3 startScale, Vector3 targetScale, float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, (Time.time - startTime) / duration);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
