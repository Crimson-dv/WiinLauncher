using UnityEngine;
using UnityEngine.EventSystems;

public class PanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public GameObject targetObject; // Objeto que se activar�/desactivar�
    public AudioClip activationSound; // Sonido al activar el objeto
    public AudioClip deactivationSound; // Sonido al desactivar el objeto
    public GameObject triggerObject; // Objeto o bot�n que provoca la desactivaci�n
    private AudioSource audioSource;

    void Start()
    {
        // Aseg�rate de que el objeto objetivo est� inicialmente desactivado
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }

        // A�ade o obt�n el componente AudioSource en este GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Activa o desactiva el objeto y reproduce el sonido correspondiente
        ToggleObjectAndPlaySound();
    }

    void ToggleObjectAndPlaySound()
    {
        if (targetObject != null && triggerObject != null)
        {
            // Almacena el estado actual antes de cambiarlo
            bool currentState = targetObject.activeSelf;

            // Invierte el estado de activaci�n/desactivaci�n
            targetObject.SetActive(!currentState);

            // Reproduce el sonido correspondiente
            if (audioSource != null)
            {
                if (!currentState && activationSound != null)
                {
                    audioSource.PlayOneShot(activationSound);
                }
                else if (currentState && deactivationSound != null)
                {
                    audioSource.PlayOneShot(deactivationSound);
                }
            }
        }
    }
}
