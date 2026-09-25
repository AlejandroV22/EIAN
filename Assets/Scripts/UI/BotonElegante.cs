using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BotonElegante : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 escalaOriginal;
    private Color colorOriginal;
    private TMP_Text textoBoton;

    public float escalador = 1.15f;
    public GameObject flecha;

    void Start()
    {
        flecha.SetActive(false);
        escalaOriginal = transform.localScale;
        textoBoton = GetComponentInChildren<TMP_Text>();
        colorOriginal = textoBoton.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = escalaOriginal * escalador;
        flecha.SetActive(true);
        textoBoton.color = new Color(0f, 0f, 145f / 255f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = escalaOriginal;
        flecha.SetActive(false);
        textoBoton.color = colorOriginal;
    }
}