using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BotonesQuiz : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Colores del texto")]
    public Color normalColor = Color.black;   // Color por defecto
    public Color hoverColor = Color.red;      // Color al pasar el mouse

    private TextMeshProUGUI textoBoton;

    void Start()
    {
        
        textoBoton = GetComponentInChildren<TextMeshProUGUI>();

        if (textoBoton != null)
        {
            textoBoton.color = normalColor;
        }
        else
        {
            Debug.LogWarning("No se encontró un TextMeshProUGUI en el botón " + gameObject.name);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (textoBoton != null)
        {
            textoBoton.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (textoBoton != null)
        {
            textoBoton.color = normalColor;
        }
    }
}
