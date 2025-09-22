using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZonaInteractiva2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string infoTexto;
    public InfoPanel panel;

    [Header("Tracking")]
    public string zonaID;
    public string temaID;

    private Image image;
    private Color originalColor;
    private float originalAlpha;

    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
        originalAlpha = image.color.a;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ToggleZonasInteractivas.zonasActivas && image.color.a > 0)
        {
            Color hoverColor = originalColor;
            hoverColor.a = Mathf.Clamp01(originalAlpha + 0.3f);
            image.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ToggleZonasInteractivas.zonasActivas && image.color.a > 0)
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
            PlayerPrefs.SetInt("ZonaVisitada_" + zonaID, 1); 
            PlayerPrefs.Save();
            Debug.Log("Zona marcada como visitada: " + zonaID);
        }
    }

    public bool FueVisitada()
    {
        return PlayerPrefs.GetInt("ZonaVisitada_" + zonaID, 0) == 1;
    }
}
