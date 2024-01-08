using UnityEngine;

public class MuteAudioOnCanvasActivation : MonoBehaviour
{
    public Canvas targetCanvas; // Canvas que se activará/desactivará
    public AudioSource audioSource; // AudioSource que se muteará

    void Start()
    {
        // Si no se ha asignado un AudioSource, intenta obtenerlo del mismo GameObject
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Si el Canvas está asignado, desactívalo al inicio (puedes ajustar esto según tus necesidades)
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Verifica si el Canvas está activo y mutea el AudioSource en consecuencia
        if (targetCanvas != null && audioSource != null)
        {
            audioSource.mute = targetCanvas.gameObject.activeSelf;
        }
    }
}
