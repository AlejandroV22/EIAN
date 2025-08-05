using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ZonaInteractiva : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Color hoverColor = new Color(1f, 1f, 1f, 0.3f); // Color al pasar el mouse
    private Image image;
    private Color originalColor;
    public string nombreEscena;

    [Header("Tracking")]
    public int zonaID;
    public string nombreTema; 
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
            switch(zonaID) 
            {
            case 1:
                SceneManager.LoadScene(nombreEscena);
                break;
            case 2:
                if (PlayerPrefs.GetInt("QuizAprobado_" + nombreTema, 0) == 0)
                {
                    Debug.Log("Faltan resolver el quiz  para este tema.");
                    return;
                }
                SceneManager.LoadScene(nombreEscena);
                Debug.Log("El quiz fue aprobado. Abriendo nueva ruta");
                
                break;
            default:
                return;
            }
    }
}
