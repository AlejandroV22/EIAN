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

    private Graphic graphic;
    private Color originalColor;
    private float originalAlpha;

    void Start()
    {
        graphic = GetComponent<Graphic>();

        if (graphic == null)
        {
            Debug.LogError($"[ZonaInteractiva2] No se encontró un componente Graphic en {gameObject.name}");
            enabled = false;
            return;
        }

        originalColor = graphic.color;
        originalAlpha = graphic.color.a;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ToggleZonasInteractivas.zonasActivas && graphic.color.a > 0)
        {
            Color hoverColor = originalColor;
            hoverColor.a = Mathf.Clamp01(originalAlpha + 0.3f);
            graphic.color = hoverColor;
            graphic.SetVerticesDirty();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ToggleZonasInteractivas.zonasActivas && graphic.color.a > 0)
        {
            graphic.color = originalColor;
            graphic.SetVerticesDirty();
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