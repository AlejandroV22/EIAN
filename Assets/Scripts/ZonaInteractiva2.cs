using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZonaInteractiva2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private const string NOMBRE_PANEL = "PanelInfoRecorte";

    // Cache compartido: la búsqueda se hace una sola vez para todas las zonas
    private static InfoPanel panelCache;

    public string infoTexto;
    public InfoPanel panel;

    [Header("Tracking")]
    public string zonaID;
    public string temaID;

    [Header("Hover")]
    [SerializeField] private float alphaExtra = 0.3f;

    private Graphic graphic;
    private Color colorAntesDeHover;
    private bool enHover;

    void Awake()
    {
        if (panel == null)
            panel = BuscarPanel();
    }

    void Start()
    {
        graphic = GetComponent<Graphic>();

        if (graphic == null)
        {
            Debug.LogError($"[ZonaInteractiva2] No se encontró un componente Graphic en {gameObject.name}");
            enabled = false;
        }
    }

    private static InfoPanel BuscarPanel()
    {
        // Si ya lo encontró otra zona (y no fue destruido al cambiar de escena), se reutiliza
        if (panelCache != null) return panelCache;

        // Include = también encuentra objetos desactivados en la jerarquía
        InfoPanel[] paneles = FindObjectsByType<InfoPanel>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (InfoPanel p in paneles)
        {
            if (p.gameObject.name == NOMBRE_PANEL)
            {
                panelCache = p;
                return p;
            }
        }

        Debug.LogWarning($"[ZonaInteractiva2] No se encontró ningún InfoPanel llamado '{NOMBRE_PANEL}' en la escena.");
        return null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (graphic == null || enHover) return;

        if (ToggleZonasInteractivas.zonasActivas && graphic.color.a > 0)
        {
            colorAntesDeHover = graphic.color;
            enHover = true;

            Color hoverColor = colorAntesDeHover;
            hoverColor.a = Mathf.Clamp01(colorAntesDeHover.a + alphaExtra);
            graphic.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RestaurarColor();
    }

    void OnDisable()
    {
        RestaurarColor();
    }

    private void RestaurarColor()
    {
        if (!enHover || graphic == null) return;

        Color color = colorAntesDeHover;

        if (!ToggleZonasInteractivas.zonasActivas)
            color.a = 0f;

        graphic.color = color;
        enHover = false;
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