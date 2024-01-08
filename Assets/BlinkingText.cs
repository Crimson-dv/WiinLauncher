using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class BlinkingText : MonoBehaviour
{
    public CanvasGroup bootScreenCanvasGroup;
    public TextMeshProUGUI blinkingText;
    public float appearDuration = 2f;
    public float fadeDuration = 1f;
    public float blinkInterval = 1f;
    public float keepLowOpacityDuration = 9f;
    public string menuSceneName = "Menu";
    public AudioClip keyPressSound;  // Nuevo campo para asignar el sonido en el Inspector

    private float timeElapsed = 0f;
    private float lowOpacityTimeElapsed = 0f;
    private bool startedBlinking = false;
    private bool keyPressed = false;
    private AudioSource audioSource;  // Nuevo AudioSource para reproducir el sonido

    void Start()
    {
        // Configura el AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = keyPressSound;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Solo empieza a contar el tiempo después de 6 segundos
        if (Time.time >= 6f && !startedBlinking)
        {
            startedBlinking = true;
            InvokeRepeating("ToggleBlinking", appearDuration, blinkInterval);
        }

        if (startedBlinking)
        {
            timeElapsed += Time.deltaTime;

            float lerpValue = Mathf.PingPong(timeElapsed / fadeDuration, 1f);

            Color targetColor = blinkingText.color;
            targetColor.a = lerpValue;
            blinkingText.color = targetColor;

            // Verifica si se presionó cualquier tecla del teclado o se hizo clic con el mouse
            if (!keyPressed && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
            {
                keyPressed = true;
                // Baja la opacidad del objeto CanvasGroup
                StartCoroutine(FadeOutCanvasGroup(bootScreenCanvasGroup));
                // Reproduce el sonido
                PlayKeyPressSound();
            }

            // Cambia a la escena llamada "Menu" después de la opacidad baja
            if (keyPressed && bootScreenCanvasGroup.alpha == 0f)
            {
                if (lowOpacityTimeElapsed >= keepLowOpacityDuration)
                {
                    SceneManager.LoadScene(menuSceneName);
                }
            }

            // Incrementa el tiempo transcurrido con opacidad baja
            if (bootScreenCanvasGroup.alpha == 0f)
            {
                lowOpacityTimeElapsed += Time.deltaTime;
            }
        }
    }

    void ToggleBlinking()
    {
        // No necesitas cambiar isBlinking, simplemente ajusta la dirección
        // direction *= -1f;
    }

    IEnumerator FadeOutCanvasGroup(CanvasGroup canvasGroup)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float lerpValue = Mathf.SmoothStep(1f, 0f, elapsedTime / fadeDuration);

            // Interpola la opacidad del CanvasGroup
            canvasGroup.alpha = lerpValue;

            yield return null;
        }

        // Asegúrate de que la opacidad sea 0 al finalizar
        canvasGroup.alpha = 0f;
    }

    void PlayKeyPressSound()
    {
        // Reproduce el sonido solo una vez
        if (keyPressSound != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
