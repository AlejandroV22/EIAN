using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BotonElegante : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 escalaOriginal;
    private Color colorOriginal;

    public float escalador = 1.15f;
    public GameObject flecha;
    public TMP_Text textoBoton;

    void Start()
    {
        if (flecha != null) flecha.SetActive(false);

        escalaOriginal = transform.localScale;

        if (textoBoton == null)
            textoBoton = GetComponentInChildren<TMP_Text>(true); // true = incluye inactivos

        if (textoBoton == null)
        {
            Debug.LogError("No se encontró TMP_Text en " + gameObject.name, this);
            return;
        }

        colorOriginal = textoBoton.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = escalaOriginal * escalador;
        if (flecha != null) flecha.SetActive(true);
        if (textoBoton != null) textoBoton.color = new Color(0f, 0f, 145f / 255f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = escalaOriginal;
        if (flecha != null) flecha.SetActive(false);
        if (textoBoton != null) textoBoton.color = colorOriginal;
    }
}