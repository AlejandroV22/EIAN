using UnityEngine;
using UnityEngine.EventSystems;

public class ZonaInteractiva2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string infoTexto;
    public InfoPanel panel;

    [Header("Tracking")]
    public string zonaID;
    public string temaID;

    private UIPolygon polygon;
    private Color originalColor;
    private float originalAlpha;

    void Start()
    {
        polygon = GetComponent<UIPolygon>();
        if (polygon == null)
        {
            Debug.LogError($"[ZonaInteractiva2] No se encontró un componente UIPolygon en {gameObject.name}");
            enabled = false;
            return;
        }

        originalColor = polygon.color;
        originalAlpha = polygon.color.a;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ToggleZonasInteractivas.zonasActivas && polygon.color.a > 0)
        {
            Color hoverColor = originalColor;
            hoverColor.a = Mathf.Clamp01(originalAlpha + 0.3f);
            polygon.color = hoverColor;
            polygon.SetVerticesDirty(); // actualiza visualmente el color
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ToggleZonasInteractivas.zonasActivas && polygon.color.a > 0)
        {
            polygon.color = originalColor;
            polygon.SetVerticesDirty();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (panel != null)
        {
            panel.MostrarInformacion(infoTexto);
        }

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
