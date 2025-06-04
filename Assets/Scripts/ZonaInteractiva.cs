using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ZonaInteractiva : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public Color hoverColor = new Color(1f, 1f, 1f, 0.3f); // Color al pasar el mouse
    private Image image;
    private Color originalColor;
    public string nombreEscena;

    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverColor;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
          SceneManager.LoadScene(nombreEscena);
    }
}
