using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ZonaInteractiva : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string nombreEscena;
    public PanelAdvertencia panelAdvertencia;

    [Header("Tracking")]
    public int zonaID;
    public string nombreTema;

    [Header("Hover")]
    [SerializeField] private float alphaExtra = 0.3f;

    private UIPolygon polygon;
    private Color colorAntesDeHover;
    private bool enHover;

    void Start()
    {
        polygon = GetComponent<UIPolygon>();

        if (polygon == null)
        {
            Debug.LogError($"[ZonaInteractiva] No se encontró un componente UIPolygon en {gameObject.name}");
            enabled = false;
            return;
        }

        // La zona debe poder recibir clics aunque su alpha sea 0.
        polygon.raycastTarget = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (polygon == null || enHover) return;

        // Solo hay hover si las zonas están visibles
        if (!ToggleZonasInteractivas.zonasActivas || polygon.color.a <= 0f)
            return;

        // Guardamos el color ACTUAL, no el de Start()
        colorAntesDeHover = polygon.color;
        enHover = true;

        Color hoverColor = colorAntesDeHover;
        hoverColor.a = Mathf.Clamp01(colorAntesDeHover.a + alphaExtra);
        polygon.color = hoverColor;
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
        if (!enHover || polygon == null) return;

        Color color = colorAntesDeHover;

        // Si el toggle se apagó durante el hover, respeta ese estado
        if (!ToggleZonasInteractivas.zonasActivas)
            color.a = 0f;

        polygon.color = color;
        enHover = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Sin comprobar zonasActivas: la zona sigue siendo clickeable
        // aunque visualmente tenga alpha = 0.
        Debug.Log("Clic detectado en: " + gameObject.name);

        switch (zonaID)
        {
            case 1:
                SceneManager.LoadScene(nombreEscena);
                break;

            case 2:
            case 3:
            case 4:
                // zonaID 2 -> quiz 1, 3 -> quiz 2, 4 -> quiz 3
                if (PlayerPrefs.GetInt("QuizAprobado_" + nombreTema, 0) == 0)
                {
                    if (panelAdvertencia != null)
                    {
                        panelAdvertencia.MostrarMensaje(
                            $"Debes aprobar el quiz {zonaID - 1} para poder acceder."
                        );
                    }
                    return;
                }

                SceneManager.LoadScene(nombreEscena);
                break;

            default:
                Debug.LogWarning($"[ZonaInteractiva] zonaID {zonaID} no tiene una acción asignada.");
                break;
        }
    }
}