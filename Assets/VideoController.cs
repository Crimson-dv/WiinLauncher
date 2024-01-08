using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PanelVideoPair
{
    public GameObject panel;
    public string videoName;
}

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public List<PanelVideoPair> panelVideoPairs;

    void Start()
    {
        // Asignar funciones a los paneles
        foreach (var pair in panelVideoPairs)
        {
            Button panelButton = pair.panel.GetComponent<Button>();
            if (panelButton != null)
            {
                panelButton.onClick.AddListener(() => PlayVideo(pair.videoName));
            }
            else
            {
                Debug.LogError("El panel no tiene un componente Button adjunto: " + pair.panel.name);
            }
        }
    }

    void PlayVideo(string videoName)
    {
        VideoClip videoClip = Resources.Load<VideoClip>("Videos/" + videoName);

        if (videoClip != null)
        {
            // Detener la reproducción actual
            videoPlayer.Stop();

            // Asignar el nuevo video
            videoPlayer.clip = videoClip;

            // Esperar un frame antes de reproducir (puedes ajustar el tiempo según sea necesario)
            StartCoroutine(PlayVideoAfterFrame());
        }
        else
        {
            Debug.LogError("Video no encontrado: " + videoName);
        }
    }

    IEnumerator PlayVideoAfterFrame()
    {
        yield return null; // Esperar un frame

        // Reproducir el video
        videoPlayer.Play();
    }
}
