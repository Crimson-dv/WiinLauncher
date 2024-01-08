using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragAndDrop : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;

        // Asegurarse de que los paneles estén organizados en el orden correcto al inicio
        OrganizePanels();
    }

    void OrganizePanels()
    {
        // Obtener todos los paneles con el tag "Channel"
        GameObject[] panels = GameObject.FindGameObjectsWithTag("Channel");

        // Organizar los paneles en el orden correcto
        foreach (var panel in panels)
        {
            panel.GetComponent<RectTransform>().SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Asegurarse de que el panel arrastrado esté al frente durante el arrastre
        rectTransform.SetAsLastSibling();

        rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Raycast para encontrar los objetos bajo el cursor
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        // Buscar el panel bajo el cursor
        RectTransform targetPanel = null;

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Channel"))
            {
                RectTransform potentialTarget = result.gameObject.GetComponent<RectTransform>();

                // Verificar si el panel de destino es diferente
                if (potentialTarget != rectTransform)
                {
                    // Almacenar las posiciones originales
                    Vector2 originalPanelPosition = originalPosition;
                    Vector2 targetPanelPosition = potentialTarget.anchoredPosition;

                    // Intercambiar posiciones
                    rectTransform.anchoredPosition = targetPanelPosition;
                    potentialTarget.anchoredPosition = originalPanelPosition;

                    // Actualizar la posición original para el próximo arrastre
                    originalPosition = rectTransform.anchoredPosition;

                    // Organizar los paneles nuevamente
                    OrganizePanels();

                    break;
                }
            }
        }

        // Si no hay panel de destino, restaurar a la posición original
        if (targetPanel == null)
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
