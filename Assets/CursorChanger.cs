using UnityEngine;
using UnityEngine.UI;

public class CursorChanger : MonoBehaviour
{
    public Texture2D cursorTexture; // Asigna la textura del cursor en el Inspector
    public Color shadowColor = new Color(0f, 0f, 0f, 0.5f); // Color y transparencia de la sombra
    public Vector2 shadowOffset = new Vector2(2f, -2f); // Desplazamiento de la sombra
    public CursorMode cursorMode = CursorMode.Auto;

    private GameObject shadowObject;

    void Start()
    {
        // Crear un objeto de sombra al inicio
        CreateShadow();
    }

    void Update()
    {
        // Actualizar la posición de la sombra según la posición del ratón
        Vector3 mousePosition = Input.mousePosition;
        Vector2 localCursor;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform as RectTransform,
            mousePosition,
            Camera.main,
            out localCursor
        );

        // Ajustar las proporciones de la sombra según el tamaño del cursor
        shadowObject.GetComponent<RectTransform>().sizeDelta = new Vector2(cursorTexture.width, cursorTexture.height);
        shadowObject.transform.localPosition = localCursor + shadowOffset;

        // Cambiar el cursor
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
    }

    private void CreateShadow()
    {
        // Crear un objeto de sombra si no existe
        if (shadowObject == null)
        {
            shadowObject = new GameObject("CursorShadow");
            shadowObject.transform.SetParent(transform, false);

            // Agregar un componente RawImage para mostrar la textura de sombra
            RawImage shadowImage = shadowObject.AddComponent<RawImage>();
            shadowImage.texture = cursorTexture;
            shadowImage.color = shadowColor;
        }
    }
}
