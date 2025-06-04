using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ZonaInteractiva2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Color hoverColor = new Color(1f, 1f, 1f, 0.3f); 
    private Image image;
    private Color originalColor;

    public string infoTexto; 
    public InfoPanel panel;  

    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(image.color.a != 0){
            image.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(image.color.a != 0){
            image.color = originalColor;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
          panel.MostrarInformacion(infoTexto); // Muestra el texto en el panel
          Debug.Log("Clic detectado en: " + gameObject.name); // Nuevo log 
    }

}
