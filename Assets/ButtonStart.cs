using UnityEngine;
using UnityEngine.EventSystems;
using System.Diagnostics;
using System.Collections; // Agregamos esta línea
using System;

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

        try
        {
            EjecutarEmuladorConROM();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"Error al ejecutar el emulador: {e.Message}");
        }
    }

    void EjecutarEmuladorConROM()
    {
        string rutaEmuladorDolphin = "C:\\Users\\Jeremy\\Desktop\\Dolphin-x64\\Dolphin.exe";
        string rutaROM = "C:\\Users\\Jeremy\\Downloads\\Telegram Desktop\\Super Smash Bros. Brawl (EUR) (En,Fr,De,Es,It).iso";

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = rutaEmuladorDolphin;
        startInfo.Arguments = $"\"{rutaROM}\" --fullscreen";

        Process.Start(startInfo);
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
