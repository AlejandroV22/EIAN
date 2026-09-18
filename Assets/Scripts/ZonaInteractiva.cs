using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ZonaInteractiva : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private UIPolygon polygon;
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

        // IMPORTANTE:
        // La zona siempre debe poder recibir raycasts/clicks,
        // incluso cuando su alpha sea 0.
        polygon.raycastTarget = true;

        // Aplicar inmediatamente el estado global actual.
        AplicarEstadoVisual();
    }

    private void AplicarEstadoVisual()
    {
        Color color = polygon.color;

        if (ToggleZonasInteractivas.zonasActivas)
        {
            color.a = originalAlpha;
        }
        else
        {
            color.a = 0f;
        }

        polygon.color = color;
        polygon.SetVerticesDirty();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // El hover SOLO modifica la apariencia cuando las zonas
        // están visibles.
        if (!ToggleZonasInteractivas.zonasActivas)
            return;

        Color hoverColor = originalColor;
        hoverColor.a = Mathf.Clamp01(originalAlpha + 0.3f);

        polygon.color = hoverColor;
        polygon.SetVerticesDirty();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!ToggleZonasInteractivas.zonasActivas)
            return;

        polygon.color = originalColor;
        polygon.SetVerticesDirty();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // NO poner aquí:
        // if (!ToggleZonasInteractivas.zonasActivas) return;

        // El objetivo es que la zona siga siendo clickeable
        // aunque visualmente tenga alpha = 0.

        Debug.Log("Clic detectado en: " + gameObject.name);

        switch (zonaID)
        {
            case 1:
                SceneManager.LoadScene(nombreEscena);
                break;

            case 2:
                if (PlayerPrefs.GetInt("QuizAprobado_" + nombreTema, 0) == 0)
                {
                    if (panelAdvertencia != null)
                    {
                        panelAdvertencia.MostrarMensaje(
                            "Debes aprobar el quiz 1 para poder acceder."
                        );
                    }

                    return;
                }

                SceneManager.LoadScene(nombreEscena);
                break;

            case 3:
                if (PlayerPrefs.GetInt("QuizAprobado_" + nombreTema, 0) == 0)
                {
                    if (panelAdvertencia != null)
                    {
                        panelAdvertencia.MostrarMensaje(
                            "Debes aprobar el quiz 2 para poder acceder."
                        );
                    }

                    return;
                }

                SceneManager.LoadScene(nombreEscena);
                break;

            case 4:
                if (PlayerPrefs.GetInt("QuizAprobado_" + nombreTema, 0) == 0)
                {
                    if (panelAdvertencia != null)
                    {
                        panelAdvertencia.MostrarMensaje(
                            "Debes aprobar el quiz 3 para poder acceder."
                        );
                    }

                    return;
                }

                SceneManager.LoadScene(nombreEscena);
                break;

            default:
                Debug.LogWarning(
                    $"[ZonaInteractiva] zonaID {zonaID} no tiene una acción asignada."
                );
                break;
        }
    }
}