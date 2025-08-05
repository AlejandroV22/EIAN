using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZonaInteractiva2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Color hoverColor = new Color(1f, 1f, 1f, 0.3f);
    private Image image;
    private Color originalColor;

    public string infoTexto;
    public InfoPanel panel;

    [Header("Tracking")]
    public string zonaID;
    public string temaID;

    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image.color.a != 0)
        {
            image.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image.color.a != 0)
        {
            image.color = originalColor;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        panel.MostrarInformacion(infoTexto);
        Debug.Log("Clic detectado en: " + gameObject.name);
        if (!string.IsNullOrEmpty(zonaID))
        {
            PlayerPrefs.SetInt("ZonaVisitada_" + zonaID, 1); // marca como visitada
            PlayerPrefs.Save();
            Debug.Log("Zona marcada como visitada: " + zonaID);
        }
    }

    
    public bool FueVisitada()
    {
        return PlayerPrefs.GetInt("ZonaVisitada_" + zonaID, 0) == 1;
    }
}
