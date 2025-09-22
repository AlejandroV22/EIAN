using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ZonaInteractiva : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image image;
    private Color originalColor;
    private float originalAlpha;

    public string nombreEscena;
    public PanelAdvertencia panelAdvertencia;

    [Header("Tracking")]
    public int zonaID;
    public string nombreTema; 

    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
        originalAlpha = image.color.a;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Solo si las zonas están activas (toggle encendido)
        if (ToggleZonasInteractivas.zonasActivas && image.color.a > 0)
        {
            Color hoverColor = originalColor;
            hoverColor.a = Mathf.Clamp01(originalAlpha + 0.3f); // menos transparencia
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
        switch(zonaID) 
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
