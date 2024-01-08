using UnityEngine;

public class MenuFadeInAnimation : MonoBehaviour
{
    public CanvasGroup menuCanvasGroup;
    public float fadeInDuration = 2f;

    private void Start()
    {
        // Asegúrate de que el CanvasGroup se inicialice con opacidad 0
        menuCanvasGroup.alpha = 0f;

        // Inicia la animación de desvanecimiento
        StartCoroutine(FadeInAnimation());
    }

    private System.Collections.IEnumerator FadeInAnimation()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            // Incrementa gradualmente la opacidad
            menuCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);

            // Espera un frame
            yield return null;

            // Incrementa el tiempo transcurrido
            elapsedTime += Time.deltaTime;
        }

        // Asegúrate de que la opacidad sea 1 al finalizar
        menuCanvasGroup.alpha = 1f;
    }
}
