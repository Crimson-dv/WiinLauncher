using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Diagnostics;

[RequireComponent(typeof(AudioSource))]
public class ButtonStart : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float hoverScaleFactor = 1.1f;
    public float animationDuration = 0.2f;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private Vector3 originalScale;
    private AudioSource audioSource;

    void Start()
    {
        originalScale = transform.localScale;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = hoverSound;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ScaleOverTime(transform.localScale, originalScale * hoverScaleFactor, animationDuration));

        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(ScaleOverTime(transform.localScale, originalScale, animationDuration));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        OpenExeFile(@"steam://rungameid/271590");
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

    void OpenExeFile(string filePath)
    {
        Process.Start(new ProcessStartInfo(filePath));
    }
}
