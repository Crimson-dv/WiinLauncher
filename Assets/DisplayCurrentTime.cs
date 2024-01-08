using UnityEngine;
using TMPro;

public class DisplayCurrentTime : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Start()
    {
        // Llama al método UpdateTime cada minuto
        InvokeRepeating("UpdateTime", 0f, 60f);
    }

    void UpdateTime()
    {
        // Obtiene la hora actual
        System.DateTime currentTime = System.DateTime.Now;

        // Formatea la hora actual en formato de 12 horas sin AM/PM
        string formattedTime = currentTime.ToString("h:mm");

        // Actualiza el texto en el componente TextMeshProUGUI
        if (timeText != null)
        {
            timeText.text = formattedTime;
        }
    }
}
