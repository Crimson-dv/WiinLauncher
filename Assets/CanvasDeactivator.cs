using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class CanvasDeactivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float hoverScaleFactor = 1.1f;
    public float animationDuration = 0.2f;

    public Canvas targetCanvas; // Referencia al canvas que se desactivará
    public VideoPlayer videoPlayer; // Referencia al VideoPlayer que se quiere reiniciar

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Desactiva el canvas al presionar la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape) && targetCanvas != null && targetCanvas.gameObject.activeSelf)
        {
            // Desactiva el canvas y reinicia el VideoPlayer
            targetCanvas.gameObject.SetActive(false);
            ResetVideoPlayer();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ScaleOverTime(transform.localScale, originalScale * hoverScaleFactor, animationDuration));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(ScaleOverTime(transform.localScale, originalScale, animationDuration));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Desactiva el canvas específico al hacer clic en el botón
        if (targetCanvas != null && targetCanvas.gameObject.activeSelf)
        {
            // Desactiva el canvas y reinicia el VideoPlayer
            targetCanvas.gameObject.SetActive(false);
            ResetVideoPlayer();
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

    void ResetVideoPlayer()
    {
        // Establece el VideoClip en None cuando el canvas se desactiva
        if (videoPlayer != null)
        {
            videoPlayer.clip = null;
        }
    }
}
