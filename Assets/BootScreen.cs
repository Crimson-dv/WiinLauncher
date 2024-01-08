using UnityEngine;
using UnityEngine.UI;

public class BootScreen : MonoBehaviour
{
    public Image bootImage;
    public float fadeInDuration = 2f;

    private float currentTime = 0f;

    void Start()
    {
        // Inicializa la opacidad a cero
        Color startColor = bootImage.color;
        startColor.a = 0f;
        bootImage.color = startColor;
    }

    void Update()
    {
        // Incrementa el tiempo actual
        currentTime += Time.deltaTime;

        // Calcula el valor de interpolación usando Lerp
        float lerpValue = currentTime / fadeInDuration;

        // Limita el valor de interpolación entre 0 y 1
        lerpValue = Mathf.Clamp01(lerpValue);

        // Interpola el color de forma suave
        Color targetColor = bootImage.color;
        targetColor.a = Mathf.Lerp(0f, 1f, lerpValue);
        bootImage.color = targetColor;

        // Desactiva el script cuando se completa la transición
        if (lerpValue >= 1f)
        {
            enabled = false;
        }
    }
}
