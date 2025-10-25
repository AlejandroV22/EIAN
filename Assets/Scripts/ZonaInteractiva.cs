using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ZonaInteractiva : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private UIPolygon polygon;              // reemplaza Image
    private Color originalColor;
    private float originalAlpha;

    public string nombreEscena;
    public PanelAdvertencia panelAdvertencia;

    [Header("Tracking")]
    public int zonaID;
    public string nombreTema;

    void Start()
    {
        polygon = GetComponent<UIPolygon>();
        if (polygon == null)
        {
            Debug.LogError($"[ZonaInteractiva] No se encontró un componente UIPolygon en {gameObject.name}");
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
            hoverColor.a = Mathf.Clamp01(originalAlpha + 0.3f); // menos transparencia
            polygon.color = hoverColor;
            polygon.SetVerticesDirty(); // fuerza a redibujar el color actualizado
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
        if (!ToggleZonasInteractivas.zonasActivas)
            return;

        switch (zonaID)
        {
            case 1:
                SceneManager.LoadScene(nombreEscena);
                break;
            case 2:
                if (PlayerPrefs.GetInt("QuizAprobado_" + nombreTema, 0) == 0)
                {
                    panelAdvertencia.MostrarMensaje("Debes aprobar el quiz 1 para poder acceder.");
                    return;
                }
                SceneManager.LoadScene(nombreEscena);
                break;
            case 3:
                if (PlayerPrefs.GetInt("QuizAprobado_" + nombreTema, 0) == 0)
                {
                    panelAdvertencia.MostrarMensaje("Debes aprobar el quiz 2 para poder acceder.");
                    return;
                }
                SceneManager.LoadScene(nombreEscena);
                break;
            case 4:
                if (PlayerPrefs.GetInt("QuizAprobado_" + nombreTema, 0) == 0)
                {
                    panelAdvertencia.MostrarMensaje("Debes aprobar el quiz 3 para poder acceder.");
                    return;
                }
                SceneManager.LoadScene(nombreEscena);
                break;
            default:
                return;
        }
    }
}
