using UnityEngine;

public class MuteAudioOnCanvasActivation : MonoBehaviour
{
    public Canvas targetCanvas; // Canvas que se activar�/desactivar�
    public AudioSource audioSource; // AudioSource que se mutear�

    void Start()
    {
        // Si no se ha asignado un AudioSource, intenta obtenerlo del mismo GameObject
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Si el Canvas est� asignado, desact�valo al inicio (puedes ajustar esto seg�n tus necesidades)
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Verifica si el Canvas est� activo y mutea el AudioSource en consecuencia
        if (targetCanvas != null && audioSource != null)
        {
            audioSource.mute = targetCanvas.gameObject.activeSelf;
        }
    }
}
