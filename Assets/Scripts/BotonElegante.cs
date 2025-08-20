using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotonElegante : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject subrayado; // arrastra el objeto "Subrayado" en el inspector
    private Vector3 escalaOriginal;

    void Start()
    {
        if (subrayado != null)
            subrayado.SetActive(false);

        escalaOriginal = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (subrayado != null)
            subrayado.SetActive(true);

        // efecto de crecer un poquito
        transform.localScale = escalaOriginal * 1.05f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (subrayado != null)
            subrayado.SetActive(false);

        // volver a la escala original
        transform.localScale = escalaOriginal;
    }
}
